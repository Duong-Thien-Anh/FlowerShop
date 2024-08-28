

function AddLoaiSP() {
    var obj = {
        maloaisp: $("#MaLoaiSP").val(),
        tenloaisp: $("#TenLoaiSP").val()
    };
    if (obj.maloaisp.trim() == "" || obj.tenloaisp.trim() == "")
        alert("Vui lòng nhập đầy đủ thông tin")
    else {
        $.ajax({
            url: "/LoaiSanPham/Add",
            data: JSON.stringify(obj),
            type: "POST",
            contentType: "application/json;charset-utf-8",
            dataType: "json",
            success: function (result) {
                $("#myModal").modal('hide');
                location.reload();
                alert("Thêm Loại Sản Phẩm Thành Công");
            },
            error: function (errorMessage) {
                alert(errorMessage.responseText);
            }
        });
    }

}


function UpdateSanPham() {
    var obj = {
        maloaisp: $("#MaLoaiSP").val(),
        tenloaisp: $("#TenLoaiSP").val()
    };
    $.ajax({
        url: "/LoaiSanPham/Update",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset-utf-8",
        dataType: "Json",
        success: function (result) {
            location.reload();
            $('#myModal').modal('hide');
            $('#MaLoaiSP').val("");
            $('#TenLoaiSP').val("");
            alert("Chỉnh sửa thành công");
        },
        error: function (erroeMessage) {
            alert(erroeMessage.responseText);
        }
    });
}
