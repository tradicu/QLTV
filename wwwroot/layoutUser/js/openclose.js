// Tạo một đối tượng Date mới để lấy thời gian hiện tại
var currentDate = new Date();

// Lấy ngày trong tuần (thứ)
var dayOfWeek = currentDate.getDay();

// Kiểm tra nếu là thứ 2 đến thứ 6
if (dayOfWeek >= 1 && dayOfWeek <= 5) {
    // Lấy giờ hiện tại
    var currentHour = currentDate.getHours();
    // Lấy phút hiện tại
    var currentMinute = currentDate.getMinutes();
    // Lấy giây hiện tại
    var currentSecond = currentDate.getSeconds();

    // Kiểm tra nếu thời gian hiện tại nằm trong khoảng từ 07:30:00 đến 17:30:00
    if ((currentHour > 7 || (currentHour === 7 && currentMinute >= 30)) && (currentHour < 17 || (currentHour === 17 && currentMinute < 30))) {
        // Nếu đúng, gán văn bản "Đang mở cửa" vào phần tử p
        document.getElementById("openStatus").innerText = "Đang mở cửa";
        // Thêm lớp 'open' vào phần tử p
        document.getElementById("openStatus").classList.add("open");
    } else {
        // Nếu sai, gán văn bản "Không mở cửa" vào phần tử p
        document.getElementById("openStatus").innerText = "Đóng  cửa";
        // Thêm lớp 'closed' vào phần tử p
        document.getElementById("openStatus").classList.add("closed");
    }
} else {
    // Nếu là Chủ Nhật hoặc thứ 7, gán văn bản "Không mở cửa" vào phần tử p
    document.getElementById("openStatus").innerText = "Đóng cửa";
    // Thêm lớp 'closed' vào phần tử p
    document.getElementById("openStatus").classList.add("closed");
}
