
    let eyeicon = document.getElementById("eyeicon");
    let password = document.getElementById("MatKhau");
    let imagePath1 = "@Url.Content("~/Content/img / eye - open.png")";
    let imagePath2 = "@Url.Content("~/Content/img / eye - close.png")";

    eyeicon.onclick = function () {
        if (password.type === "password") {
            password.type = "text";
            eyeicon.img
            eyeicon.src = imagePath1;
        } else {
            password.type = "password";
            eyeicon.src = imagePath2;
        }
    };
