var Question = [];
var QuestionResult = [];


function GetQueCheckList(Id) {
    debugger;
    $("#InspCheckList").empty();
    $("#InspCheckList").load("/InspectionFlow/GetQueCheckList/" + Id, function () {
        //$('#InspCheckList').DataTable({
        //    'lengthChange': true,
        //    'searching': true,
        //    'ordering': false,
        //    'info': true,
        //    'autoWidth': false,
        //    "scrollY": "520px",
        //    "scrollX": false,
        //    "scrollCollapse": true,
        //    "paging": false,
        //    "responsive": true,
        //    "columnDefs": [
        //        { "width": "70%", "targets": 0 },
        //        { "width": "30%", "targets": 1 }
        //    ]

        //});
        //$('.select2').select2();
        //$('.select2').width("100%");


    });

}

//$('.btn-copy').click(function () {
//    $('.example-1').append($('.example-2').html());

//});



function GetResponseList() {
    debugger;
    
    /*if ($check.prop('checked') == true) {*/
    $("#Reversetab").load("/InspectionFlow/GetQuestionResponse/", function () {
       cbbx
    });
    //}
    //else {
    //    swal("warning", "Please Select Record", "warning");
    //    window.onkeydown = null;
    //    window.onfocus = null;
    //}

}

function GetOpContent() {
    debugger;
    $("#Operator_Supplied_Content").empty();

    $("#Operator_Supplied_Content").load("/InspectionFlow/GetOpContent/", function () {

    });
}


function InspUploadFile() {
    debugger;
    $("#Operator_Supplied_Content").load("/InspectionFlow/FileUpload/" , function () {

    });
}


function GetQuestionDetails(Id) {
    debugger;
    $("#Guidance").empty();
    /*var id = 1;*/
    $("#Guidance").load("/InspectionFlow/GetQuestionDetails/" + Id, function () {
        debugger;
        $(document).ready(function () {
            $("#ShortTag").click(function () {
                $("#Tag").toggle();
            });
            $("#Objective").click(function () {
                $("#Obj").toggle();
            });
            $("#Ind_Guidance").click(function () {
                $("#Ind").toggle();
            });
            $("#Inspection_Guidance").click(function () {
                $("#Insp_Guidance").toggle();
            });
            $("#Suggested").click(function () {
                $("#Sia").toggle();
            });
            $("#ExpectedEvidence").click(function () {
                $("#Exp").toggle();
            });
            $("#Potential").click(function () {
                $("#Pfn").toggle();
            });
        });
    });
}

function GetQuestionResponse(Id) {
    debugger;
    $("#Response").empty();
    /*var id = 1;*/
    $("#Response").load("/InspectionFlow/GetQuestionResponse/" + Id, function () {
        debugger;
        $(document).ready(function () {

            $('.btn-copy').click(function () {
                var type = $(this).text();
                $('.QuestionContainer').append($('.' + type).html());
                $('.' + type).empty();
            });
        });
    });
}