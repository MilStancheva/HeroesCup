$(function () {
    $("#start_date").datepicker({
        autoclose: true,
        todayHighlight: true
    }).datepicker('update', new Date());

    $("#end_date").datepicker({
        autoclose: true,
        todayHighlight: true
    }).datepicker('update', new Date());
});