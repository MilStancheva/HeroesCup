$(function () {

    tinymce.init({
        selector: 'textarea#editor',
        plugins: 'print preview paste importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen link template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount image imagetools textpattern noneditable help charmap quickbars',
        menubar: 'file edit view insert format tools table help',
        toolbar: 'undo redo | bold italic underline | fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap | fullscreen  preview print | insertfile image media template link anchor codesample | ltr rtl',
        toolbar_sticky: true,
        autosave_ask_before_unload: true,
        autosave_interval: "30s",
        autosave_prefix: "{path}{query}-{id}-",
        autosave_restore_when_empty: false,
        autosave_retention: "2m",
        height: 600,
        quickbars_selection_toolbar: 'bold italic | quicklink h2 h3 blockquote quickimage quicktable',
        //noneditable_noneditable_class: "mceNonEditable",
        toolbar_mode: 'sliding',
        block_formats: "Paragraph=p;Header 1=h1;Header 2=h2;Header 3=h3;Header 4=h4;Content=div;Quote=blockquote",
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
            textBlock: { selector: 'div,p,ul,ol,li,table', classes: 'text-block' }
        },
        style_formats: [
            {
                "title": "Heading1",
                "format": "h1"
            },
            {
                "title": "Heading2",
                "format": "h2"
            },
            {
                "title": "Heading3",
                "format": "h3"
            },
            {
                "title": "Heading4",
                "format": "h4"
            },
            {
                "title": "Heading5",
                "format": "h5"
            },
            {
                "title": "Body1-Bold",
                "format": "body1Bold"
            },
            {
                "title": "Body2-Bold",
                "format": "body2Bold"
            },
            {
                "title": "Body3-Bold",
                "format": "body3Bold"
            },
            {
                "title": "Body1",
                "format": "body1"
            },
            {
                "title": "Body2",
                "format": "body2"
            },
            {
                "title": "Body3",
                "format": "body3"
            },
            {
                "title": "TextBlock",
                "fotmat": "textBlock"
            }
        ],
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
        importcss_append: false,
        content_css: "/manager/clubsmodule/css/editor-styles.css",
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
                return '<a class="btn btn-default btn-heroes"><span>' + title + '<span></a>';
            };
        },
        /* enable title field in the Image dialog*/
        image_title: true,
        /* enable automatic uploads of images represented by blob or data URIs*/
        automatic_uploads: true,
        /*
          URL of our upload handler (for more details check: https://www.tiny.cloud/docs/configure/file-image-upload/#images_upload_url)
          images_upload_url: 'postAcceptor.php',
          here we add custom filepicker only to Image dialog
        */
        file_picker_types: 'image',
        /* and here's our custom image picker*/
        file_picker_callback: function (cb, value, meta) {
            var input = document.createElement('input');
            input.setAttribute('type', 'file');
            input.setAttribute('accept', 'image/*');

            /*
              Note: In modern browsers input[type="file"] is functional without
              even adding it to the DOM, but that might not be the case in some older
              or quirky browsers like IE, so you might want to add it to the DOM
              just in case, and visually hide it. And do not forget do remove it
              once you do not need it anymore.
            */

            input.onchange = function () {
                var file = this.files[0];

                var reader = new FileReader();
                reader.onload = function () {
                    /*
                      Note: Now we need to register the blob in TinyMCEs image blob
                      registry. In the next release this part hopefully won't be
                      necessary, as we are looking to handle it internally.
                    */
                    var id = 'blobid' + (new Date()).getTime();
                    var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                    var base64 = reader.result.split(',')[1];
                    var blobInfo = blobCache.create(id, file, base64);
                    blobCache.add(blobInfo);

                    /* call the callback and populate the Title field with the file name */
                    cb(blobInfo.blobUri(), {
                        title: file.name,
                        alt: file.name,
                        width: '496'
                    });
                };
                reader.readAsDataURL(file);
            };

            input.click();
        }
    });
})
