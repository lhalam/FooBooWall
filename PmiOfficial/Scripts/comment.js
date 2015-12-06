      
$(function () {
    $("#commentbutton").click(function () {
        if (validateComment()) {
            $.post("/api/Event/AddComment",
                {
                    EventId: $("#eventidinfo").val(),
                    UserId: $("#useridinfo").val(),
                    Text: $("#newcomment").val()
                });
            alert("User registered successfully!");
            document.location.href = ""; 
        }
    })
});

   
function validateComment() {
    if ($("#newcomment").val().length ==0) {
        return false;
    }else
     return true;
}