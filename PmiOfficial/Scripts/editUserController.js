var checkEditedUser = function () {
    var editedUser = {
        Id: userId,
        FirstName: $("#FirstName").val(),
        LastName: $("#LastName").val(),
        EMail: $("#Email").val(),
        Birthday: new Date($("#BirthDate").val()).getTime(),
        SkypeName: $("#SkypeName").val()
    }

    var emailRegexp = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/
    var result = editedUser.FirstName.length > 0 &&
                 editedUser.LastName.length > 0 &&
                 emailRegexp.test(editedUser.EMail);

    if (result) {
        $("#BirthDate").val(editedUser.Birthday);
    }


    return result;
}

$(function () {
    $('#BirthDate').datepick({
        maxDate: new Date()
    });
});