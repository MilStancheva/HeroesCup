$(function () {

    tinymce.init({
        selector: 'textarea#editor',
        plugins: 'print preview paste importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen link template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern noneditable help charmap quickbars',
        menubar: 'file edit view insert format tools table help',
        toolbar: 'undo redo | bold italic underline strikethrough | fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | fullscreen  preview save print | insertfile image media template link anchor codesample | ltr rtl',
        toolbar_sticky: true,
        autosave_ask_before_unload: true,
        autosave_interval: "30s",
        autosave_prefix: "{path}{query}-{id}-",
        autosave_restore_when_empty: false,
        autosave_retention: "2m",
        image_advtab: true,
        content_css: '/assets/src/css/editor-styles.css',
        importcss_append: true,
        templates: [
            { title: 'New Table', description: 'creates a new table', content: '<div class="mceTmpl"><table width="98%%"  border="0" cellspacing="0" cellpadding="0"><tr><th scope="col"> </th><th scope="col"> </th></tr><tr><td> </td><td> </td></tr></table></div>' },
            { title: 'Starting my story', description: 'A cure for writers block', content: 'Once upon a time...' },
            { title: 'New list with dates', description: 'New List with dates', content: '<div class="mceTmpl"><span class="cdate">cdate</span><br /><span class="mdate">mdate</span><h2>My List</h2><ul><li></li><li></li></ul></div>' }
        ],
        template_cdate_format: '[Date Created (CDATE): %m/%d/%Y : %H:%M:%S]',
        template_mdate_format: '[Date Modified (MDATE): %m/%d/%Y : %H:%M:%S]',
        height: 600,
        image_caption: true,
        quickbars_selection_toolbar: 'bold italic | quicklink h2 h3 blockquote quickimage quicktable',
        noneditable_noneditable_class: "mceNonEditable",
        toolbar_mode: 'sliding',
        contextmenu: "link image imagetools table",
        font_formats: 'Montserrat, sans-serif; Arial=arial,helvetica,sans-serif; Courier New=courier new,courier,monospace;',
        fontsize_formats: '11px 12px 14px 16px 18px 22px 24px 28px 36px 42px 48px',
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
    });
})
