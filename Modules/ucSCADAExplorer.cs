using Plate.Domain.Interfaces;
using Plate.Modules;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plate
{
    public partial class ucSCADAExplorer : BaseModule
    {
        // ① 외부에서 주입할 수 있도록 프로퍼티 추가
        public IAccessDbRepository Repository { get; set; }

        public ucSCADAExplorer() {

            InitializeComponent();
            InitTreeView();
            //AddAllNodes(iShow.Down);
        }
        IOverlaySplashScreenHandle progressPanelHandle = null;

        void ShowProgress()
        {
            progressPanelHandle = ShowProgressPanel();
        }
        void CloseProgress()
        {
            if (progressPanelHandle != null)
                CloseProgressPanel(progressPanelHandle);

        }
        private void InitTreeView() {
           
            treeView.OptionsView.ShowColumns = true;
            treeView.OptionsView.ShowIndicator = false;
            treeView.OptionsView.ShowVertLines = false;
            treeView.OptionsView.ShowHorzLines = false;
            treeView.OptionsBehavior.Editable = false;
            treeView.OptionsSelection.EnableAppearanceFocusedCell = false;
            treeView.AfterCheckNode += OnAfterCheckNode;

            // --- 필터 기능 켜기 ---
            //treeView.OptionsView.ShowAutoFilterRow = true;      // 상단에 필터 행 표시
            //treeView.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
            //treeView.OptionsFilter.ShowAllTableValuesInFilterPopup = true;

            // 또는 찾기 패널 (필터 행 대신):
            treeView.OptionsFind.AlwaysVisible = true;
            treeView.OptionsFind.FindNullPrompt = "Keyword...";

            // 컬럼명과 FieldName 을 꼭 지정하세요
            var col = treeView.Columns.Add();
            col.FieldName = "Name";
            col.Caption = "TagName";
            col.Visible = true;
            // ③ 컬럼별 필터 조건 설정
            col.OptionsFilter.AutoFilterCondition =
                DevExpress.XtraTreeList.Columns.AutoFilterCondition.Contains;

            // …컬럼/룩앤필 설정…
            treeView.OptionsView.ShowColumns = true;
            treeView.OptionsFind.AlwaysVisible = true;  // 상단 찾기 패널
            treeView.OptionsFind.ShowClearButton = true;
            treeView.OptionsFind.ShowFindButton = true;

           
            // --- 체크박스 켜기 ---
            treeView.OptionsView.ShowCheckBoxes = true;         // 노드 옆 체크박스 표시
            treeView.OptionsBehavior.AllowRecursiveNodeChecking = true; // 자식/부모 자동 연동

            // 필요한 이벤트 훅
            treeView.CustomDrawNodeCell += treeView_CustomDrawNodeCell;
            treeView.AfterExpand += treeView_AfterExpand;
            treeView.AfterCollapse += treeView_AfterCollapse;
            treeView.MouseDoubleClick += treeView_MouseDoubleClick;
        }
        

        private void OnAfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            // e.Node 는 방금 체크/언체크된 노드
            //var isNowChecked = e.Node.CheckState == CheckState.Checked;
            // 1) 전체 체크된 노드 중에서
            var checkedNodes = treeView
                .GetAllCheckedNodes()
                .OfType<DevExpress.XtraTreeList.Nodes.TreeListNode>();

            // 2) Leaf 노드(자식 없는 노드)만 골라내기
            var checkedTags = checkedNodes
                .Where(n => !n.HasChildren)
                .Select(n => n.GetValue("Name").ToString())
                .ToList();

            // 이제 checkedTags 는 체크된 TagName 리스트입니다.
            // 예: 디버그 출력
            OwnerForm.CheckedTags = checkedTags;
            OwnerForm.AppendLog("체크된 태그: " + string.Join(", ", checkedTags));
        }
        public async void PopulateTreeSafeAsync()
        {
            // UI에 표시
            BeginInvoke(new Action(() => ShowProgress()));

            if (Repository == null)
            {
                BeginInvoke(new Action(() =>
                    MessageBox.Show("Repository가 설정되지 않았습니다.", "오류")));
                return;
            }

            // 데이터 로딩은 백그라운드에서 수행
            var treeData = await Task.Run(() => LoadTreeData());

            // UI에서 TreeList 조작
            BeginInvoke(new Action(() =>
            {
                treeView.BeginUpdate();
                treeView.Nodes.Clear();
                AddTreeNodes(treeView, treeData);
                treeView.ExpandAll();
                treeView.EndUpdate();
                CloseProgress();
            }));
        }
        // UI Thread에서 실행됨
        private void AddTreeNodes(TreeList treeView, List<TreeNodeData> nodes, TreeListNode parent = null)
        {
            foreach (var node in nodes)
            {
                // 올바른 AppendNode 사용
                var newNode = treeView.AppendNode(new object[] { node.Text }, parent);
                // 아이콘 인덱스는 속성으로 설정
                newNode.ImageIndex = node.IconIndex;
                newNode.SelectImageIndex = node.IconIndex;
                AddTreeNodes(treeView, node.Children, newNode);
            }
        }
        // Task.Run에서 호출됨 (UI 접근 없이 메모리 구조만 생성)
        private List<TreeNodeData> LoadTreeData()
        {
            var result = new List<TreeNodeData>();

            foreach (var proj in Repository.GetProjects())
            {
                var projNode = new TreeNodeData { Text = proj.ProjName };

                foreach (var node in Repository.GetNodes(proj.ProjIdbw))
                {
                    var nodeNode = new TreeNodeData { Text = node.NodeName };

                    var zeroTags = Repository.GetTags(0).Where(t => t.DeviceIdbw == 0).ToList();

                    if (zeroTags.Any(t => t.TagType == 3))
                    {
                        var constNode = new TreeNodeData { Text = "Constants" };
                        foreach (var t in zeroTags.Where(t => t.TagType == 3))
                            constNode.Children.Add(new TreeNodeData { Text = t.TagName, IconIndex = 14 });
                        nodeNode.Children.Add(constNode);
                    }

                    if (zeroTags.Any(t => t.TagType == 1))
                    {
                        var calcNode = new TreeNodeData { Text = "Calculates" };
                        foreach (var t in zeroTags.Where(t => t.TagType == 1))
                            calcNode.Children.Add(new TreeNodeData { Text = t.TagName, IconIndex = 14 });
                        nodeNode.Children.Add(calcNode);
                    }

                    foreach (var port in Repository.GetComports(node.ProjNodeIdbw))
                    {
                        var portNode = new TreeNodeData { Text = $"Port{port.ComportIdbw} ({port.InterfaceName})" };

                        foreach (var dev in Repository.GetDevices(port.ComportIdbw))
                        {
                            var devNode = new TreeNodeData { Text = dev.DeviceName };

                            foreach (var tag in Repository.GetTags(dev.DeviceIdbw))
                                devNode.Children.Add(new TreeNodeData { Text = tag.TagName, IconIndex = 14 });

                            portNode.Children.Add(devNode);
                        }

                        nodeNode.Children.Add(portNode);
                    }

                    projNode.Children.Add(nodeNode);
                }

                result.Add(projNode);
            }

            return result;
        }

        // UI Thread에서 실행됨

        void treeView_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e) {
            if(e.Node.Id == 1) 
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
        }
        void SetIndex(TreeListNode node, int index, bool expand) {
            int newIndex = expand ? index - 1 : index + 1;
            if(node.StateImageIndex == index)
                node.StateImageIndex = newIndex;
        }
        void treeView_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            SetIndex(e.Node, 7, true);
            SetIndex(e.Node, 9, true);
        }
        void treeView_AfterCollapse(object sender, DevExpress.XtraTreeList.NodeEventArgs e) {
            SetIndex(e.Node, 6, false);
            SetIndex(e.Node, 8, false);
        }

        public event EventHandler PropertiesItemClick;
        void iShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
        }
        void iProperties_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(PropertiesItemClick != null)
                PropertiesItemClick(sender, EventArgs.Empty);
        }
        public event EventHandler TreeViewItemClick;
        void treeView_MouseDoubleClick(object sender, MouseEventArgs e) {
            if(TreeViewItemClick != null)
                TreeViewItemClick(sender, EventArgs.Empty);
        }
        public event EventHandler RefereshItemClick;

        private void iRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (RefereshItemClick != null)
                RefereshItemClick(sender, EventArgs.Empty);
        }
    }
    public class TreeNodeData
    {
        public string Text { get; set; }
        public int IconIndex { get; set; } = 6;
        public List<TreeNodeData> Children { get; set; } = new List<TreeNodeData>();
    }
}
