using BlazorCounterHelper;
using BlazorWindowHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorJsFastDataExchangerDemo.Shared
{
    public partial class MainLayout
    {
        //[Inject]
        //HttpClient httpClient { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject]
        IJSRuntime jsRuntime { get; set; }


        protected override async Task OnInitializedAsync()
        {


            BWHJsInterop.jsRuntime = jsRuntime;

            CounterHelper.Initialize();
            await CounterHelper.CmdAddCounter(new TSCounter() { Source = navigationManager.Uri, Action = "visit" });

            await base.OnInitializedAsync();

            return;
        }

        protected override void OnAfterRender(bool firstRender)
        {

            if (firstRender)
            {
                BWHelperFunctions.CheckIfMobile();
            }

            base.OnAfterRender(firstRender);
        }


    }
}
