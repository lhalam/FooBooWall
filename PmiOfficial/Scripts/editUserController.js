
<<<<<<< HEAD
=======
$(function () {

    var chat = $.connection.chatHub;

    chat.client.addMessage = function (name, message) {
        $('#chatroom').append('<p><b>' + htmlEncode(name)
            + '</b>: ' + htmlEncode(message) + '</p>');
    };

    chat.client.onConnected = function (id, userName, allUsers) {

        $('#chatHhUserID').val(id);
        $('#chatHhUserName').val(userName);

        $('#hdId').val(id);
        $('#username').val(userName);
        $('#header').html('<h5>Hello, ' + userName + '</h5>');

        for (i = 0; i < allUsers.length; i++) {

            AddUser(chat, allUsers[i].ConnectionId, allUsers[i].Name);
        }
    }

    chat.client.onNewUserConnected = function (id, name) {

        AddUser(chat, id, name);
    }

    chat.client.onUserDisconnected = function (id, userName) {
        $('#' + id).remove();

        var ctrId = 'private_' + id;
        $('#' + ctrId).remove();
        var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');
        $(disc).hide();
        $('#divusers').prepend(disc);
        $(disc).fadeIn(200).delay(2000).fadeOut(200);
    }

    chat.client.sendPrivateMessage = function (windowId, fromUserName, message) {

        var ctrId = 'private_' + windowId;

        if ($('#' + ctrId).length == 0) {
            createPrivateChatWindow(chat, windowId, ctrId, fromUserName);
        }

        $('#' + ctrId).find('#divMessage').append('<div class="message"><span>' + fromUserName + '</span>: ' + message + '</div>');

        var height = $('#' + ctrId).find('#divMessage')[0].scrollHeight;
        $('#' + ctrId).find('#divMessage').scrollTop(height);

    }

    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {
            chat.server.send($('#username').val(), $('#message').val());
            $('#message').val('');
        });
        registerEvents(chat);
        chat.server.connect(userName, loggedUserID);

    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function AddUser(chatHub, id, name) {
    var userId = $('#chatHhUserID').val();
    var code = "";
    if (userId == id) {

        code = $('<div class="loginUser">' + name + "</div>");
    }
    else {
        code = $('<a id="' + id + '" class="user" >' + name + '<a>');
        $(code).click(function () {
            var id = $(this).attr('id');
            if (userId != id)
                OpenPrivateChatWindow(chatHub, id, name);
        });
    }
    $("#divusers").append(code);
}

function registerEvents(chatHub) {
    var isToggleChatButtonToggled = true;

    $('#toggleChatButton').click(function () {
        if (isToggleChatButtonToggled) {
            $('#toggleChatButton').html('<span class="glyphicon glyphicon-minus"></span>');
            $('#toggleChatButtonDiv').animate({ right: 240 });
            $('#chatMultUsersDiv').animate({ right: 0 });
        }
        else {
            $('#toggleChatButton').html('<span class="glyphicon glyphicon-list"></span>');
            $('#toggleChatButtonDiv').animate({ right: 0 });
            $('#chatMultUsersDiv').animate({ right: -240 });
        }
        isToggleChatButtonToggled = !isToggleChatButtonToggled;
    });
}

function OpenPrivateChatWindow(chatHub, id, userName) {
    var ctrId = 'private_' + id;
    if ($('#' + ctrId).length > 0) return;
    createPrivateChatWindow(chatHub, id, ctrId, userName);
}

function createPrivateChatWindow(chatHub, userId, ctrId, userName) {

    var div = '<div id="' + ctrId + '" class="ui-widget-content draggable" rel="0">' +
               '<div class="header">' +
                  '<div  style="float:right;">' +
                      '<img id="imgDelete"  style="cursor:pointer;" src="/Images/delete.png"/>' +
                   '</div>' +

                   '<span class="selText" rel="0">' + userName + '</span>' +
               '</div>' +
               '<div id="divMessage" class="messageArea">' +

               '</div>' +
               '<div class="buttonBar">' +
                  '<input id="txtPrivateMessage" class="msgText" type="text"   />' +
                  '<input id="btnSendMessage" class="submitButton button" type="button" value="Send"   />' +
               '</div>' +
            '</div>';
    var $div = $(div);
    // DELETE BUTTON IMAGE
    $div.find('#imgDelete').click(function () {
        $('#' + ctrId).remove();
    });
    // Send Button event
    $div.find("#btnSendMessage").click(function () {

        $textBox = $div.find("#txtPrivateMessage");
        var msg = $textBox.val();
        if (msg.length > 0) {

            chatHub.server.sendPrivateMessage(userId, msg);
            $textBox.val('');
        }
    });
    // Text Box event
    $div.find("#txtPrivateMessage").keypress(function (e) {
        if (e.which == 13) {
            $div.find("#btnSendMessage").click();
        }
    });
    AddDivToContainer($div);
}

function AddDivToContainer($div) {
    $('#divContainer').prepend($div);

    $div.draggable({

        handle: ".header",
        stop: function () {

        }
    });
}
>>>>>>> e69b3341a9f15008c7757579401cac5d21d4a2a2

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