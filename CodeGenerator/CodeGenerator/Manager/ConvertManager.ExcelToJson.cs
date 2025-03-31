using CodeGenerator.InI;
using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator.Manager
{
    public struct ExcelField
    {
        public string name;
        public string value;
    }

    public partial class ConvertManager
    {
        public List<FileInfo> GetExcelFiles()
        {
            if (string.IsNullOrEmpty(InIManager.Instance.ExcelInputPath) == true)
                return null;

            DirectoryInfo directory = new DirectoryInfo(InIManager.Instance.ExcelInputPath);
            
            List<FileInfo> ret = new List<FileInfo>();
            foreach (var item in directory.GetFiles().ToList())
            {
                if (item.Name.Contains("~") == false)
                    ret.Add(item);
            }
            
            return ret;
        }

        public List<DataTable> ConvertExcelToJson(string targetPath, List<string> filters)
        {
            if (Directory.Exists(targetPath) == false)
                Directory.CreateDirectory(targetPath);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            List<DataTable> ret = new List<DataTable>();
            foreach (FileInfo fileInfo in GetExcelFiles())
            {
                if (filters.Contains(fileInfo.Name) == true)
                    continue;

                DataSet excelDataSet;
                using (FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    ExcelReaderConfiguration config = new ExcelReaderConfiguration()
                    {
                        FallbackEncoding = Encoding.Unicode
                    };
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream, config))
                    {
                        excelDataSet = reader.AsDataSet();
                        foreach (DataTable table in excelDataSet.Tables)
                        {
                            ret.Add(table);
                            CreateJson(targetPath, table);
                        }
                    }
                }
            }

            return ret;
        }

        private List<string> GetExcelDataNames(DataTable table, bool showVariable = false)         //데이터명
        {
            List<string> names = new List<string>();
            for (int i = 0; i < table.Columns.Count; i++) 
            {
                string key = table.Rows[0][i].ToString();
                if (showVariable == false)
                {
                    int idx = key.LastIndexOf(".");
                    if (idx >= 0)
                        key = key.Substring(idx + 1);
                }

                names.Add(key);
            }

            return names;
        }

        private Dictionary<int, List<ExcelField>> GetExcelData(DataTable table)
        {
            Dictionary<int, List<ExcelField>> dict = new Dictionary<int, List<ExcelField>>();
            
            List<string> names = GetExcelDataNames(table, true);
            for (int y = 1; y < table.Rows.Count; y++)
            {
                List<ExcelField> values = new List<ExcelField>();
                for (int x = 0; x < table.Columns.Count; x++)
                {
                    if (names[x].Contains("~") == true)
                        continue;

                    ExcelField excelField = new ExcelField();
                    excelField.name = names[x];
                    excelField.value = table.Rows[y][x].ToString();
                    values.Add(excelField);
                }

                dict.Add(y - 1, values);
            }

            return dict;
        }

        private void CreateJson(string targetPath, DataTable table)
        {
            int idx = table.TableName.IndexOf("_");
            string fileName = table.TableName.Substring(idx + 1);
            string directoryName = table.TableName.Substring(0, idx);

            string directory = targetPath + $@"\{directoryName}";
            string path = $@"{directory}\{fileName}.json";
            if (Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(path) == false)
            {
                File.Create(path).Close();
            }

            var datas = GetExcelData(table);
            string result = "";
            for (int i = 0; i < datas.Count; i++)
            {
                string jsonData = "";

                if (datas.TryGetValue(i, out var data) == false)
                    continue;

                for (int j = 0; j < data.Count; j++)
                {
                    var info = data[j];

                    int lastIdx = info.name.LastIndexOf(".");
                    string variable = "";
                    if (lastIdx >= 0)
                    {
                        variable = info.name.Substring(0, lastIdx);
                    }

                    info.name = info.name.Substring(lastIdx + 1);
                    jsonData += $"\t\t\t\"{info.name}\" : {GetValue(info.value, variable)}";

                    if (j + 1 < data.Count)
                    {
                        jsonData += ",\n";
                    }
                }

                result += string.Format(Define.JSON_FORMAT, jsonData);
            }

            string jsonFormatter =
@"[{0}
]";
            File.WriteAllText(path, string.Format(jsonFormatter, result));
        }

        string GetValue(string data, string variable)
        {
            int idx = data.LastIndexOf(".");
            string value = data;
            if (idx > 0)
            {
                value = value.Substring(idx);
            }

            if (variable == "string")
                return $"\"{value}\"";

            return value;
        }
    }
}