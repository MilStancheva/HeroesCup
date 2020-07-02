$(function () {
    var  config;
    config = {
        format: 'dd mm yyyy'
    };

    $("#start_date").datepicker(config);
    $("#end_date").datepicker(config);
});