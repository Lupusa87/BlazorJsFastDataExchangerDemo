using BlazorJsFastDataExchanger;
using BlazorWindowHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorJsFastDataExchangerDemo.Pages
{
    public partial class InputFilePage
    {

        [Inject] IJSRuntime jsRuntime { get; set; }


        LocalJsInterop _LocalJsInterop;


        public bool IsDisabled = false;

        protected List<string> log = new List<string>();



        protected override void OnInitialized()
        {

            _LocalJsInterop = new LocalJsInterop(jsRuntime);
            BWHJsInterop.jsRuntime = jsRuntime;


            base.OnInitialized();
        }


        public async void loadFileRegular()
        {


            if (await _LocalJsInterop.HasFile("fileUpload"))
            {

                BlazorTimeAnalyzer.Reset("Regular mode");

                BlazorTimeAnalyzer.Add("A1", MethodBase.GetCurrentMethod());

                log.Add(await _LocalJsInterop.GetFile("myTmpVar1", "fileUpload"));

                BlazorTimeAnalyzer.LogAll();
                StateHasChanged();
            }
            else
            {
                await _LocalJsInterop.Alert("Please select file");
            }

           
        }

        public async void loadFileFast()
        {


            if (await _LocalJsInterop.HasFile("fileUpload"))
            {
                BlazorTimeAnalyzer.Reset("Fast mode");
                BlazorTimeAnalyzer.Add("A1", MethodBase.GetCurrentMethod());

                await _LocalJsInterop.ReadFile("myTmpVar1", "fileUpload");


                BlazorTimeAnalyzer.Add("A2", MethodBase.GetCurrentMethod());


                int l = JsFastDataExchanger.GetBinaryDataLenght("myTmpVar1");

                byte[] b = new byte[l];

                BlazorTimeAnalyzer.Add("A3", MethodBase.GetCurrentMethod());
                JsFastDataExchanger.GetBinaryData("myTmpVar1", b);


                log.Add(Encoding.UTF8.GetString(b));

                BlazorTimeAnalyzer.LogAll();
                StateHasChanged();
            }
            else
            {
                await _LocalJsInterop.Alert("Please select file");
            }

        }

     
        public void ClearLog()
        {
            if (log.Any())
            {
                log = new List<string>();

            }
        }
    }


}
