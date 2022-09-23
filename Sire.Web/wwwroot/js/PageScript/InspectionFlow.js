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
    $("#Operator_Supplied_Content").load("/InspectionFlow/FileUpload/", function () {

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
    //debugger;
    $("#Response").empty();
    /*var id = 1;*/
    $("#Response").load("/InspectionFlow/GetQuestionResponse/" + Id, function () {
        $(document).ready(function () {
            if ($(".QuestionContainer").text().trim() == "") {
                $('#selResponseType > option:gt(0)').each(function () {
                    var type = $(this).val();
                    $('.QuestionContainer').append($('.' + type).html());
                    generateIds();
                });
            } 
            $('.btn-copy').click(function () {
                var type = $('#selResponseType').val();
                if (type != "") {
                    $('.QuestionContainer').append($('.' + type).html());
                    generateIds();
                }
            });
        });
    });
}

function generateIds() {
    $('.QuestionContainer').last('div.table-responsive').find('input,textarea').each(function () {
        var index = $(this).closest('div.table-responsive').index();
        if ($(this).attr('type') == 'checkbox') {
            //$(this).addClass('form-control checkbox-inline');
            $(this).attr('id', $(this).attr('type') + '_' + index);
            $(this).next('label').attr('for', $(this).attr('type') + '_' + index);
        }
        else if ($(this).attr('type') == 'radio') {
            //$(this).addClass('form-control');
            $(this).attr('id', $(this).attr('type') + '_' + $(this).val() + '_' + index);
            $(this).attr('name', $(this).attr('name') + '_' + index);
            $(this).next('label').attr('for', $(this).attr('type') + '_' + $(this).val() + '_' + index);
        }
        else {
            $(this).attr('id', 'Response_Comment_' + index);
        }
    });
}

function saveResponseData(questionId) {
    var responseDataArr = [];
    $('.QuestionContainer > div.table-responsive').each(function () {
        var responseData = {};
        responseData.Inspection_Question_id = questionId;
        responseData.Id = $(this).attr('data-id');
        responseData.ResponseType = $(this).attr('data-responsetype');
        responseData.Is_Answerable = $(this).find('input[type="checkbox"]')[0].checked;
        if (responseData.Is_Answerable == false) {
            responseData.Response_Value = $(this).find('input[type="radio"]:checked').val();
        }
        responseData.Response_Comment = $(this).find('textarea').val();
        responseDataArr.push(responseData);
    });
    $.ajax({
        type: "POST",
        url: "/InspectionFlow/SaveQuestionResponse/",
        dataType: "json",
        data: { data: responseDataArr },

        success: function (data) {
            alert(data);
            GetQuestionResponse(questionId);
        },
        error: function (ex) {
            alert("Failed to Save" + ex);
        }
    });
    debugger
}

function CompleteInspection(id) {
    $.ajax({
        type: "GET",
        url: "/InspectionFlow/CompleteInspectionQuestion/" + id,
        success: function (data) {
            alert("Assessment Completed");
            if ('referrer' in document) {
                window.location = document.referrer;
            }
            else {
                window.history.back();
            }
            //window.location = 'InspectionQuestion/' + id;           

            //alert("Inspection Completed");
            //window.location = window.location;
            //$('#completeInspection').attr('disabled', 'disabled');
            window.onkeydown = null;
            window.onfocus = null;
        }
    });
}