jQuery(document).ready(function ($) {
    $(".clickable-row").click(function() {
        window.location = $(this).data("href");
    });
    $("#resetFilterButton").click(function () {
        $("#SearchText").val("");
        $("#FilteredBranch").val("Alle");
    });
});