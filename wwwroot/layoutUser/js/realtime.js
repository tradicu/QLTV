// Function để cập nhật thời gian thực lên phần tử h2
function updateRealtime() {
    // Lấy phần tử h2 có id là "realtimeData"
    var h2Element = document.getElementById("realtimeData");

    // Tạo một đối tượng Date mới để lấy thời gian hiện tại
    var currentTime = new Date();

    // Biến đổi thời gian hiện tại thành định dạng chuỗi
    var timeString = currentTime.toLocaleTimeString();

    // Gán chuỗi thời gian vào phần tử h2
    h2Element.innerText = timeString;
}

// Gọi hàm updateRealtime() mỗi giây (1000ms)
setInterval(updateRealtime, 1000);
