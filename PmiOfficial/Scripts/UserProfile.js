var usefulLinkImageUrlChanged = function() {
    document.getElementById("UsefulLinkImage").src = document.getElementById("UsefulLinkImageUrl").value;
}

var saveAddedUsefulLink = function () {
    var UsefulLink =
        {
            UsefulLinkName: document.getElementById("UsefulLinkName").valueOf(),
            UsefulLinkName: document.getElementById("UsefulLinkUrl").valueOf(),
            UsefulLinkName: document.getElementById("UsefulLinkImageUrl").valueOf()
        };
    
    
}


/*$(document).ready(function () {
    $('#UsefulLinkImageUrl').change(function () {
        $('#UsefulLinkImage').src = this.value;
    });
});*/

/*var usefulLinkImageUrlChanged = function () {
    ("#UsefulLinkImage").src = ("#UsefulLinkImageUrl").valueOf();
}*/