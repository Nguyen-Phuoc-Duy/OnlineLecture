// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function registerClass(idSubject) {
    fetch('Home/AddTheClass', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
           
        },
        body: JSON.stringify({ IdSubject: idSubject })
    })
        .then(response => response.json())
        .then(data => {
            // Xử lý kết quả nếu cần
            console.log(data);
            window.location.href = "/Home/Index"; // Chuyển hướng sau khi gửi thành công
        })
        .catch(error => {
            console.error('Error:', error);
        });
}