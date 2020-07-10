piranha.editor.addInline = function (id, toolbarId) {
    tinymce.init({
        selector: "#" + id,
        fixed_toolbar_container: "#" + toolbarId,
        menubar: menubar,
        branding: branding,
        statusbar: statusbar,
        inline: inline,
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
        //contextmenu: contextMenu,
        extended_valid_elements: extendedValidElements,
        block_formats: blockFormats,
        formats: formats,
        style_formats: styleFormats,
        color_map: colorMap,
        file_picker_types: filePickerTypes,
        file_picker_callback: filePickerCallback,
        content_css: contentCss,
        font_formats: fontFormats,
        fontsize_formats: fontsizeFormats,
        setup: setUp,
        image_title: imageTitle,
        image_class_list: imageClassList,
    });
};

//
// Remove the TinyMCE instance with the given id.
//
piranha.editor.remove = function (id) {
    tinymce.remove(tinymce.get(id));
    $("#" + id).parent().find('.tiny-brand').remove();
};
