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