// GreenScreen is a component that a page overlay allows your monitor to consume less energy when idle
// Copyright(C) 2022 BlueTeam OÜ (14454093) info@blueteam.ee
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Lesser General Public License v3.0 or later <https://www.gnu.org/licenses/licenses.html#LGPL>.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

// SPDX-License-Identifier: LGPL-3.0-or-later

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blueteam.GreenScreen
{
    public partial class GreenScreen : ComponentBase, IDisposable
    {
        [Inject] public IJSRuntime JavaScriptEngine { get; set; }
        [Parameter] public TimeOnly Timeout { get; set; }
        
        [Parameter] public string LogoPath { get; set; }
        [Parameter] public string TextHeader { get; set; }
        [Parameter] public string TextExplanation { get; set; }
        [Parameter] public string TextToResume { get; set; }

        /// <summary>
        /// This event is raised when the energy saving page overlay activates.
        /// </summary>
        [Parameter] public EventCallback ScreenActivated { get; set; }

        private DotNetObjectReference<GreenScreen> _objectReference;

        public GreenScreen()
        {
            Timeout = new TimeOnly(0,0,10);
            LogoPath = @"_content/Blueteam.GreenScreen/images/logo.svg";
            TextHeader = "RESPECT THE ENVIRONMENT, EVEN WITH A SMALL ACTION";
            TextExplanation = "This screen allows your monitor to consume less power when your computer is idle or when you walk away.";
            TextToResume = "To resume browsing, click anywhere on the screen.";
        }

        protected override async Task OnAfterRenderAsync(bool bFirstRender)
        {
            await base.OnAfterRenderAsync(bFirstRender);
            if (bFirstRender)
            {
                _objectReference = DotNetObjectReference.Create(this);
                await InitializeGreenScreenJS(_objectReference, Timeout.Ticks / TimeSpan.TicksPerMillisecond);
                //await JavaScriptEngine.ResetInactivityTimer();

                StateHasChanged();
            }
        }

        // Callback for when the user has been idle too long...
        [JSInvokable]
        public Task ScreenTimeout()
        {
            return ScreenActivated.InvokeAsync();
        }

        public void Dispose()
        {
            DisposeGreenScreenJS();
            _objectReference?.Dispose();
            GC.SuppressFinalize(this);
        }


        public async ValueTask InitializeGreenScreenJS<T>(
            DotNetObjectReference<T> dotNetObjectReference, long timeoutInSeconds) where T : class
        {
            await JavaScriptEngine.InvokeVoidAsync("GreenScreen.initialize", dotNetObjectReference, timeoutInSeconds);
        }

        public async ValueTask ResetGreenScreenJS()
        {
            await JavaScriptEngine.InvokeVoidAsync("GreenScreen.reset");
        }

        public ValueTask DisposeGreenScreenJS()
        {
            return JavaScriptEngine.InvokeVoidAsync("GreenScreen.disposeHelper");
        }

    }
}
