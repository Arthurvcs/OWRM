$(function () {
    $("#datepickerIni").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#datepickerFim").datepicker({ dateFormat: 'dd/mm/yy' });

});

function ExportarExcel() {
    var a = document.createElement('a');
    var data_type = 'data:application/vnd.ms-excel';
    var table_div = document.getElementById('dvData');
    var table_html = table_div.outerHTML.replace(/ /g, '%20');
    a.href = data_type + ', ' + table_html;
    a.download = 'Pontos.xls';
    a.click();
    e.preventDefault();
}
