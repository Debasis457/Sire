$(document).ready(function () {
    $("#Operator_Id").change(function () {
     
        $("#Vessel_Id").empty();
        debugger;
        $.ajax({
            type: "POST",
            url: "/Training/GetVessel/",
            dataType: "json",
            data: { id: $("#Operator_Id").val() },

            success: function (data) {


                debugger;
                $("#Vessel_Id").empty();
                $("#Vessel_Id").append('<option data-Id="' + 0 + '">Select</option>');
                if (data != null && data.length > 0) {
                    var k = 0;
                    $.each(data, function (i, items) {
                        debugger
                        k = k + 1;
                        console.log(items);
                        $("#Vessel_Id").append('<option value="' + items.id + '" data-Id="' + k + '">' + items.name + '</option>');
                    });

               }

            },
            error: function (ex) {
                alert("Failed to Vessel" + ex);
            }
        });
        return false;
    });
});
function GetQuestionBySection(id) {
    debugger
    var traningId = $("#hdnTraningId").val();
    $.ajax({
        type: "GET",
        url: "/Training/GetQuestionBySection?id=" + id + "&traningId=" + traningId,

        success: function (r) {
            $(".bindPartialQuetion").html(r);
        }
    });
}

function GetApplicableQuestionBySection(id) {
    debugger
    var traningId = $("#hdnTraningId").val();
    $.ajax({
        type: "GET",
        url: "/Training/GetQuestionBySection?id=" + id + "&traningId=" + traningId,

        success: function (r) {
            $(".bindPartialQuetion").html(r);
        }
    });
}

