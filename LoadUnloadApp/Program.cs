using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace LoadUnloadApp
{
    class Program
    {
        static void Main()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            DirectoryInfo dir = new DirectoryInfo(baseDir);
            while (dir.GetFiles("*.sln").Count() <= 0)
            {
                dir = dir.Parent;
            }
            string configMgr = "Debug";
#if !DEBUG
            configMgr = "Release";
#endif
            // Specify the path to the DLL
            string dllDirectory = $"{Path.Combine(dir.FullName, "SampleDll\\bin\\", configMgr)}"; // Update this path to your DLL directory
            string dllPath = Path.Combine(dllDirectory, "SampleDll.dll");
            Console.WriteLine($"Dll need to unload path :: {dllPath}");
            if (!File.Exists(dllPath))
            {
                Console.WriteLine("DLL not found: " + dllPath);
                return;
            }

            // Create a new application domain
            AppDomainSetup setup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory, // Set the base directory to the DLL's location
                PrivateBinPath = AppDomain.CurrentDomain.BaseDirectory // Set the private binary path
            };

            AppDomain newDomain = AppDomain.CreateDomain("NewDomain", null, setup);

            try
            {
                // Use a proxy to call methods in the new AppDomain
                var proxy = (DomainProxy)newDomain.CreateInstanceAndUnwrap(typeof(DomainProxy).Assembly.FullName, typeof(DomainProxy).FullName);
                ModelV1 model = new ModelV1()
                {
                    Name = $"{Guid.NewGuid()} :: Hello World"
                };
                proxy.ExecuteInNewDomain(dllPath, model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                // Unload the application domain
                AppDomain.Unload(newDomain);
                Console.WriteLine("Assembly unloaded.");
            }
            Application.Run();
        }
    }
}
