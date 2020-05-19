$(function () {
    var start_date_datepicker, end_date_datepicker, config;
    config = {
        locale: 'bg-bg',
        uiLibrary: 'bootstrap4',
        format: 'dd/mm/yyyy'
    };

    start_date_datepicker = $("#start_date").datepicker(config);
    end_date_datepicker = $("#end_date").datepicker(config);
});