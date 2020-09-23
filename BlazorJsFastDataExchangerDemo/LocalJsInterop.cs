using Microsoft.JSInterop;
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


        internal async ValueTask<bool> HasFile(string inputFileElementID)
        {
            return await _JSRuntime.InvokeAsync<bool>("LocalJsFunctions.HasFile", inputFileElementID);

        }

        internal async ValueTask<bool> ReadFile(string variableName, string inputFileElementID)
        {
            return await _JSRuntime.InvokeAsync<bool>("LocalJsFunctions.ReadFile", variableName, inputFileElementID);

        }

        internal async ValueTask<string> GetFile(string variableName, string inputFileElementID)
        {
            return await _JSRuntime.InvokeAsync<string>("LocalJsFunctions.GetFile", variableName, inputFileElementID);

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

