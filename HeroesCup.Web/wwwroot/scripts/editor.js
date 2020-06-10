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
        menubar: 'file edit view insert format tools table help',
        branding: false,
        statusbar: true,
        inline: true,
        convert_urls: false,
        plugins: [
            piranha.editorconfig.plugins
        ],
        width: "100%",
        toolbar_sticky: false,
        autosave_ask_before_unload: true,
        autosave_interval: "30s",
        autosave_prefix: "{path}{query}-{id}-",
        autosave_restore_when_empty: false,
        autosave_retention: "2m",
        image_advtab: true,
        autoresize_min_height: 0,
        toolbar: piranha.editorconfig.toolbar,
        toolbar_mode: 'sliding',
        contextmenu: "link image imagetools table",
        extended_valid_elements: piranha.editorconfig.extended_valid_elements,
        block_formats: piranha.editorconfig.block_formats,
        formats: {
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
            textBlock: { selector: 'div,p,ul,ol,li,table', classes: 'text-block'}
        },
        style_formats: piranha.editorconfig.style_formats,
        color_map: [
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
        ],
        file_picker_types: 'file image media',
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
                    var input = document.createElement('input');
                    input.setAttribute('type', 'file');
                    input.setAttribute('accept', 'image/*');

                    callback(data.publicUrl, {
                        title: data.publicUrl,
                        alt: data.publicUrl,
                        width: '496'
                    });

                    /*
                      Note: In modern browsers input[type="file"] is functional without
                      even adding it to the DOM, but that might not be the case in some older
                      or quirky browsers like IE, so you might want to add it to the DOM
                      just in case, and visually hide it. And do not forget do remove it
                      once you do not need it anymore.
                    */

                //    input.onchange = function () {
                //        var file = this.files[0];

                //        var reader = new FileReader();
                //        reader.onload = function () {
                //            /*
                //              Note: Now we need to register the blob in TinyMCEs image blob
                //              registry. In the next release this part hopefully won't be
                //              necessary, as we are looking to handle it internally.
                //            */
                //            var id = 'blobid' + (new Date()).getTime();
                //            var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                //            var base64 = reader.result.split(',')[1];
                //            var blobInfo = blobCache.create(id, file, base64);
                //            blobCache.add(blobInfo);

                //            /* call the callback and populate the Title field with the file name */
                //            callback(blobInfo.blobUri(), {
                //                title: file.name,
                //                alt: file.name,
                //                width: '496'
                //            });
                //        };
                //        reader.readAsDataURL(file);
                //    };

                //    input.click();
                }, "image");
            }
        },
        content_css: "/css/editor-styles.css",
        font_formats: 'Montserrat, sans-serif; Arial=arial,helvetica,sans-serif; Courier New=courier new,courier,monospace;',
        fontsize_formats: '11px 12px 14px 16px 18px 22px 24px 28px 36px 42px 48px',
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
        },
        /* enable title field in the Image dialog*/
        image_title: true
    });
};

//
// Remove the TinyMCE instance with the given id.
//
piranha.editor.remove = function (id) {
    tinymce.remove(tinymce.get(id));
    $("#" + id).parent().find('.tiny-brand').remove();
};

