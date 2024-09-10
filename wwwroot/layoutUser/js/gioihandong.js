document.addEventListener("DOMContentLoaded", function () {
    var elements = document.querySelectorAll('.my-2');
    elements.forEach(function (element) {
        var lineHeight = parseFloat(window.getComputedStyle(element).lineHeight);
        var maxHeight = lineHeight * 2; // Giới hạn 2 dòng
        var contentHeight = element.scrollHeight;
        if (contentHeight > maxHeight) {
            element.classList.add('ellipsis');
        }
    });
});
