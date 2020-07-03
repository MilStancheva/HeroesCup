$(function () {
    var clubsSelector = 'textarea.editor';
    var quickbarsSelectionToolbar = 'bold italic | quicklink h2 h3 blockquote quickimage quicktable';
    var height = 200;
    var importCssAppend = false;
    var automaticUploads = true;
    var clubsFilePickerTypes = 'image';
    var clubsFilePickerCallback = function (cb, value, meta) {
        var input = document.createElement('input');
        input.setAttribute('type', 'file');
        input.setAttribute('accept', 'image/*');

        input.onchange = function () {
            var file = this.files[0];

            var reader = new FileReader();
            reader.onload = function () {
                var id = 'blobid' + (new Date()).getTime();
                var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                var base64 = reader.result.split(',')[1];
                var blobInfo = blobCache.create(id, file, base64);
                blobCache.add(blobInfo);

                /* call the callback and populate the Title field with the file name */
                cb(blobInfo.blobUri(), {
                    title: meta.filename,
                    alt: file.name,
                    width: '496'
                });
            };
            reader.readAsDataURL(file);
        };

        input.click();
    };
    var heroesCupContentCss = [
        "//fonts.googleapis.com/css?family=Montserrat:300,400,500",
        "/css/editor-styles.css"
    ];

    tinymce.init({
        selector: clubsSelector,
        menubar: menubar,
        branding: branding,
        statusbar: statusbar,
        inline: false,
        convert_urls: convertUrls,
        plugins: plugins,
        width: width,
        toolbar_sticky: stickyToolbar,
        autosave_ask_before_unload: autosaveAskBeforeUnload,
        autosave_interval: autosaveInterval,
        autosave_prefix: autosavePrefix,
        autosave_restore_when_empty: autosaveRestoreWhenEmpty,
        autosave_retention: autosaveRetention,
        image_advtab: imageAdvtab,
        autoresize_min_height: autoresizeMinHeight,
        toolbar: toolbar,
        toolbar_mode: toolbarMode,
        contextmenu: contextMenu,
        extended_valid_elements: extendedValidElements,
        block_formats: blockFormats,
        formats: formats,
        style_formats: styleFormats,
        color_map: colorMap,
        file_picker_types: clubsFilePickerTypes,
        file_picker_callback: clubsFilePickerCallback,
        content_css: heroesCupContentCss,
        font_formats: fontFormats,
        fontsize_formats: fontsizeFormats,
        setup: setUp,
        image_title: imageTitle,
        height: height,
        quickbars_selection_toolbar: quickbarsSelectionToolbar,
        import_css_append: importCssAppend,
        automatic_uploads: automaticUploads,
    });
})
