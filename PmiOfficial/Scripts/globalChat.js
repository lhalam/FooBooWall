$(function () {
    if (userName)
    {
        // Ссылка на автоматически-сгенерированный прокси хаба
        var chat = $.connection.chatHub;
        // Объявление функции, которая хаб вызывает при получении сообщений
        chat.client.addMessage = function (name, message) {
            // Добавление сообщений на веб-страницу 
            $('#globalChat').find('#divMessage').append('<div class="message"><span>' + name + '</span>: ' + message + '</div>');
        };

        // Добавляем нового пользователя
        chat.client.onlineUserCount = function (count) {
            $("#userCount").text(count);
        }
        // Открываем соединение
        $.connection.hub.start().done(function () {

            $('#globalSendMessage').click(function () {
                if ($('#globalMessage').val() != '') {
                    // Вызываем у хаба метод Send
                    chat.server.send($('#username').val(), $('#globalMessage').val());
                    $('#globalMessage').val('');
                }
            });
        });
    }
});

// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

$(function () {
    if (userName) {
        var chat = $.connection.chatHub;


        chat.client.onConnected = function (id, userName, allUsers) {
            $('#globalTitle').click(function () {
                $('#globalChat')
                    .show()
                    .draggable({
                        handle: ".header",
                        stop: function () {
                        }
                    });
            });
            $('#globalChat').find('#imgDelete').click(function () {
                $('#globalChat').hide();
            });
            // установка в скрытых полях имени и id текущего пользователя
          //  $('#header').html('<h5>Hello, ' + userName + '</h5>');
          //  $("#userCount").text(allUsers.length);
            $('#chatHhUserID').val(id);
            $('#chatHhUserName').val(userName);

            $('#hdId').val(id);
            $('#username').val(userName);
            $('#header').html('<h5>Hello, ' + userName + '</h5>');

            for (i = 0; i < allUsers.length; i++) {

                AddUser(chat, allUsers[i].UserId, allUsers[i].Name);
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


        });
    }
   
});

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

function OpenPrivateChatWindow(chatHub, id, name) {
    var ctrId = 'private_' + id;
    if ($('#' + ctrId).length > 0) return;
    createPrivateChatWindow(chatHub, id, ctrId, name);
}

function createPrivateChatWindow(chatHub, userId, ctrId, name) {

    var div = '<div id="' + ctrId + '" class="ui-widget-content draggable" rel="0">' +
               '<div class="header">' +
                  '<div  style="float:right;">' +
                      '<img id="imgDelete"  style="cursor:pointer;" src="/Images/delete.png"/>' +
                   '</div>' +

                   '<span class="selText" rel="0">' + name + '</span>' +
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

            chatHub.server.sendPrivateMessage(name, msg);
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

