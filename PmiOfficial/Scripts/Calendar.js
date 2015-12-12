$(function () {
    $.get('/api/EventApi/Get')
    .then(function (httpData) {
        $("#calendar").fullCalendar({
            events: httpData,
            eventClick: function (calEvent, jsEvent, view) {
                location.href = '/Event?eventId=' + calEvent.id + '&userId=' + loggedUserID;
            },
            eventColor: '#9575CD'
            
        });
    });
    
});


$('#prev-month-btn').click(function () {
    $('#calendar').fullCalendar('prev');
});

$('#next-month-btn').click(function () {
    $('#calendar').fullCalendar('next');
});