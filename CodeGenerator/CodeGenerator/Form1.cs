using CodeGenerator.InI;
using System.Windows.Forms;
using CodeGenerator.Manager;
using System.Data;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace CodeGenerator
{
    public partial class Form1 : Form
    {
        private List<string> _filters = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            excelInputPathBox.Text = InIManager.Instance.ExcelInputPath;
            clientJsonOutputPathBox.Text = InIManager.Instance.ClientJsonOutputPath;
            clientSourceOutputPathBox.Text = InIManager.Instance.ClientSourceOutputPath;
            clientPacketOutputPathBox.Text = InIManager.Instance.ClientPacketOutputPath;
            serverJsonOutputPathBox.Text = InIManager.Instance.ServerJsonOutputPath;
            serverSourceOutputPathBox.Text = InIManager.Instance.ServerSourceOutputPath;
            serverPacketOutputPathBox.Text = InIManager.Instance.ServerPacketOutputPath;
            clientJsonCheckBox.Checked = InIManager.Instance.CheckBuildClientJson;
            serverJsonCheckBox.Checked = InIManager.Instance.CheckBuildServerJson;

            var fileInfos = ConvertManager.Instance.GetExcelFiles();
            if (fileInfos == null || fileInfos.Count == 0)
                return;

            excelListViewer.Items.Clear();
            foreach (var fileInfo in ConvertManager.Instance.GetExcelFiles())
            {
                excelListViewer.Items.Add(fileInfo.Name);
            }

            for (int i = 0; i < excelListViewer.Items.Count; i++)
            {
                excelListViewer.SetItemChecked(i, true);
            }
        }

        #region Input Data Events

        private void OnExcelTextBoxEvent(object sender, EventArgs e)
        {
            InIManager.Instance.ExcelInputPath = excelInputPathBox.Text;
        }

        private void OnClientJsonCheckBoxEvent(object sender, EventArgs e)
        {
            InIManager.Instance.CheckBuildClientJson = clientJsonCheckBox.Checked;
        }

        private void OnClientJsonOutputPathBoxEvent(object sender, EventArgs e)
        {

            InIManager.Instance.ClientJsonOutputPath = clientJsonOutputPathBox.Text;
        }

        private void OnClientSourceOutputPathBoxEvent(object sender, EventArgs e)
        {
            InIManager.Instance.ClientSourceOutputPath = clientSourceOutputPathBox.Text;
        }

        private void OnClientPacketOutputPathBoxEvent(object sender, EventArgs e)
        {
            InIManager.Instance.ClientPacketOutputPath = clientPacketOutputPathBox.Text;
        }

        private void OnServerJsonOutputPathBoxEvent(object sender, EventArgs e)
        {
            InIManager.Instance.ServerJsonOutputPath = serverJsonOutputPathBox.Text;
        }

        private void OnServerJsonCheckBoxEvent(object sender, EventArgs e)
        {
            InIManager.Instance.CheckBuildServerJson = serverJsonCheckBox.Checked;
        }

        private void OnServerSourceOutputPathBoxEvent(object sender, EventArgs e)
        {
            InIManager.Instance.ServerSourceOutputPath = serverSourceOutputPathBox.Text;
        }

        private void OnServerPacketOutputPathBoxEvent(object sender, EventArgs e)
        {
            InIManager.Instance.ServerPacketOutputPath = serverPacketOutputPathBox.Text;
        }
        #endregion

        private void OnClickBuildButton(object sender, EventArgs e)
        {
            if (InIManager.Instance.CheckBuildClientJson == false)
            {
                return;
            }

            List<DataTable> jsonFiles = ConvertManager.Instance.ConvertExcelToJson(InIManager.Instance.ClientJsonOutputPath, _filters);
            List<(string, string)> clientDataFiles = ConvertManager.Instance.GenerateDataCodeByClient(InIManager.Instance.ClientSourceOutputPath, jsonFiles);
            ConvertManager.Instance.CreateDataFormmatByClient(InIManager.Instance.ClientSourceOutputPath, clientDataFiles);

            //TODO 서버 코드 생성기도 추가하기
            //if (InIManager.Instance.CheckBuildServerJson == true)
            //{
            //    jsonFiles = ConvertManager.Instance.ConvertExcelToJson(InIManager.Instance.ServerJsonOutputPath);
            //}

            MessageBox.Show("생성 완료", "Code Generator", MessageBoxButtons.OK);
        }

        private void OnClickGeneratePacketButton(object sender, EventArgs e)
        {

        }

        private void OnClickShowExcelListButton(object sender, EventArgs e)
        {
            excelListViewer.Items.Clear();
            foreach (var fileInfo in ConvertManager.Instance.GetExcelFiles())
            {
                excelListViewer.Items.Add(fileInfo.Name);
            }

            for (int i = 0; i < excelListViewer.Items.Count; i++)
            {
                excelListViewer.SetItemChecked(i, true);
            }
        }

        private void OnClickExcelListViewerSelectedIndexChanged(object sender, EventArgs e)
        {
            ItemCheckEventArgs arg = e as ItemCheckEventArgs;

            string itemText = excelListViewer.Items[arg.Index].ToString();
            if (arg.NewValue == CheckState.Unchecked)
            {
                AddFilterItem(itemText);
            }
            else if (arg.NewValue == CheckState.Checked)
            {
                RemoveFilterItem(itemText);
            }
        }

        void AddFilterItem(string value)
        {
            _filters.Add(value);
        }

        void RemoveFilterItem(string value)
        {
            _filters.Remove(value);
        }
    }
}
