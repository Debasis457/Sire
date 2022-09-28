var Question = [];
var QuestionResult = [];
$("#id").val("0");

function GetQuestion() {
    debugger;
    $("#QueLibrary").empty();
    $("#QueLibrary").load("/TrainingQuestion/GetQuestion/" , function () {

    });

}
function GetRenkBaseQuestion() {
    debugger;
    $("#RankBaseQue").empty();

    $("#RankBaseQue").load("/TrainingQuestion/GetRenkBaseQuestion/", function () {

    });
}
function GetApplicableQuestions() {
        debugger;
    $("#ApplicableQue").empty();

    $("#ApplicableQue").load("/TrainingQuestion/GetApplicableQuestions/", function () {

        });
    }

    function GetCIVQquestion() {
        debugger;
        $("#CIVQ").empty();

        $("#CIVQ").load("/TrainingQuestion/GetCIVQquestion/", function () {

        });
    }

function GetTagQuestion() {
        debugger;
        $("#TagQuestion").empty();

        $("#TagQuestion").load("/TrainingQuestion/GetTagQuestion/", function () {

        });

}