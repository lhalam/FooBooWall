var usercontext = $("#userStatsCanvas").get(0).getContext("2d");
var userres = {
    labels: userlabels.split(", "),
    datasets: [{
        label: "User Stats",
        data: userdata.split(", ")
    }]
};

var userLineChart = new Chart(usercontext).Bar(userres, { responsive: true });

var eventcontext = $("#eventStatsCanvas").get(0).getContext("2d");

var eventres = {
    labels: eventlabels.split(", "),
    datasets: [{
        label: "Event Stats",
        data: eventdata.split(", ")
    }]
};
var eventLineChart = new Chart(eventcontext).Bar(eventres, { responsive: true });