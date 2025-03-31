using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.InI
{
    public class InIManager
    {
        public static InIManager Instance { get; private set; } = new InIManager();

        #region InI Data
        public string ExcelInputPath { get { return GetValue(Define.INI_KEY.Excel, ""); } set { SetValue(Define.INI_KEY.Excel, value); } }
        public string ClientJsonOutputPath { get { return GetValue(Define.INI_KEY.ClientJson, ""); } set { SetValue(Define.INI_KEY.ClientJson, value); } }
        public string ServerJsonOutputPath { get { return GetValue(Define.INI_KEY.ClientSource, ""); } set { SetValue(Define.INI_KEY.ClientSource, value); } }
        public string ClientPacketOutputPath { get { return GetValue(Define.INI_KEY.ClientPacket, ""); } set { SetValue(Define.INI_KEY.ClientPacket, value); } }
        public string ClientSourceOutputPath { get { return GetValue(Define.INI_KEY.ServerJson, ""); } set { SetValue(Define.INI_KEY.ServerJson, value); } }
        public string ServerPacketOutputPath { get { return GetValue(Define.INI_KEY.ServerSource, ""); } set { SetValue(Define.INI_KEY.ServerSource, value); } }
        public string ServerSourceOutputPath { get { return GetValue(Define.INI_KEY.ServerPacket, ""); } set { SetValue(Define.INI_KEY.ServerPacket, value); } }
        public bool CheckBuildClientJson
        {
            get
            {
                return GetValue(Define.INI_KEY.CheckBuildClientJson, "0") == "1";
            }
            set
            {
                string result = "0";
                if (value == true)
                    result = "1";

                SetValue(Define.INI_KEY.CheckBuildClientJson, result);
            }
        }
        public bool CheckBuildServerJson
        {
            get
            {
                return GetValue(Define.INI_KEY.CheckBuildServerJson, "0") == "1";
            }
            set
            {
                string result = "0";
                if (value == true)
                    result = "1";

                SetValue(Define.INI_KEY.CheckBuildServerJson, result);
            }
        }
        #endregion

        public InIManager()
        {
            if (File.Exists(GetIniPath) == false)
            {
                FileStream buffer = File.Create(GetIniPath);
                buffer.Close();
            }
        }

        public string GetIniPath
        {
            get
            {
                string path = Application.StartupPath + @"\config.ini";
                return path;
            }
        }

        private const string SECTION = "SYSTEM";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public void SetValue(Define.INI_KEY key, string value)
        {
            WritePrivateProfileString(SECTION, key.ToString(), value, GetIniPath);
        }

        public string GetValue(Define.INI_KEY key, string defaultValue)
        {
            int bufferSize = 255;
            StringBuilder stringBuilder = new StringBuilder(bufferSize);
            int size = GetPrivateProfileString(SECTION, key.ToString(), defaultValue, stringBuilder, bufferSize, GetIniPath);
            
            if (stringBuilder != null && bufferSize > 0)
            {
                return stringBuilder.ToString();
            }

            return defaultValue;
        }
    }
}
