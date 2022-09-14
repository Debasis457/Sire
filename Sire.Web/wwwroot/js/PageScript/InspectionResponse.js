var Question = [];
var QuestionResult = [];
var INS_RESPONSE = [];

$(document).ready(function () {
    debugger
    $('.select2').select2();
    $('.select2').width("100%");
    $(".hid").hide();

    $('#BADetails').DataTable({
        'lengthChange': true,
        'searching': true,
        'ordering': false,
        'info': true,
        "scrollCollapse": true,
        "paging": false
    });

    
});


 

function SetResponse(QId, HardwareResp, ProcessResp, HumanResp) {


    debugger
    INS_RESPONSE = [];
    var $rows = $('#HardwareDetails').find('tbody tr') && $('#ProcessDetails').find('tbody tr');

    if ($rows.length > 0) {

        isCheck = true;
        var HardResponse = $("input[name='Hardyes']:checked").val();
        var HBinComment = $("#HardBinComment").val();
        var HardGradComment = $("#HardGradComment").val();
        var ProcessResponse = $("input[name='Proyes']:checked").val();
        var ProBinComment = $("#ProBinComment").val();
        var ProGradComment = $("#ProGradComment").val();
        var HumGradComment = $("#HumGradComment").val();
        /*   $.each($rows, function () {*/
        debugger;



        if (HardwareResp == "Binary") {
            var HardJson = {

                Inspection_Question_id: QId,
                ResponseType: 0,
                Response_Value: HardResponse,
                Response_Comment: HBinComment
            }
        }
        else
            if (HardwareResp == "Graduated") {
                var HardJson = {

                    Inspection_Question_id: QId,
                    ResponseType: 0,
                    Response_Value: "",
                    Response_Comment: HardGradComment
                }
            }

            else
                if (HardwareResp == "None") {
                    var HardJson = {

                        Inspection_Question_id: QId,
                        ResponseType: 1,
                        Response_Value: "",
                        Response_Comment: ""
                    }
                }
        if (ProcessResp == "Binary") {
            var ProJson = {

                Inspection_Question_id: QId,
                ResponseType: 1,
                Response_Value: ProcessResponse,
                Response_Comment: ProBinComment
            }
        }
        else
            if (ProcessResp == "Graduated") {
                var ProJson = {

                    Inspection_Question_id: QId,
                    ResponseType: 1,
                    Response_Value: "",
                    Response_Comment: ProGradComment
                }
            }
            else
                if (ProcessResp == "None") {
                    var ProJson = {

                        Inspection_Question_id: QId,
                        ResponseType: 1,
                        Response_Value: "",
                        Response_Comment: ""
                    }

                }
                    if (HumanResp == "Binary") {
                        var HumJson = {

                            Inspection_Question_id: QId,
                            ResponseType: 2,
                            Response_Value: ProcessResponse,
                            Response_Comment: ProBinComment
                        }
                    }
                    else
                        if (HumanResp == "Graduated")
                        {
                            var HumJson = {

                                Inspection_Question_id: QId,
                                ResponseType: 2,
                                Response_Value: "",
                                Response_Comment: HumGradComment
                            }
                        }
                        else
                            if (HumanResp == "None")
                            {
                                var HumJson = {

                                    Inspection_Question_id: QId,
                                    ResponseType: 2,
                                    Response_Value: "",
                                    Response_Comment: ""
                                }
                            }




                    INS_RESPONSE.push(HardJson, ProJson, HumJson);

                    /* });*/

                    return INS_RESPONSE;
                }
    }






function ResponseValue(Id) {
    debugger;  
        $.ajax({
            url: '/InspectionFlow/PassQuestionResponse/',
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: { 'Id': Id},
            success: function (data) {
                console.log(data);
                console.log(data.id);
                console.log(data.length);
                debugger;
                var HardwareResp = "";
                var ProcessResp = "";
                var ResponseType = "";
                var HumanResp = "";
                var QId = 0;
                if (data.id > 0)
                {

                    debugger;
                    QId = data.id;
                 
                    HardwareResp = data.hardware_Response_Type;                  
                    ProcessResp = data.process_Response_Type;
                    HumanResp = data.human_Response_Type;

                    Result = SetResponse(QId, HardwareResp, ProcessResp, HumanResp);

                    console.log(Result);
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        data: JSON.stringify(Result),
                        contentType: 'application/json',
                        url: "/InspectionFlow/SaveResponse",
                        success: function (data) {
                            /*  alert(r + " record(s) inserted.");*/
                            debugger;
                            alert("Response Saved SuccessFully...")
                            swal("warning", "Response Saved SuccessFully...", "warning");
                            window.onkeydown = null;
                            window.onfocus = null;
                        }
                    });
                }
                else {
                    swal("warning", "Response Not Available...", "warning");
                    window.onkeydown = null;
                    window.onfocus = null;
                }
            }
        });
  
}

