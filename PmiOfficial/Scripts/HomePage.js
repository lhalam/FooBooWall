$(".copy_rigths").on('click', function (ev) {
    if (!$("#left_dog").is(":visible")) {
        $("#left_dog").show();
        $("#right_dog").show();
        $(".slider").hide();
        $("#snop_song")[0].src += "&autoplay=1";
        ev.preventDefault();
        resizeColumns();
    }
    else {
        $(".slider").show();
        $("#left_dog").hide();
        $("#right_dog").hide();
        $("#snop_song")[0].src = "//www.youtube.com/embed/KlujizeNNQM?rel=0";
        ev.preventDefault();
        resizeColumns();
    }
});

var $myCols = $("#myColumns");

function resizeColumns() {
    var visibleCols = $myCols.children(":visible").length;

    var div = Math.floor(12 / visibleCols);
    var rem = 12 % visibleCols;

    var colSize = (rem === 0) ? div : 2;

    $myCols.children().removeClass().addClass('col-md-' + colSize);

}