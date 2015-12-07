var usefulLinkImageUrlChanged = function() {
    document.getElementById("UsefulLinkImage").src = document.getElementById("UsefulLinkImageUrl").value;
}

var saveAddedUsefulLink = function () {

    var UsefulLinkName = document.getElementById("UsefulLinkName").value;
    var UsefulLinkUrl = document.getElementById("UsefulLinkUrl").value;
    var UsefulLinkImageUrl = document.getElementById("UsefulLinkImageUrl").value;
    var UsefulLinkCommentNode = document.getElementById("UsefulLinkComment");
    //var UsefulLinkComment = UsefulLinkCommentNode.value;

    var usefulLinkToAdd =
        {
            OwnerUserID: userId,
            Name: document.getElementById("UsefulLinkName").value,
            Url: document.getElementById("UsefulLinkUrl").value,
            ImageUrl: document.getElementById("UsefulLinkImageUrl").value,
            //Comment: document.getElementById("UsefulLinkComment").value
        };
    $.post("api/UsefulLinks/AddUsefulLink", usefulLinkToAdd)
      .success(onSuccessfullUsefulLinkAdding());
    //loggedUserID
}

var onSuccessfullUsefulLinkAdding = function () {
    alert("Useful link was added successfully!");
    window.location.reload(true);
}

var removeUsefulLink = function (id) {
    //$.ajax({
    //    url: url,
    //    type: 'DELETE',
    //    success: callback,
    //    data: data,
    //    contentType: type
    //});
    $.post("api/UsefulLinks/RemoveUsefulLink/" + id, id).success(
        function(){
            window.location.reload(true);
        });
}

/*$(document).ready(function () {
    $('#UsefulLinkImageUrl').change(function () {
        $('#UsefulLinkImage').src = this.value;
    });
});*/

/*var usefulLinkImageUrlChanged = function () {
    ("#UsefulLinkImage").src = ("#UsefulLinkImageUrl").valueOf();
}*/