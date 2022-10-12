$(document).ready(function () {
    GetQuestion($('#hdnInspectionId').val());
});
function GetQuestion(id) {
    $.ajax({
        type: "GET",
        url: "/InspectionQuestion/GetQuestionLibrary?id=" + id,

        success: function (r) {
            $("#QueLibrary").html(r);
        }
    });
}
function GetApplicableQuestions(id) {
    $.ajax({
        type: "GET",
        url: "/InspectionQuestion/GetApplicableQuestions?id=" + id,

        success: function (r) {
            $("#ApplicableQue").html(r);
        }
    });
}
function GetRankBasedQuestion(id) {
    $.ajax({
        type: "GET",
        url: "/InspectionQuestion/GetRankBasedQuestions?id=" + id,

        success: function (r) {
            $("#RankBaseQue").html(r);
        }
    });
}
function GetCIVQuestion(id) {
    $.ajax({
        type: "GET",
        url: "/InspectionQuestion/GetPredictedCIVQuestions?id=" + id,

        success: function (r) {
            $("#CIVQ").html(r);
        }
    });
}
function GetTagQuestion(id) {
    $.ajax({
        type: "GET",
        url: "/InspectionQuestion/GetTaggedQuestions?id=" + id,

        success: function (r) {
            $("#TagQuestion").html(r);
        }
    });
}