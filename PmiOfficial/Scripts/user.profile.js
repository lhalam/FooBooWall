﻿$(document).on('click', '#close-preview', function () {
    $('.image-preview').popover('hide');
    // Hover befor close the preview
    $('.image-preview').hover(
        function () {
            $('.image-preview').popover('show');
        },
         function () {
             $('.image-preview').popover('hide');
         }
    );
});

$(function () {

    // Create a button to close the preview window

    var closebtn = $('<button/>', {
        type: "button",
        text: 'x',
        id: 'close-preview',
        style: 'font-size: initial;',
    });
    closebtn.attr("class", "close pull-right");

    // Set the popover default content

    $('.image-preview').popover({
        trigger: 'manual',
        html: true,
        title: "<strong>Image Preview:</strong>" + $(closebtn)[0].outerHTML,
        content: "No image found!",
        placement: 'bottom'
    });

    // Clear event

    $('.image-preview-clear').click(function () {
        $('.image-preview').attr("data-content", "").popover('hide');
        $('.image-preview-filename').val("");
        $('.image-preview-clear').hide();
        $('.image-preview-input input:file').val("");
        $(".image-preview-input-title").text("Browse");
    });

    $(".image-preview-input input:file").change(function () {

        //Create the preview image (width and height set to fit in popover)

        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });

        var file = this.files[0];
        var reader = new FileReader();

        // Set preview image to the popover data-content

        reader.onload = function (e) {
            $(".image-preview-input-title").text("Modify");
            $(".image-preview-filename").val(file.name);
            img.attr('src', e.target.result);
            $(".image-preview").attr("data-content", $(img)[0].outerHTML).popover("show");
        }
        reader.readAsDataURL(file);
    });
});