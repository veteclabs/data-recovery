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
        // �� �ܺο��� ������ �� �ֵ��� ������Ƽ �߰�
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

            // --- ���� ��� �ѱ� ---
            //treeView.OptionsView.ShowAutoFilterRow = true;      // ��ܿ� ���� �� ǥ��
            //treeView.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
            //treeView.OptionsFilter.ShowAllTableValuesInFilterPopup = true;

            // �Ǵ� ã�� �г� (���� �� ���):
            treeView.OptionsFind.AlwaysVisible = true;
            treeView.OptionsFind.FindNullPrompt = "Keyword...";

            // �÷���� FieldName �� �� �����ϼ���
            var col = treeView.Columns.Add();
            col.FieldName = "Name";
            col.Caption = "TagName";
            col.Visible = true;
            // �� �÷��� ���� ���� ����
            col.OptionsFilter.AutoFilterCondition =
                DevExpress.XtraTreeList.Columns.AutoFilterCondition.Contains;

            // ���÷�/����� ������
            treeView.OptionsView.ShowColumns = true;
            treeView.OptionsFind.AlwaysVisible = true;  // ��� ã�� �г�
            treeView.OptionsFind.ShowClearButton = true;
            treeView.OptionsFind.ShowFindButton = true;

           
            // --- üũ�ڽ� �ѱ� ---
            treeView.OptionsView.ShowCheckBoxes = true;         // ��� �� üũ�ڽ� ǥ��
            treeView.OptionsBehavior.AllowRecursiveNodeChecking = true; // �ڽ�/�θ� �ڵ� ����

            // �ʿ��� �̺�Ʈ ��
            treeView.CustomDrawNodeCell += treeView_CustomDrawNodeCell;
            treeView.AfterExpand += treeView_AfterExpand;
            treeView.AfterCollapse += treeView_AfterCollapse;
            treeView.MouseDoubleClick += treeView_MouseDoubleClick;
        }
        

        private void OnAfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            // e.Node �� ��� üũ/��üũ�� ���
            //var isNowChecked = e.Node.CheckState == CheckState.Checked;
            // 1) ��ü üũ�� ��� �߿���
            var checkedNodes = treeView
                .GetAllCheckedNodes()
                .OfType<DevExpress.XtraTreeList.Nodes.TreeListNode>();

            // 2) Leaf ���(�ڽ� ���� ���)�� ��󳻱�
            var checkedTags = checkedNodes
                .Where(n => !n.HasChildren)
                .Select(n => n.GetValue("Name").ToString())
                .ToList();

            // ���� checkedTags �� üũ�� TagName ����Ʈ�Դϴ�.
            // ��: ����� ���
            OwnerForm.CheckedTags = checkedTags;
            OwnerForm.AppendLog("üũ�� �±�: " + string.Join(", ", checkedTags));
        }
        public async void PopulateTreeSafeAsync()
        {
            // UI�� ǥ��
            BeginInvoke(new Action(() => ShowProgress()));

            if (Repository == null)
            {
                BeginInvoke(new Action(() =>
                    MessageBox.Show("Repository�� �������� �ʾҽ��ϴ�.", "����")));
                return;
            }

            // ������ �ε��� ��׶��忡�� ����
            var treeData = await Task.Run(() => LoadTreeData());

            // UI���� TreeList ����
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
        // UI Thread���� �����
        private void AddTreeNodes(TreeList treeView, List<TreeNodeData> nodes, TreeListNode parent = null)
        {
            foreach (var node in nodes)
            {
                // �ùٸ� AppendNode ���
                var newNode = treeView.AppendNode(new object[] { node.Text }, parent);
                // ������ �ε����� �Ӽ����� ����
                newNode.ImageIndex = node.IconIndex;
                newNode.SelectImageIndex = node.IconIndex;
                AddTreeNodes(treeView, node.Children, newNode);
            }
        }
        // Task.Run���� ȣ��� (UI ���� ���� �޸� ������ ����)
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

        // UI Thread���� �����

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
