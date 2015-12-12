var usefulLinkImageUrlChanged = function() {
    document.getElementById("UsefulLinkImage").src = document.getElementById("UsefulLinkImageUrl").value;
}

var saveAddedUsefulLink = function () {
    var usefulLinkToAdd =
        {
            OwnerUserID: userId,
            Name: $("#UsefulLinkName").val(),
            Url: $("#UsefulLinkUrl").val(),
            ImageUrl: $("#UsefulLinkImageUrl").val(),
            Comment: $("#UsefulLinkComment").val()
        };
    $.post("api/UsefulLinks/AddUsefulLink", usefulLinkToAdd).success(onSuccessfullUsefulLinkAdding).fail(onFailedUsefulLinkAdding);
}

var onSuccessfullUsefulLinkAdding = function () {
    $("#errors").empty();
    window.location.reload(true);
}

var onFailedUsefulLinkAdding = function (error) {
    var errors = [];
    for (var key in error.responseJSON.ModelState) {
        for (var i = 0; i < error.responseJSON.ModelState[key].length; i++) {
            errors.push(error.responseJSON.ModelState[key][i])
        }
    }
    var message = errors.join('<br />');
    $("#errors").html(message);
}

var removeUsefulLink = function (id) {
    $.post("api/UsefulLinks/RemoveUsefulLink/" + id, id).success(
        function(){
            window.location.reload(true);
        });
}