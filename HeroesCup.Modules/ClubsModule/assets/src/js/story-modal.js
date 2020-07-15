$(function () {
    $(document).ready(function () {
        var missions = $('.misson-field').val();

        if (missions === null) {
            $('#noMissionsModal').modal('show');
        }
    });
});
