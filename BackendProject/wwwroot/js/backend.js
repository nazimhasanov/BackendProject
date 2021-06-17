let subInput;
$(document).on("click", "#button-subscribe", function () {
    $("#response-subscribe").empty()
    subInput = $("#Email-sucscribe").val()
    if (subInput.length > 1) {
        $.ajax({
            type: "Get",
            url: "Home/Subscribe",
            data: {
                "email": subInput

            },
            success: function (res) {
                $("#response-subscribe").append(res)
            }
        })
    }
  

})
$(document).ready(function () {
    let search;

    $(document).on("keyup", "#search-home-input", function () {
        search = $(this).val().trim();

        $(`#home-search #global-search`).remove();

        if (search.length > 0) {
            $.ajax({
                url: '/Home/Search?search=' + search,
                type: "Get",
                success: function (res) {

                    $(`#home-search`).append(res)
                }
            });
        }
    })
})
$(document).ready(function () {
    let search;
    $(document).on("keyup", "#search-event-input", function () {

        search = $(this).val().trim();

        $("#new-events").empty()

        if (search.length > 0) {
            $.ajax({
                url: '/Event/Search?search=' + search,
                type: "Get",
                success: function (res) {
                    $("#old-events").css("display", "none")
                    $("#pagination1").css("display", "none")
                    $("#new-events").append(res)
                }
            });
        }
        else {
            $("#old-events").css("display", "block")
            $("#pagination1").css("display", "block")
        }
    });
});
