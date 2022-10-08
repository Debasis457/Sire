var Question = [];
var QuestionResult = [];
$("#id").val("0");


$(document).ready(function () {
    GetQueCheckList($("#hdnQuestionId").val());
})

function GetQueCheckList(Id) { 
    debugger;
    $("#CheckList").empty();
    $("#CheckList").load("/TrainingFlow/GetQueCheckList/" + Id, function () {
       
    });

}
function GetQuestionDetails(Id) {  
    debugger;
    $("#Guidance").empty(); 
   
    $("#Guidance").load("/TrainingFlow/GetQuestionDetails/" + Id, function () {
      
    });
}
function GetTask(id) {
    debugger;
    $("#Tasks").empty();
    var trainingId = $("#hdnTraningId").val();
    $("#Tasks").load("/TrainingFlow/GetTasks/" + id + "&trainingId=" + trainingId, function () {
 
        if (trainingId == "0") {
            debugger;
            $("#taskButton").hide();
        } else {
            $("#taskButton").show();
        }
      
    });
} 

function GetOpContent() {
    debugger;
    $("#Operator_Supplied_Content").empty();

    $("#Operator_Supplied_Content").load("/TrainingFlow/GetOpContent/", function () {

    });
}

function UploadFile() {
    debugger;
    $("#Operator_Supplied_Content").load("/TrainingFlow/FileUpload/", function () {

    });

}