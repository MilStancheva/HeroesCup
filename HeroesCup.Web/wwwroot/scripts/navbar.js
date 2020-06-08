$(function () {
    $(document).scroll(function () {
        var $nav = $(".fixed-top");
        $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());
        var $shareButtons = $(".share-buttons");
        $shareButtons.toggleClass('sticky', $(this).scrollTop() > $nav.height());
    });
});