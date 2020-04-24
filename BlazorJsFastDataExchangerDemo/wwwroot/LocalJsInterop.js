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
};
