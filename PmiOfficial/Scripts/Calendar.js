$(function () {
    $.get('/api/EventApi/Get')
    .then(function (httpData) {
        $("#calendar").fullCalendar({
            events: httpData,
            eventClick: function (calEvent, jsEvent, view) {
                location.href = '/Event?eventId=' + calEvent.id + '&userId=' + loggedUserID;
            },
            eventColor: '#378006'
        });
    });
    
});