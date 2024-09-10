document.getElementById('uploadBtn').addEventListener('click', function () {
    var fileInput = document.getElementById('pdfFile');
    var file = fileInput.files[0];
    var formData = new FormData();
    formData.append('pdfFile', file);

    fetch('/SachAdminController/Create', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('Tải lên file PDF thành công!');
            } else {
                alert('Lỗi khi tải lên file PDF: ' + data.message);
            }
        })
        .catch(error => console.error('Error:', error));
});