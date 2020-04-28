﻿function getArrayBufferFromFileAsync(file) {
    const reader = new FileReader();
    return new Promise((resolve, reject) => {
        reader.onload = function () { resolve(reader.result); };
        reader.onprogress = function (e) {
            window.BJSFDEJsFunctions.InvokeOnProgressAction("loadprogress," + e.loaded + "," + e.total);
        };
        reader.onerror = function (err) { reject(err); };
        reader.readAsArrayBuffer(file);
    });
}

window.LocalJsFunctions = {
    Alert: function (message) {
        return alert(message);
    },
    SetData: function (v, t) {
        this[v] = t;
        return true;
    },
    GetData: function (v) {
        var result = this[v];
        //this[v] = null;
        delete this[v];
        return result;
    },
    ProcessData: function (v) {
        this[v] = this[v].split("").reverse().join("");
    },
    HasFile: async function (inputFile) {
        return document.getElementById(inputFile).files.length>0;
    },
    ReadFile: async function (v, inputFile) {

        window[v] = await getArrayBufferFromFileAsync(document.getElementById(inputFile).files[0]);
       
        //window.BJSFDEJsFunctions.CallDotNetMethod("a", "a", "a", "a", "a");
        window.BJSFDEJsFunctions.InvokeOnMessageAction("fileloadingdone");
        return true;
    },
    GetFile: async function (v, inputFile) {

        var a = await getArrayBufferFromFileAsync(document.getElementById(inputFile).files[0]);
      
        return new TextDecoder().decode(a);
    },
};
