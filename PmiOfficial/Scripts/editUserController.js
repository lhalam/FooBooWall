var saveEditedUser = function () {
    var editedUser = {
        Id: userId,
        FirstName: $("#FirstName").val(),
        LastName: $("#LastName").val(),
        EMail: $("#Email").val(),
        Birthday: $("#BirthDate").val()
    }
   
    //TODO: add image upload to service

    $.post("UserProfile/Edit", editedUser)
      .success(function (data) {
          alert("User was edited successfully!");
          window.location.reload(true);
      });
}