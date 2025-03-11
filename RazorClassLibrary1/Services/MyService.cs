using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary1.Services
{
    interface IMyService {
        void log();
    }

    public class MyService : IMyService
    {
        public void log()
        {
            //Console.WriteLine("My debug output.");
            System.Diagnostics.Debug.WriteLine("This is a log");
        }
    }
}
