
window.LocalJsFunctions = {
    Alert: function (message) {
        return alert(message);
    },
    SendData: function (t) {
        GlobalExchangeData = t;

        return true;
    },
    ReadData: function () {
        return GlobalExchangeData;
    },
    ProcessGlobalExchangeData: function () {
        GlobalExchangeData = GlobalExchangeData.split("").reverse().join("");
    },
};
