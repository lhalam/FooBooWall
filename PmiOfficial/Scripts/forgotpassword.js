$("#nextBtn").click(function () {

    if ($("#emailForRestoring").val().length > 0) {
        var email =
        {
            Email: $("#emailForRestoring").val()
        }
        $.post("/api/Retrieval/GetCode", email)
            .then(function (response) {
                $("#emailForRestoring").prop('disabled', true);
                document.getElementById("toolTipForCode").style.display = 'block';
                document.getElementById("toolTipForCode").innerHTML = response;
                document.getElementById("codeForRestoringDiv").style.display = 'block';
                document.getElementById("nextBtnDiv").style.display = 'none';
                document.getElementById("nextBtn1Div").style.display = 'block';
            }, function (error) {
                document.getElementById("toolTipForCode").style.display = 'block';
                document.getElementById("toolTipForCode").innerHTML = JSON.parse(error.responseText).Message;
            });
    }


});

$("#nextBtn1").click(function () {

    if ($("#codeForRestoring").val().length > 0) {
        var codeModel =
        {
            Email: $("#emailForRestoring").val(),
            Code: $("#codeForRestoring").val()
        }
        $.post("/api/Retrieval/ConfirmCode", codeModel).then(function (response) {
            $("#codeForRestoring").prop('disabled', true);
            document.getElementById("toolTipForPassword").style.display = 'block';
            document.getElementById("toolTipForPassword").innerHTML = response;
            document.getElementById("nextBtn1Div").style.display = 'none';
            document.getElementById("formNewPassword").style.display = 'block';
            document.getElementById("formRepeatPassword").style.display = 'block';
            document.getElementById("nextBtn2Div").style.display = 'block';
        }, function (error) {
            document.getElementById("toolTipForPassword").style.display = 'block';
            document.getElementById("toolTipForPassword").innerHTML = JSON.parse(error.responseText).Message;
        });
    }
});
$("#nextBtn2").click(function () {

    if (validatePassword()) {
        var newPasswordModel =
        {
            Email: $("#emailForRestoring").val(),
            Password: $("#newPassword").val()
        }
        $.post("/api/Retrieval/ConfirmPassword", newPasswordModel).then(function (response) {
            document.location.href = '/Login';
        });
    }

});

var validatePassword = function () {

    var pas1 = $("#newPassword").val();
    var pas2 = $("#repeatPassword").val();
    if (pas1 != pas2 || pas1.length < 8) {
        var item = document.getElementById("passwordError");
        item.style.display = 'block';
        item.innerHTML = "Passwords are different or have length lesser 8";
        return false;
    }
    return true;

}

$("#emailForRestoring").focus(function () {
    document.getElementById("toolTipForCode").style.display = 'none';
    document.getElementById("toolTipForCode").innerHTML = " ";
});

$("#codeForRestoring").focus(function () {
    document.getElementById("toolTipForPassword").style.display = 'none';
    document.getElementById("toolTipForPassword").innerHTML = " ";
});