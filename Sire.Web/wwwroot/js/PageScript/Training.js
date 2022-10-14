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

function GetQuestionBySection(chepter,id) {
    debugger
    var traningId = $("#hdnTraningId").val();
    $.ajax({
        type: "GET",
        url: "/Training/GetQuestionBySection?id=" + id + "&traningId=" + traningId + "&chepter=" + chepter,

        success: function (r) {
            $(".bindPartialQuetion").html(r);
        }
    });
}

function GetRankBasedQuestionBySection(chepter , id) {
    debugger
    var trainingId = $("#hdnTraningId").val();
    var rankGroupId = $("#hdnRankGroupId").val();
    $.ajax({
        type: "GET",
        url: "/Training/GetRankBasedQuestionsBySection?id=" + id + "&rankGroupId=" + rankGroupId + "&trainingId=" + trainingId + "&chepter=" + chepter ,

        success: function (r) {
            $(".bindPartialQuetion").html(r);
        }
    });
}

function GetApplicableQuestionBySection(chepter, id) {
    debugger
    var traningId = $("#hdnTraningId").val();
    $.ajax({
        type: "GET",
        url: "/Training/GetApplicableQuestionBySection?id=" + id + "&traningId=" + traningId + "&chepter=" + chepter,

        success: function (r) {
            $(".bindApplicableQuetion").html(r);
        }
    });
}

$(document).ready(function () {
    debugger;
    $('#Rank_Id').change(function () {
        debugger;
        GetRankBasedQuestion($('#hdnTraningId').val(), $(this).val());
    });
});

function GetRankBasedQuestion(id, rankGroupId) {
    $.ajax({
        type: "GET",
        url: "/TrainingQuestion/GetRenkBaseQuestion?id=" + id,
        data: { rankGroupId: rankGroupId },
        success: function (r) {
            $("#RankBaseQue").html(r);
        }
    });
}
