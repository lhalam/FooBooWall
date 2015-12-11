var checkEditedUser = function () {
    var editedUser = {
        Id: userId,
        FirstName: $("#FirstName").val(),
        LastName: $("#LastName").val(),
        EMail: $("#Email").val(),
        Birthday: new Date($("#BirthDate").val()).getTime(),
        SkypeName: $("#SkypeName").val()
    }

    var result = checkFirstName(editedUser.FirstName) &&
                 checkLastName(editedUser.LastName) &&
                 checkEmail(editedUser.EMail);

    if (result) {
        $("#BirthDate").val(editedUser.Birthday);
    }


    return result;
}

var EMAIL_REGEX = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/
var FIRST_LAST_NAME_REGEX = /^([A-Z\u0410-\u042f\u0407\u0406\u0404']{1}[a-z\u0430-\u044f\u0456\u0457\u0454']{1,20}\u002d{1}[A-Z\u0410-\u042f\u0407\u0406\u0404']{1}[a-z\u0430-\u044f\u0456\u0457\u0454']{1,20}|[A-Z\u0410-\u042f\u0407\u0406\u0404']{1}[a-z\u0430-\u044f\u0456\u0457\u0454']{1,20})$/;


var checkFirstName = function (firstName) {
    if (!FIRST_LAST_NAME_REGEX.test(firstName)) {
        alert("There is an error in first name!");
        return false;
    }
    return true;
}

var checkLastName = function (lastName) {
    if (!FIRST_LAST_NAME_REGEX.test(lastName)) {
        alert("There is an error in last name!");
        return false;
    }
    return true;
}

var checkEmail = function (email) {
    if (!EMAIL_REGEX.test(email)) {
        alert("There is an error in email!");
        return false;
    }
    return true;
}

$(function () {
    $('#BirthDate').datepick({
        maxDate: new Date()
    });
});