$(function () {
    var clubsSelector = 'textarea#editor';
    var quickbarsSelectionToolbar = 'bold italic | quicklink h2 h3 blockquote quickimage quicktable';
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
        statusbar: statusbar,
        branding: branding,
        inline: false,
        plugins: simple_plugins,
        width: width,
        autoresize_min_height: autoresizeMinHeight,
        toolbar: simple_toolbar,
        block_formats: blockFormats,
        formats: formats,
        style_formats: styleFormats,
        content_css: heroesCupContentCss,
        font_formats: fontFormats,
        fontsize_formats: fontsizeFormats,
        height: height,
        quickbars_selection_toolbar: quickbarsSelectionToolbar,
        import_css_append: importCssAppend,
    });
})
