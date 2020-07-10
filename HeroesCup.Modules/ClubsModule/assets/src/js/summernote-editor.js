$(document).ready(function () {
    $('.editor').summernote({
        height: 180,
        toolbar: [
            ['style', ['bold', 'italic', 'clear']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['insert', ['link']],
        ]
    });
});
