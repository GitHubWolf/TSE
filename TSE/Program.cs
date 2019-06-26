using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Before start the main form, we will need to resolve some DLL loading issues.
            AppDomain appDomain = AppDomain.CurrentDomain;
            appDomain.AssemblyResolve += new ResolveEventHandler(DllResolveEventHandler);

            Application.Run(new FormMain());
        }

        static Assembly DllResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly assembly = null;
            string[] argSegments = args.Name.Split(',');
            if (argSegments[0] == "InActionLibrary")
            {
                string dllFileName = System.AppDomain.CurrentDomain.BaseDirectory + @"InActionLibrary.dll";
                assembly = Assembly.LoadFile(dllFileName);
            }
            else
            {
                assembly = null;
            }

            return assembly;
        }
    }
}
