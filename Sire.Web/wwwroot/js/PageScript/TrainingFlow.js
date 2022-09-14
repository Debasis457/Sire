var Question = [];
var QuestionResult = [];
$("#id").val("0");

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
function GetTask() {
    debugger;
    $("#Tasks").empty();

    $("#Tasks").load("/TrainingFlow/GetTasks/", function () {

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