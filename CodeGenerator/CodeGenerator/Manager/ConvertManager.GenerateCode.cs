using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Manager
{
    public partial class ConvertManager
    {
        private List<string> _dataClassName = new List<string>();

        public void CreateDataFormmatByClient(string targetPath, List<(string directory, string file)> clientDataFiles)
        {
            string fields = "";
            string loaders = "";
            string clears = "";
            string binaryConverter = "";

            foreach ((string directory, string file) info in clientDataFiles)
            {
                string className = info.file + "Script";
                string fieldName = "Get" + className + "s";
                fields += $"\tpublic List<{className}> {fieldName} " + "{ get; private set; }\n";

                string loader = string.Format(Define.DATA_LOAD_FORMAT, fieldName, className, $"{info.directory}\", \"{info.file}");
                
                loaders += loader + "\n";
                binaryConverter += "\t\t" + className + "Loader.ConvertBinary();\n";
                
                clears += string.Format("\t\t{0}.Clear();\n", fieldName);
            }

            string fullPath = $"{targetPath}\\DataManager.Loader.cs";
            if (Directory.Exists(targetPath) == false)
                Directory.CreateDirectory(targetPath);

            File.Create(fullPath).Close();
            File.WriteAllText(fullPath, string.Format(Define.DATA_MANAGER_FORMAT, fields, loaders, binaryConverter, clears));
        }

        public List<(string, string)> GenerateDataCodeByClient(string targetPath, List<DataTable> tables)
        {
            List<(string, string)> ret = new List<(string, string)>();
            if (targetPath == null)
                return ret;

            if (Directory.Exists(targetPath) == true)
                Directory.Delete(targetPath, true);

            foreach (DataTable table in tables)
            {
                int idx = table.TableName.IndexOf("_");
                string directoryName = table.TableName.Substring(0, idx);
                string className = table.TableName.Substring(idx + 1);
                string fullPath = $@"{targetPath}\{directoryName}\Data.Content.{className}.cs";
                string result = string.Format(Define.CLIENT_DATA_CONTENT_FORMAT, className + "Script", GetFields(table), $@"Assets/AssetBundles/Data/{directoryName}/{className}");

                string directoryPath = targetPath + @$"\{directoryName}";
                if (Directory.Exists(directoryPath) == false)
                    Directory.CreateDirectory(directoryPath);
                
                ret.Add((directoryName, className));
                File.WriteAllText(fullPath, result);
            }

            return ret;
        }

        private string GetFields(DataTable table)
        {
            List<string> fields = GetExcelDataNames(table, true);
            string field = "";

            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].Contains("~") == true)
                    continue;

                string info = fields[i];
                int splitIdx = info.LastIndexOf(".");
                
                string variable = "int";
                string variableName = info;
                if (splitIdx >= 0)
                {
                    variableName = info.Substring(splitIdx + 1);
                    variable = info.Substring(0, splitIdx);
                }
                
                string result = string.Format(Define.DATA_MANAGER_FIELD_FORMAT, variable, variableName);
                if (i + 1 < fields.Count)
                    result += "\n";

                field += result;
            }

            return field;
        }
    }
}
