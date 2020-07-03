$(function () {
    $(document).ready(function () {
        $(function () {
            $.ajax({
                type: 'GET',
                url: '/manager/clubsmodule/bg-cities.json',
                dataType: 'json',
                success: function (data) {
                    var items = [];
                    $.each(data, function (key, val) {
                        items.push(val.name);
                    });

                    $("#location").autocomplete({
                        source: items
                    });
                }
            });
        });
    });
});
