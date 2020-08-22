/*global
    piranha, tinymce
*/

//
// Create a new inline editor
//

var menubar = 'file edit view insert format tools table help';
var plugins = [
    piranha.editorconfig.plugins
];
var branding = false;
var statusbar = true;
var inline = true;
var convertUrls = false;
var width = "100%";
var stickyToolbar = true;
var autosaveAskBeforeUnload = true;
var autosaveInterval = "30s";
var autosavePrefix = "{path}{query}-{id}-";
var autosaveRestoreWhenEmpty = false;
var autosaveRetention = "2m";
var imageAdvtab = true;
var autoresizeMinHeight = 0;
var toolbar = piranha.editorconfig.toolbar;
var toolbarMode = "sliding";
//var contextMenu = "link image imagetools table";
var extendedValidElements = piranha.editorconfig.extended_valid_elements;
var blockFormats = piranha.editorconfig.block_formats;
var formats = {
    h1: { block: 'h1', classes: 'heading1' },
    h2: { block: 'h2', classes: 'heading2' },
    h3: { block: 'h3', classes: 'heading3' },
    h4: { block: 'h4', classes: 'heading4' },
    h5: { block: 'h5', classes: 'heading5' },
    body1: { selector: 'div,p,td,th,div,ul,ol,li,table', classes: 'body1' },
    body2: { selector: 'div,p,td,th,div,ul,ol,li,table', classes: 'body2' },
    body3: { selector: 'div,p,td,th,div,ul,ol,li,table', classes: 'body3' },
    body1Bold: { selector: 'div,p,td,th,div,ul,ol,li,table', classes: 'body1-bold' },
    body2Bold: { selector: 'div,p,td,th,div,ul,ol,li,table', classes: 'body2-bold' },
    body3Bold: { selector: 'div,p,td,th,div,ul,ol,li,table', classes: 'body3-bold' },
    topPadding: { selector: 'div,p,ul,ol,li,table', classes: 'top-padding' },
    bottomPadding: { selector: 'div,p,ul,ol,li,table', classes: 'bottom-padding' }
};
var styleFormats = piranha.editorconfig.style_formats;
var colorMap = [
    'BFEDD2', 'Light Green',
    'FAF8F3', 'Light Yellow',
    'F8CAC6', 'Light Red',
    'ECCAFA', 'Light Purple',
    'EBE7FD', 'Light Blue',
    'FFEFE8', 'Light Orange',

    '2DC26B', 'Green',
    'FFE89E', 'Yellow',
    'E03E2D', 'Red',
    'B96AD9', 'Purple',
    '370AEB', 'Blue',

    '1C1E21', 'Dark',
    '169179', 'Dark Turquoise',
    'FE5E17', 'Orange',
    'BA372A', 'Dark Red',
    '843FA1', 'Dark Purple',
    '236FA1', 'Dark Blue',
    'EEECE6', 'Dark Yellow',

    'ECF0F1', 'Light Gray',
    'CED4D9', 'Medium Gray',
    '95A5A6', 'Gray',
    '7E8C8D', 'Dark Gray',
    '34495E', 'Navy Blue',
    'E2232E', 'Warning',

    '000000', 'Black',
    'ffffff', 'White'
];
var filePickerTypes = "file image media";
var filePickerCallback = function (callback, value, meta) {

    if (meta.filetype == 'file') {
        piranha.mediapicker.openCurrentFolder(function (data) {
            callback(data.publicUrl, { text: data.filename });
        }, null);
    }

    if (meta.filetype == 'image') {
        piranha.mediapicker.openCurrentFolder(function (data) {
            callback(data.publicUrl, {
                title: meta.filename,
                alt: meta.filename
            })
        }, "image");
    }
};
var contentCss = [
    "//fonts.googleapis.com/css?family=Montserrat:300,400,500",
    "/css/editor-styles.css"
];
var fontFormats = "Montserrat, sans-serif; Arial=arial,helvetica,sans-serif; Courier New=courier new,courier,monospace;";
var fontsizeFormats = "11px 12px 14px 16px 18px 22px 24px 28px 36px 42px 48px";
var setUp = function (editor) {
    var title = "Click here to add text";
    editor.ui.registry.addButton('heroButton', {
        text: 'Hero Button',
        tooltip: 'Insert Hero Button',
        classes: "btn btn-default btn-heroes",
        onAction: function (_) {
            editor.insertContent(heroButtonHtml(title));
        }
    });

    var heroButtonHtml = function (title) {
        return '<a class="btn btn-default btn-heroes"><span>' + title + '<span></a>';
    };

    editor.ui.registry.addButton('top_padding', {
        text: 'Top',
        tooltip: 'Set top padding',
        classes: "top-padding",
        onAction: function (_) {
            tinymce.activeEditor.formatter.apply('topPadding');
        }
    });

    editor.ui.registry.addButton('bottom_padding', {
        text: 'Bottom',
        tooltip: 'Set bottom padding',
        classes: "bottom-padding",
        onAction: function (_) {
            tinymce.activeEditor.formatter.apply('bottomPadding');
        }
    });
};
var imageTitle = true;
var imageClassList = [
    { title: 'Blog Image', value: 'events-image' },
    { title: 'Illustration Image', value: 'illustration-image' },
    { title: 'No styles', value: '' },
];

var pasteAsText = true;

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
        paste_as_text: pasteAsText
    });
};

//
// Remove the TinyMCE instance with the given id.
//
piranha.editor.remove = function (id) {
    tinymce.remove(tinymce.get(id));
    $("#" + id).parent().find('.tiny-brand').remove();
};
