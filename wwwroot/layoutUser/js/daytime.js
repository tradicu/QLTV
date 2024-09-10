// Tạo một đối tượng Date mới để lấy thời gian hiện tại
var currentDate = new Date();

// Lấy ngày trong tuần (thứ)
var dayOfWeek = currentDate.getDay();
var days = ['Chủ nhật', 'Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7'];
var currentDay = days[dayOfWeek];

// Lấy ngày trong tháng
var dayOfMonth = currentDate.getDate();

// Lấy tháng
var month = currentDate.getMonth();
var months = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'];
var currentMonth = months[month];

// Lấy năm
var year = currentDate.getFullYear();

// Tạo một chuỗi để hiển thị thứ, ngày, tháng và năm
var dateString = currentDay + ',' + dayOfMonth + '/' + currentMonth + '/' + year;
var daterealtime = year + '-' + currentMonth + '-' + dayOfMonth;
// Lấy phần tử h2 có id là "currentDate"
var h2Element = document.getElementById("currentDate");
var hehe = document.getElementById("currentDateDate");
// Gán chuỗi dateString vào nội dung của phần tử h2
h2Element.innerText = dateString;
hehe.innerText = dateString;