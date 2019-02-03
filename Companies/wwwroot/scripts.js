jQuery(document).ready(function ($) {
    $(".clickable-row").click(function() {
        window.location = $(this).data("href");
    });
    $("#resetFilterButton").click(function () {
        $("#TextContains").val("");
        $("#FilteredBranch").val("Alle");
    });
});