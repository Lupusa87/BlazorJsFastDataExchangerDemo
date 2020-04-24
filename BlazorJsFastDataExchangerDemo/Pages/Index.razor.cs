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
    public partial class Index
    {

        [Inject] IJSRuntime jsRuntime { get; set; }

   
        LocalJsInterop _LocalJsInterop;

        protected int TransportCode = 0;

        public bool IsDisabled = false;

        protected string JsMessage = "fastinterop";

        protected List<string> log = new List<string>();

       

        protected override void OnInitialized()
        {

            _LocalJsInterop = new LocalJsInterop(jsRuntime);
            BWHJsInterop.jsRuntime = jsRuntime;


            base.OnInitialized();
        }



        public async void JsSendMessage()
        {



            if (!string.IsNullOrEmpty(JsMessage))
            {

                ExpandData();

                log.Add(JsMessage);


                BlazorTimeAnalyzer.Reset();
                BlazorTimeAnalyzer.Add("A1", MethodBase.GetCurrentMethod());
                await _LocalJsInterop.SetData("myTmpVar1",JsMessage);
               
                BlazorTimeAnalyzer.Add("A2", MethodBase.GetCurrentMethod());
                _LocalJsInterop.ProcessData("myTmpVar1");


                BlazorTimeAnalyzer.Add("A3", MethodBase.GetCurrentMethod());
                log.Add(await _LocalJsInterop.GetData("myTmpVar1"));

                BlazorTimeAnalyzer.LogAll();


                JsMessage = string.Empty;

            }
            else
            {
                await _LocalJsInterop.Alert("Please input message");
            }


            StateHasChanged();
        }


        public async void JsSendMessageFast()
        {



            if (!string.IsNullOrEmpty(JsMessage))
            {

                ExpandData();

                log.Add(JsMessage);

              
                BlazorTimeAnalyzer.Reset();
                BlazorTimeAnalyzer.Add("A1", MethodBase.GetCurrentMethod());
                JsFastDataExchanger.SetData("myTmpVar1",JsMessage);
             

                 BlazorTimeAnalyzer.Add("A2", MethodBase.GetCurrentMethod());
                 _LocalJsInterop.ProcessData("myTmpVar1");


                BlazorTimeAnalyzer.Add("A3", MethodBase.GetCurrentMethod());

                log.Add(JsFastDataExchanger.GetData("myTmpVar1"));

                BlazorTimeAnalyzer.LogAll();

                JsMessage = string.Empty;

            }
            else
            {
                await _LocalJsInterop.Alert("Please input message");
            }


            StateHasChanged();
        }


        public void ExpandData()
        {
            StringBuilder sb = new StringBuilder();

       
            for (int i = 0; i < 100000; i++)
            {
                sb.Append(JsMessage);
            }


            JsMessage = sb.ToString();
           
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
