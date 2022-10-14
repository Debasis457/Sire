

$(document).ready(function () {
   
    GetTrainingList();
});

function GetTrainingList() {
  /*  debugger;*/
   /* $("#TrainingStatus").empty();*/
    $("#TrainingStatus").load("/VesselDetails/GetTrainingList/", function () {
       
    });

}