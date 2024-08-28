// Tự động thay đổi chiều rộng của menu khi cửa sổ thay đổi kích thước
window.addEventListener("resize", function () {
    var navbar = document.querySelector(".navigations");
    navbar.style.width = "auto"; // Đặt lại chiều rộng ban đầu
    var navbarWidth = navbar.scrollWidth;
    var windowWidth = window.innerWidth;

    if (navbarWidth > windowWidth) {
        navbar.style.width = navbarWidth + "px"; // Thiết lập chiều rộng nếu nội dung quá rộng
    }
});
document.addEventListener('DOMContentLoaded', () => {
    const wrapper = document.querySelector('.wrapper');
    const loginLink = document.querySelector('.login-link');
    const registerLink = document.querySelector('.register-link');
    const btnPopup = document.querySelector('.btnlogin-popup');
    registerLink.addEventListener('click', () => {
        wrapper.classList.add('active');
    });

    loginLink.addEventListener('click', () => {
        wrapper.classList.remove('active');
    });
    btnPopup.addEventListener('click', () => {
        wrapper.classList.add('active-popup');
    });
    iconClose.addEventListener('click', () => {
        wrapper.classList.remove('active-popup');
    });
});

