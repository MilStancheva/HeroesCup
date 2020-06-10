$(function () {

    tinymce.init({
        selector: 'textarea#editor',
        plugins: 'print preview paste importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen link template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount image imagetools textpattern noneditable help charmap quickbars',
        menubar: 'file edit view insert format tools table help',
        toolbar: 'undo redo | bold italic underline | fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap | fullscreen  preview print | insertfile image media template link anchor codesample | ltr rtl | customarign',
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
        content_css: [
            "//fonts.googleapis.com/css?family=Montserrat:300,400,500",
            "/manager/clubsmodule/css/editor-styles.css"
        ],
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

            editor.ui.registry.addButton('customarign', {
                text: 'Set Margin',
                tooltip: 'Set Margin',
                classes: "textBlock",
                onAction: function (_) {
                    tinymce.activeEditor.formatter.apply('textBlock');
                }
            });
        },
        image_title: true,
        automatic_uploads: true,
        file_picker_types: 'image',
        file_picker_callback: function (cb, value, meta) {
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
        }
    });
})
