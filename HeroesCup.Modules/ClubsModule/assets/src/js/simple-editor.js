$(function () {
    var clubsSelector = 'textarea#editor';
    var height = 600;
    var heroesCupContentCss = [
        "//fonts.googleapis.com/css?family=Montserrat:300,400,500",
        "/css/editor-styles.css"
    ];
    var importCssAppend = false;
    var simple_plugins = "preview paste autolink link";
    var simple_toolbar = "undo redo | bold italic | numlist bullist |  removeformat  |  preview | link ";

    tinymce.init({
        selector: clubsSelector,
        menubar: menubar,
        statusbar: false,
        branding: branding,
        inline: false,
        plugins: simple_plugins,
        width: width,
        autoresize_min_height: autoresizeMinHeight,
        toolbar: simple_toolbar,
        content_css: heroesCupContentCss,
        height: height,
        import_css_append: importCssAppend,
    });
})
