/*global
    piranha, tinymce
*/

//
// Create a new inline editor
//
piranha.editor.addInline = function (id, toolbarId) {
    tinymce.init({
        selector: "#" + id,
        fixed_toolbar_container: "#" + toolbarId,
        menubar: false,
        branding: false,
        statusbar: false,
        inline: true,
        convert_urls: false,
        plugins: [
            piranha.editorconfig.plugins
        ],
        width: "100%",
        autoresize_min_height: 0,
        toolbar: piranha.editorconfig.toolbar,
        extended_valid_elements: piranha.editorconfig.extended_valid_elements,
        block_formats: piranha.editorconfig.block_formats,
        style_formats: piranha.editorconfig.style_formats,
        file_picker_callback: function (callback, value, meta) {
            // Provide file and text for the link dialog
            if (meta.filetype == 'file') {
                piranha.mediapicker.openCurrentFolder(function (data) {
                    callback(data.publicUrl, { text: data.filename });
                }, null);
            }

            // Provide image and alt text for the image dialog
            if (meta.filetype == 'image') {
                piranha.mediapicker.openCurrentFolder(function (data) {
                    callback(data.publicUrl, { alt: "" });
                }, "image");
            }
        },
        content_css: "/css/editor-styles.css",
        setup: function (editor) {
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
                return '<a class="btn btn-default btn-heroes"><span>' + title +'<span></a>';
            };
        }
    });
    $("#" + id).parent().append("<a class='tiny-brand' href='https://www.tiny.cloud' target='tiny'>Powered by Tiny</a>");
};

//
// Remove the TinyMCE instance with the given id.
//
piranha.editor.remove = function (id) {
    tinymce.remove(tinymce.get(id));
    $("#" + id).parent().find('.tiny-brand').remove();
};
