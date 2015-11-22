$(function () {
    $("#okbutton").click(function () {
        if (validatePassword()) {
            $.post("/api/Account/Register",
                {
                    Login: $("#login").val(),
                    Password: $("#password").val(),
                    Email: $("#email").val(),
                }).then
                (function (data) {
                    alert("User registered successfully!");
                    $("#list").text(' ');
                    document.location.href = "/Login/Login";
                }, function (error) {

                    var errors = [];
                    for (var key in error.responseJSON.ModelState) {
                        for (var i = 0; i < error.responseJSON.ModelState[key].length; i++) {
                            errors.push(error.responseJSON.ModelState[key][i])
                        }
                    }
                    var message = errors.join(' ');
                    $("#list").text(message);
                }
                );
        }
    });
});

function validatePassword() {
    if ($("#rpassword").val() != $("#password").val()) {
        $("#list").text("Passwords should match!");
        return false;
    }
    else return true;
}
//стилі
//валідація на фронтенді
//кі по імейлу
//перевід на логування