$(function () {
    $(document).ready(function () {
        $('.form').on("submit", function (e) {
            $('.form').validate({
                errorPlacement: function (error, element) {
                    return true;
                }
            });

            var allEditors = $('.form').find($(".editor"));
            allEditors.each(function (i) {

                if ($(this).val() == "" || $(this).val() == " " || $(this).val() == null) {
                    e.preventDefault(); //prevent the default action

                   return $('#validationModal').modal('show');
                }
            });

            var allSummernoteEditors = $('.form').find($(".note-editable"));
            allSummernoteEditors.each(function (i) {

                if ($(this).text() == "" || $(this).text() == " " || $(this).text() == null) {
                    e.preventDefault(); //prevent the default action
                    return $('#validationModal').modal('show');
                }
            });

            if (!$('.form').valid()) {
                e.preventDefault(); //prevent the default action
                return $('#validationModal').modal('show');
            }
        });       
    });
});

