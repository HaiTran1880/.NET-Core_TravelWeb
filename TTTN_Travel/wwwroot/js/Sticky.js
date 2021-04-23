/*Chạy Hàm Khi Người Dùng scroll trang web*/
window.onscroll = function () { myFunction() };

/*Lấy phần tử navbar*/
var navbar = document.getElementById("navbar");
var header = document.getElementsByTagName("header");
/*Lấy Vị Trí offset của navbar*/
var sticky = header.offsetTop;

/*thêm hay xóa class sticky dựa vào vị trí scroll của người dùng*/
function myFunction() {
    if (window.pageYOffset >= sticky) {
        navbar.classList.add("sticky")
        header.classList.add("fixed-header")
    } else {
        navbar.classList.remove("sticky");
        header.classList.remove("fixed-header")
    }
}