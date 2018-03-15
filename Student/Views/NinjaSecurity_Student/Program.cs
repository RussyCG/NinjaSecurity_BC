using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace NinjaSecurity_Student
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.AppSettings.InitialiseSettings();

            while (true)
            {
                new Sender().SendData(Console.ReadLine());
            }
        }
    }
}
