using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSecurity_Student
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                new Sender().SendData(Console.ReadLine());
            }
        }
    }
}
