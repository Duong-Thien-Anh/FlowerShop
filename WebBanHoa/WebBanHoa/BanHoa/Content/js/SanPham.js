document.addEventListener('DOMContentLoaded', () => {
    function redirectToDetailsPage(id) {
        // Thực hiện chuyển hướng đến trang chi tiết khi click vào card
        window.location.href = '@Url.Action("Details", "Product")/' + id;
    }
});