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

        internal void ProcessData(string variableName)
        {
            _JSRuntime.InvokeVoidAsync("LocalJsFunctions.ProcessData", variableName);
        }


        internal ValueTask<bool> SetData(string variableName, string t)
        {
            return _JSRuntime.InvokeAsync<bool>("LocalJsFunctions.SetData", variableName, t );
        }

        internal ValueTask<string> GetData(string variableName)
        {
            return _JSRuntime.InvokeAsync<string>("LocalJsFunctions.GetData", variableName);
        }
    }
}

