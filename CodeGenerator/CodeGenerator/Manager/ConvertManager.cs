using CodeGenerator.InI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Manager
{
    public partial class ConvertManager
    {
        public static ConvertManager Instance { get; private set; } = new ConvertManager();
    }
}
