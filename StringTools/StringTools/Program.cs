using CodeGenerator.Manager;
using System.Data;

namespace StringTools
{
    public class Program
    {
        private const string JSON_OUTPUT_PATH = "DatingSimulations/Assets/Addressables/Data/";

        static void Main(string[] args)
        {
            List<DataTable> dataTables = ConvertManager.Instance.ConvertExcelToJson(JSON_OUTPUT_PATH, args[0]);
        }
    }
}
