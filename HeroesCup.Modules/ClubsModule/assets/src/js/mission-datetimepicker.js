$(function () {
    var start_date_config, end_date_config;
    var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

    start_date_config = {
        format: 'dd mm yyyy',
        //minDate: today,
        maxDate: function () {
            return $('#end_date').val();
        }
    };

    end_date_config = {
        format: 'dd mm yyyy',
        minDate: function () {
            return $('#start_date').val();
        }
    };

    $("#start_date").datepicker(start_date_config);
    $("#end_date").datepicker(end_date_config);
});