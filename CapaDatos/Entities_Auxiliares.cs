using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Entities_Auxiliares
    {

    }

    public class FileLogger
    {
        public string filePath = "..\\..\\..\\LogAcceso.txt";
        public void Log(string message)
        {
            if (File.Exists(filePath) == false)
            {
                StreamWriter sw = File.CreateText(filePath);
                sw.WriteLine(message);
                sw.Close();
            }
            else
            {
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine(message);
                sw.Close();
            }
        }
    }
}
