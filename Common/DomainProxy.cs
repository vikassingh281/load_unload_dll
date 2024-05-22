using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    // Proxy class to handle assembly loading and method invocation in the new AppDomain
    public class DomainProxy : MarshalByRefObject
    {
        public void ExecuteInNewDomain(string dllPath, ModelV1 model)
        {
            try
            {
                // Load the assembly
                Assembly assembly = Assembly.LoadFrom(dllPath);

                // Get the type from the assembly
                Type type = assembly.GetType("SampleLibrary.SampleClass");

                // Create an instance of the type
                object instance = Activator.CreateInstance(type);
                Console.WriteLine($"Data sent :: {DateTime.Now} :: {model.Name}");
                // Call the method
                MethodInfo method = type.GetMethod("GetMessage");
                string result = (string)method.Invoke(instance, new object[] { model });
                Console.WriteLine($"Data recieved :: {result}"); // Output: Hello from SampleLibrary!
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in new AppDomain: " + ex.Message);
            }
        }
    }

}
