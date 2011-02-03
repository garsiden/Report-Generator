// hide exception label if previous error is being displayed
function hideException(controlId) {
    var fullname = "ctl00_ContentPlaceHolder1_".concat(controlId);
    var label = document.getElementById(fullname);

    if (label != null) {
        label.innerText = "";
    }
}
