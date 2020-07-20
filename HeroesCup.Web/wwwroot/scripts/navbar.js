$(function () {
    $(document).scroll(function () {
        var $nav = $(".fixed-top");
        $nav.toggleClass('scrolled', $(this).scrollTop() > $nav.height());        
    });

    $("#btn-nav-toggle").click(function () {
       
        if ($(this).hasClass("icon-hamburger")) {
            $(this).removeClass("icon-hamburger");
            $(this).addClass("modal-close");
            $(this).parent().addClass("heroes-nav");

        } else {
            $(this).removeClass("modal-close");
            $(this).addClass("icon-hamburger");
            $(this).parent().removeClass("heroes-nav");

        }
    });
});