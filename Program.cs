using DotNetSiemensPLCToolBoxLibrary.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Read_Data_from_SiemensS7
{
    class Program
    {
        static void Main(string[] args)
        {
            SiemensS7Hander s7handler = new SiemensS7Hander();
            s7handler.ReadTagList();
            Console.ReadKey();
        }

    }
}
