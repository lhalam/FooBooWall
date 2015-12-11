      
$(function () {
    $("#commentbutton").click(function () {
        if (validateComment()) {
            var newComment =
            {
                UserId: $("#useridinfo").val(),
                EventId: $("#eventidinfo").val(),
                //UserId:userId,
                //EventId:eventId,
                Text: $("#newcomment").val(),
            };
            $.post(AddCommentUrl, newComment).success(function (data) {
            alert("Comment added successfully!")
            });
           // document.location.href = ""; 
        }
    })
});

 
function validateComment() {
    if ($("#newcomment").val().length ==0) {
        return false;
    }else
     return true;
}