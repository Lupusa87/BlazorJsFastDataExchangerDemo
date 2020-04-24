
window.LocalJsFunctions = {
    Alert: function (message) {
        return alert(message);
    },
    SendData: function (v, t) {
        this[v] = t;
        return true;
    },
    ReadData: function (v) {
        var result = this[v];
        //this[v] = null;
        delete this[v];
        return result;
    },
    ProcessGlobalExchangeData: function (v) {
        this[v] = this[v].split("").reverse().join("");
    },
};
