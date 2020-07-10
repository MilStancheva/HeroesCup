$(document).ready(function () {
    $('.editor').summernote({
        height: 100,
        toolbar: [
            ['style', ['bold', 'italic', 'clear']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['insert', ['link']],
        ]
    });
});
