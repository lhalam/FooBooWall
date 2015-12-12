$(function () {
    if (typeof userName != 'undefined')
    {
        // Ссылка на автоматически-сгенерированный прокси хаба
        var chat = $.connection.chatHub;
        // Объявление функции, которая хаб вызывает при получении сообщений
        chat.client.addMessage = function (name, message) {
            // Добавление сообщений на веб-страницу 
            $('#globalChat').find('#divMessage').append('<div class="message"><span>' + name + '</span>: ' + message + '</div>');

            var height = $('#globalChat').find('#divMessage')[0].scrollHeight;
            $('#globalChat').find('#divMessage').scrollTop(height);
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
                    var messageModel = {
                        SenderName: $('#chatHhUserName').val(),
                        Text: $('#globalMessage').val()
                    };
                    $.post('/api/Message/sendGlobal', messageModel)
                    .done(function () {
                        chat.server.send($('#username').val(), $('#globalMessage').val());
                        $('#globalMessage').val('');
                    });
                }
            });

            $.get('/api/Message/getGlobal')
            .done(function (data) {
                AppendMessageHistory(data, '#globalChat')
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
    if (typeof userName != 'undefined') {
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
                var height = $('#globalChat').find('#divMessage')[0].scrollHeight;
                $('#globalChat').find('#divMessage').scrollTop(height);
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
            $("[id='" + userName + "']").remove();

            var ctrId = "[id='private_" + userName + "']";
            $(ctrId).remove();
            var disc = $('<div class="disconnect">"' + userName + '" logged off.</div>');
            $(disc).hide();
            $('#divusers').prepend(disc);
            $(disc).fadeIn(200).delay(2000).fadeOut(200);
        }

        chat.client.sendPrivateMessage = function (fromUserName, chatWindowID, message) {

            var ctrId = "[id='private_" + chatWindowID + "']";

            var wasAlreadyCreated = true;
            if ($(ctrId).length == 0) {
                wasAlreadyCreated = false;
                createPrivateChatWindow(chat, ctrId, chatWindowID);
            }
            if ($(ctrId).is(':hidden'))
            {
                $(ctrId).show();
            }
            
            if (wasAlreadyCreated) {
                $(ctrId).find('#divMessage').append('<div class="message"><span>' + fromUserName + '</span>: ' + message + '</div>');

                var height = $(ctrId).find('#divMessage')[0].scrollHeight;
                $(ctrId).find('#divMessage').scrollTop(height);
            }
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
    var userName = $('#chatHhUserName').val();
    var code = "";
    if (userName == name) {
        code = $('<div class="loginUser">' + name + "</div>");
    }
    else {
        if ($("[id='" + name + "']").length == 0) {
            code = $('<a id="' + name + '" class="user" >' + name + '<a>');
            $(code).click(function () {
                if (userName != name)
                    OpenPrivateChatWindow(chatHub, name);
            });
        }
        else {
            return;
        }
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

function OpenPrivateChatWindow(chatHub, name) {
    var ctrId = "[id='private_" + name + "']"
    if ($(ctrId).length > 0) {
        if ($(ctrId).is(':hidden')) {
            $(ctrId).show();
        }
        return;
    }
    createPrivateChatWindow(chatHub, ctrId, name);
}

function createPrivateChatWindow(chatHub, ctrId, name) {

    var div = '<div id="private_' + name + '" class="ui-widget-content panel panel-primary draggable" rel="0">' +
               '<div class="header panel-heading">' +
                  '<span id="imgDelete" class="glyphicon glyphicon-remove pull-right"></span>' +

                   '<span class="selText" rel="0">' + name + '</span>' +
               '</div>' +
               '<div id="divMessage" class="messageArea">' +

               '</div>' +
               '<div class="buttonBar panel-footer form-inline">' +
                  '<input id="txtPrivateMessage" class="msgText form-control" type="text"   />' +
                  '<input id="btnSendMessage" class="btn btn-primary" type="button" value="Send"   />' +
               '</div>' +
            '</div>';
    var $div = $(div);
    // DELETE BUTTON IMAGE
    $div.find('#imgDelete').click(function () {
        $(ctrId).hide();
    });
    // Send Button event
    $div.find("#btnSendMessage").click(function () {

        $textBox = $div.find("#txtPrivateMessage");
        var msg = $textBox.val();
        if (msg.length > 0) {
            messageModel = {
                SenderName: $('#chatHhUserName').val(),
                RecepientName: name,
                Text: msg
            };
            $.post("/api/Message/sendPrivate", messageModel)
            .done(function (response) {
                chatHub.server.sendPrivateMessage(name, msg);
                $textBox.val('');
            });
        }
    });
    // Text Box event
    $div.find("#txtPrivateMessage").keypress(function (e) {
        if (e.which == 13) {
            $div.find("#btnSendMessage").click();
        }
    });

    var senderName = name;
    var recepientName = $('#chatHhUserName').val();
    $.get('/api/Message/getPrivate?senderName=' + senderName + '&recepientName=' + recepientName)
    .done(function (data) {
        AppendMessageHistory(data, ctrId);
    });
    AddDivToContainer($div);
}

function AppendMessageHistory(messages, ctrId) {
    messages.forEach(function (message) {
        $(ctrId).find('#divMessage').append('<div class="message"><span>' + message.SenderName + '</span>: ' + message.Text + '</div>');
    });

    var height = $(ctrId).find('#divMessage')[0].scrollHeight;
    $(ctrId).find('#divMessage').scrollTop(height);
}

function AddDivToContainer($div) {
    $('#divContainer').prepend($div);

    $div.draggable({

        handle: ".header",
        stop: function () {

        }
    });
}

