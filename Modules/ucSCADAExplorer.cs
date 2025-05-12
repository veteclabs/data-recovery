using DataRecorvery.Domain.Interfaces;
using DataRecorvery.Modules;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DataRecorvery
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
        public void PopulateTree()
        {
            if (Repository == null)
                throw new InvalidOperationException("Repository가 설정되지 않았습니다.");

            treeView.Nodes.Clear();

            foreach (var proj in Repository.GetProjects())
            {
                var nProj = treeView.AppendNode(new object[] { proj.ProjName }, -1, -1, -1, 6);
                foreach (var node in Repository.GetNodes(proj.ProjIdbw))
                {
                    var nNode = treeView.AppendNode(new object[] { node.NodeName }, nProj.Id, -1, -1, 6);
                    
                    var zeroTags = Repository.GetTags(0)
                         .Where(t => t.DeviceIdbw == 0);

                    if (zeroTags.Any(t => t.TagType == 3))
                    {
                        var constNode = treeView.AppendNode(
                            new object[] { "Constants" }, nNode.Id, -1, -1, /*icon*/6);
                        foreach (var t in zeroTags.Where(t => t.TagType == 3))
                            treeView.AppendNode(
                                new object[] { t.TagName }, constNode.Id, -1, -1, 14);
                    }

                    if (zeroTags.Any(t => t.TagType == 1))
                    {
                        var calcNode = treeView.AppendNode(
                            new object[] { "Calculates" }, nNode.Id, -1, -1, /*icon*/6);
                        foreach (var t in zeroTags.Where(t => t.TagType == 1))
                            treeView.AppendNode(
                                new object[] { t.TagName }, calcNode.Id, -1, -1, 14);
                    }


                    foreach (var port in Repository.GetComports(node.ProjNodeIdbw))
                    {
                        var nPort = treeView.AppendNode(new object[] { "Port"+port.ComportIdbw + "(" + port.InterfaceName+")" }, nNode.Id, -1, -1, 6);
                        foreach (var dev in Repository.GetDevices(port.ComportIdbw))
                        {
                            var nDev = treeView.AppendNode(new object[] { dev.DeviceName }, nPort.Id, -1, -1, 6);
                            foreach (var tag in Repository.GetTags(dev.DeviceIdbw))
                            {
                                treeView.AppendNode(new object[] { tag.TagName }, nDev.Id, -1, -1, 14);
                            }
                        }
                    }
                }
            }

            treeView.ExpandAll();
        }
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
}
