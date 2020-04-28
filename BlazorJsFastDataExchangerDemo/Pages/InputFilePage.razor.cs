using BlazorJsFastDataExchanger;
using BlazorWindowHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorJsFastDataExchangerDemo.Pages
{
    public partial class InputFilePage
    {

        [Inject] IJSRuntime jsRuntime { get; set; }


        LocalJsInterop _LocalJsInterop;

      
        public bool IsDisabled = false;

        protected List<string> log = new List<string>();

        private BinaryInfo _BinaryInfo = new BinaryInfo("myTmpVar1");



        int LastPrintedPercent;

        protected override void OnInitialized()
        {

            _LocalJsInterop = new LocalJsInterop(jsRuntime);
            BWHJsInterop.jsRuntime = jsRuntime;

            JsFastDataExchanger.OnMessage = JsFastDataExchanger_OnMessage;
            JsFastDataExchanger.OnProgress = JsFastDataExchanger_OnProgress;




            base.OnInitialized();
        }



        public void ResetBinaryInfo()
        {
           _BinaryInfo = new BinaryInfo("myTmpVar1");
            _BinaryInfo.OnDataRead = _BinaryInfo_OnDataRead;
            _BinaryInfo.OnFinish = _BinaryInfo_OnFinish;
            log.Clear();
        }


        public void _BinaryInfo_OnDataRead(int percent)
        {
            if (percent % 20==0)
            {
                log.Add(_BinaryInfo.progressInfo);
            }

            StateHasChanged();
        }

        public async void JsFastDataExchanger_OnMessage(string msg)
        {
           

            if (msg.Equals("fileloadingdone"))
            {

                log.Add("js loaded " + _BinaryInfo.dataLenght + " bytes");
                await InvokeAsync(StateHasChanged);

                BlazorTimeAnalyzer.Add("reading in .net", MethodBase.GetCurrentMethod());
                await _BinaryInfo.LoadAsync();

            }
        }

        public async void JsFastDataExchanger_OnProgress(string msg)
        {

            string[] args = msg.Split(',');

            if (args[0].Equals("loadprogress"))
            {

                int progress = 0;

                if (!int.TryParse(args[1], out progress))
                {
                    throw new Exception("there is not provided progress value or is in wrong format!");
                }

                if (_BinaryInfo.dataLenght == -1)
                {
                    int total = 0;
                    if (!int.TryParse(args[2], out total))
                    {
                        throw new Exception("there is not provided total value or is in wrong format!");
                    }
                    _BinaryInfo.dataLenght = total;
                }

                double p = progress * 100.0 / _BinaryInfo.dataLenght;
                _BinaryInfo.progressInfo = "js loaded " + (int)p + " from 100%";

                int t = (int)(p / 20);
                if (LastPrintedPercent<t)
                {
                   
                    log.Add("js loaded " + t*20 + " from 100%");
                    LastPrintedPercent = t;
                }

                await InvokeAsync(StateHasChanged);
            }
        }

        private void _BinaryInfo_OnFinish()
        {
            _BinaryInfo.progressInfo = "done";
            log.Add(".net loaded " + _BinaryInfo.dataLenght + " bytes");
            log.Add("done");
            BlazorTimeAnalyzer.LogAll();

            StateHasChanged();
        }

        public async void loadFileRegular()
        {
            log.Clear();

            if (await _LocalJsInterop.HasFile("fileUpload"))
            {

                BlazorTimeAnalyzer.Reset("Regular mode");

                log.Add("started");
                log.Add("js is loading file...");
                StateHasChanged();

                BlazorTimeAnalyzer.Add("A1", MethodBase.GetCurrentMethod());

                string a = await _LocalJsInterop.GetFile(_BinaryInfo.variableName, "fileUpload");

                
                log.Add(".net loaded " + a.Length + " bytes");
                log.Add("done");

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
            ResetBinaryInfo();

            if (await _LocalJsInterop.HasFile("fileUpload"))
            {


                _BinaryInfo.progressInfo ="started";
                log.Add("started");
                BlazorTimeAnalyzer.Reset("Fast mode");
                BlazorTimeAnalyzer.Add("reading in js", MethodBase.GetCurrentMethod());

                _BinaryInfo.progressInfo = "js is loading file...";
                log.Add("js is loading file...");
                StateHasChanged();

                await _LocalJsInterop.ReadFile(_BinaryInfo.variableName, "fileUpload");
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
