

var saveEditedUser = function () {
    var editedUser = {
        Id: userId,
        FirstName: $("#FirstName").val(),
        LastName: $("#LastName").val(),
        EMail: $("#Email").val(),
        Birthday: new Date($("#BirthDate").val()).getTime()
    }

    if (checkValid(editedUser)) {
        $.post(editUserUrl, editedUser)
          .success(function (data) {
              alert("User was edited successfully!");
              window.location.reload(true);
          });
    } else {
        alert("Wrong input!");
    }
}

var checkValid = function (editedUser) {
    var emailRegexp = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/
    var result = editedUser.FirstName.length > 0 &&
                 editedUser.LastName.length > 0 &&
                 emailRegexp.test(editedUser.EMail);
    return result;
}

$(function () {
    $('#BirthDate').datepick({
        maxDate: new Date()
    });
});