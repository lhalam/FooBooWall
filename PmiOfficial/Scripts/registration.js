var loginok = false;
var passwordok = false;
var rpasswordok = false;
var emailok = false;
$(function ()
{
    $("#okbutton").click(function () {
        if (validatePassword() && emailok && loginok && passwordok && emailok)
        {
            $.post("/api/Verification/Send",
                {
                    Password: $("#password").val(),
                    Email: $("#email").val(),
                    Login: $("#login").val(),
                }
                ).then
            (function (data) {
                document.getElementById("verificationform").className =
                    document.getElementById("verificationform").className.replace
                        (/(?:^|\s)invisible(?!\S)/g, '');
                document.getElementById("okbutton").className += " invisible";
                }, function (error) {
                    var errors = [];
                    for (var key in error.responseJSON.ModelState) {
                        for (var i = 0; i < error.responseJSON.ModelState[key].length; i++) {
                            errors.push(error.responseJSON.ModelState[key][i])
                        }
                    }
                    var message = errors.join(' ');
                    $("#listsend").text(message);
            });
        }
    })
});




$(function () {
    $("#okbutton2").click(function () {
        $.post("/api/Verification/Confirm",
                {                 
                    Code: $("#verification").val(),
                }
                ).then
            (function (data) {
                document.getElementById("verificationresult").className += " goodres";
                document.getElementById("verificationresult").className =
   document.getElementById("verificationresult").className.replace
      (/(?:^|\s)invisible(?!\S)/g, '');
                $("#verificationresult").text("Registration code confirmed!");
                document.getElementById("registerbtn").className =
   document.getElementById("registerbtn").className.replace
      (/(?:^|\s)invisible(?!\S)/g, '');
                document.getElementById("okbutton").className += " invisible";
            }, function (error) {
                document.getElementById("verificationresult").className += " errors";
                $("#verificationresult").text("Incorrect verification code!");
            });
    })
})

$(function () {
    $("#okbutton3").click(function () {
        if (validatePassword()) {
            $.post("/api/Account/Register",
                {
                    Login: $("#login").val(),
                    Password: $("#password").val(),
                    Email: $("#email").val(),
                }).then
                (function (data) {
                    alert("User registered successfully!");
                    $("#listreg").text(' ');
                    document.location.href = "/Login";
                }, function (error) {

                    var errors = [];
                    for (var key in error.responseJSON.ModelState) {
                        for (var i = 0; i < error.responseJSON.ModelState[key].length; i++) {
                            errors.push(error.responseJSON.ModelState[key][i])
                        }
                    }
                    var message = errors.join(' ');
                    $("#listreg").text(message);
                }
                );
        }
    });
});

$(function () {
    $("#login").focusout(function () {
        if ($("#login").val() != null && $("#login").val().length < 1) {
            document.getElementById("loginmessage").className =
   document.getElementById("loginmessage").className.replace
      (/(?:^|\s)invisible(?!\S)/g, '');
            loginok = false;
        }
        else {
            document.getElementById("loginmessage").className += " invisible";
            loginok = true;
        }
    });
});

$(function () {
    $("#password").focusout(function () {
        validatePassword();
        if($("#password").val() != null && $("#password").val().length < 8)
        {
            $("#passwordmessage").text("Password should contain at least 8 characters");
            document.getElementById("passwordmessage").className += " errors";
            passwordok = false;
        }
        else
        {
            $("#passwordmessage").text(' ');
            document.getElementById("empty1").className =
   document.getElementById("empty1").className.replace
      (/(?:^|\s)invisible(?!\S)/g, '');
            passwordok = true;
        }
    });
});

$(function () {
    $("#rpassword").focusout(function () {
        validatePassword();
    });
});

$(function () {
    $("#email").focusout(function () {
        if ($("#email").val() != null && $("#email").val().length < 1) {
            document.getElementById("emailrequired").className =
   document.getElementById("emailrequired").className.replace
      (/(?:^|\s)invisible(?!\S)/g, '');
            if (document.getElementById("emailinvalid").className.indexOf("invisible") == -1)
            {
                document.getElementById("emailinvalid").className += " invisible";
            }
            emailok = false;
        }
        else
        {
            if ($("#email").val().indexOf(".") == -1 || $("#email").val().indexOf("@") == -1)
            {
                document.getElementById("emailrequired").className += " invisible";
                document.getElementById("emailinvalid").className =
   document.getElementById("emailinvalid").className.replace
      (/(?:^|\s)invisible(?!\S)/g, '');
                emailok = false;
            }
            else {
                document.getElementById("emailrequired").className += " invisible";
                document.getElementById("emailinvalid").className += " invisible";
                emailok = true;
            }
        }
    });
});

function validatePassword() {
    if ($("#rpassword").val() != $("#password").val()) {
        if (document.getElementById("repeatpasswordmessage").className.indexOf("errors") == -1) {
            document.getElementById("repeatpasswordmessage").className += " errors";
        }
        document.getElementById("repeatpasswordmessage").className =
                    document.getElementById("repeatpasswordmessage").className.replace
                        (/(?:^|\s)invisible(?!\S)/g, '');
        rpasswordok = false;
        return false;
    }
    else {
        document.getElementById("repeatpasswordmessage").className += " invisible";
        rpasswordok = true;
        return true;
    }
}

//стилі
//валідація на фронтенді
//кі по імейлу
//перевід на логування