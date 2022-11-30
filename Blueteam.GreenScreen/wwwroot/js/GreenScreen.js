// GreenScreen is a component that a page overlay allows your monitor to consume less energy when idle
// Copyright(C) 2022 BlueTeam OÜ (14454093) info@blueteam.ee
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Lesser General Public License v3.0 or later <https://www.gnu.org/licenses/licenses.html#LGPL>.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

// SPDX-License-Identifier: LGPL-3.0-or-later

window.GreenScreen = {
    timer: null,
    timing: null,
    active: null,
    dnHelper:null,
    initialize: (dotnetHelper, iTimeoutLength) => {
        active = 1;
        dnHelper = dotnetHelper;
        timing = iTimeoutLength;
        console.log("GreenScreen.initialize(" + timing.toString() + ")");
        GreenScreen.addListeners();
        document.addEventListener("mousemove", GreenScreen.reset);
        timer = setTimeout(GreenScreen.timeout, timing);
    },
    reset: () => {
        console.log("GreenScreen.reset(" + timing.toString() + ")");
        if (active === 0) {
            document.getElementById("greenscreen").style.width = "0%";
            document.getElementsByTagName("HTML")[0].style.overflowY  = "auto";
            document.getElementById("greenscreen").setAttribute("role", "");
            document.getElementById("greenscreen").blur();
            document.addEventListener("mousemove", GreenScreen.reset);
            active = 1;
        };
        clearTimeout(timer);
        timer = setTimeout(GreenScreen.timeout, timing);
    },
    timeout: () => {
        console.log("GreenScreen.timeout()");
        if (active === 1) {
            document.getElementById("greenscreen").style.width = "100%";
            document.getElementsByTagName("HTML")[0].style.overflowY = "hidden";
            document.getElementById("greenscreen").setAttribute("role", "alert");
            document.getElementById("greenscreen").focus();
            document.removeEventListener("mousemove", GreenScreen.reset);
            active = 0;
        };
        clearTimeout(timer);
        timer = null;
        dnHelper.invokeMethodAsync("ScreenTimeout");
    },
    addListeners: () => {
        document.addEventListener("keypress", GreenScreen.reset);
        document.addEventListener("keydown", GreenScreen.reset);
        document.addEventListener("mousedown", GreenScreen.reset);
        document.addEventListener("touchstart", GreenScreen.reset);
        document.addEventListener("click", GreenScreen.reset);
        document.addEventListener("scroll", GreenScreen.reset);
    },
    removeListeners: () => {
        document.removeEventListener("scroll", GreenScreen.reset);
        document.removeEventListener("click", GreenScreen.reset);
        document.removeEventListener("touchstart", GreenScreen.reset);
        document.removeEventListener("mousedown", GreenScreen.reset);
        document.removeEventListener("keydown", GreenScreen.reset);
        document.removeEventListener("keypress", GreenScreen.reset);
    },
    disposeHelper: () => {
        console.log("GreenScreen.disposeHelper()");
        document.removeEventListener("mousemove", GreenScreen.reset);
        GreenScreen.removeListeners();
        dnHelper.dispose();
    }
}
