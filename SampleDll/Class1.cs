using Common;
using System;

namespace SampleLibrary
{
    public class SampleClass : IDisposable
    {
        public void Dispose()
        {
        }

        public string GetMessage(ModelV1 modelV1)
        {
            //return "Hello from SampleLibrary!";
            return $"{DateTime.Now} :: {modelV1.Name}";
        }
    }
}
