$(function () {
    for (const option of document.querySelectorAll(".custom-option")) {
        option.addEventListener('click', function () {
            if (!this.classList.contains('selected')) {
                var value = option.value;
                var end = value.indexOf('(');
                var trimmedValue = value.substring(0, end);
                option.value = trimmedValue;
            }
        })
    }
});
