using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BlazorJsFastDataExchangerDemo
{
    public class LocalJsInterop
    {
        private readonly IJSRuntime _JSRuntime;

        internal LocalJsInterop(IJSRuntime jsRuntime)
        {
            _JSRuntime = jsRuntime;
        }


        internal ValueTask<string> Alert(string message)
        {
            return _JSRuntime.InvokeAsync<string>(
                "LocalJsFunctions.Alert", message);
        }

       

        internal void ProcessGlobalExchangeData(string variableName)
        {
            _JSRuntime.InvokeVoidAsync("LocalJsFunctions.ProcessGlobalExchangeData", variableName);
        }


        internal ValueTask<bool> SendData(string variableName, string t)
        {
            return _JSRuntime.InvokeAsync<bool>("LocalJsFunctions.SendData", variableName, t );
        }

        internal ValueTask<string> ReadData(string variableName)
        {
            return _JSRuntime.InvokeAsync<string>("LocalJsFunctions.ReadData", variableName);
        }
    }
}

