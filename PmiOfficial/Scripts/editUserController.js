var saveEditedUser = function () {
    var editedUser = {
        Id: userId,
        FirstName: $("#FirstName").val(),
        LastName: $("#LastName").val(),
        EMail: $("#Email").val(),
        Birthday: new Date($("#BirthDate").val()).getTime()
    }
   
    //TODO: add image upload to service

    $.post("UserProfile/Edit", editedUser)
      .success(function (data) {
          alert("User was edited successfully!");
          window.location.reload(true);
      });
}

var pickDate = function () {
    $('#BirthDate').datepick({
        maxDate: new Date()
    });
}
