window.downloadCSV = function (filename, base64Data) {
    var a = document.createElement('a');
    a.href = 'data:text/csv;base64,' + base64Data;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}