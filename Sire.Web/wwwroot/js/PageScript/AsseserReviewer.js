$("#btnSubmitAssRew").click(function () {
    debugger
    var CheckedList = [];
    $("#ASSREWDetails input[type=checkbox]:checked").each(function () {
        debugger
        var row = $(this).closest("tr")[0];
        var obj = { Inspection_Id: $("#hdninspectionId").val(), question_Id: row.cells[0].innerHTML.trim(), assessor_Id: $("#AssesorId option:selected").val(), reviewer_Id: $("#ReviewerId option:selected").val() }

        CheckedList.push(obj);
    });

    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(CheckedList),
        contentType: 'application/json',
        url: "/AssesorReviewer/AddEdit",
        success: function (r) {
            alert(r + " record(s) inserted.");
        }
    });
});

$("#btnSubmitAss").click(function () {
    debugger
    if ($('#AssesorId').val() == "") {
        swal("warning", "Please Select Assesor", "warning");
        window.onkeydown = null;
        window.onfocus = null;
    }
    else
        if ($(".quetionlist").is(':checked')) {
            var CheckedList = [];
            $("#ASSREWDetailsBody input[type=checkbox]:checked").each(function () {
                var ass = $("#AssesorId option:selected").val() == "" || $("#AssesorId option:selected").val() == null ? 0 : $("#AssesorId option:selected").val();
                var rew = $("#ReviewerId option:selected").val() == "" || $("#ReviewerId option:selected").val() == null ? 0 : $("#ReviewerId option:selected").val();

                var row = $(this).closest("tr")[0];
                var obj = { IsAssesor: true, Inspection_Id: $("#hdninspectionId").val() == '' ? null : $("#hdninspectionId").val(), question_Id: row.cells[0].innerHTML.trim(), assessor_Id: ass, reviewer_Id: rew }
                CheckedList.push(obj);
            });

            $.ajax({
                type: "POST",
                dataType: "json",
                data: JSON.stringify(CheckedList),
                contentType: 'application/json',
                url: "/AssesorReviewer/AddEdit",
                success: function (r) {
                    swal("Assesor Added Successfully");
                    window.onkeydown = null;
                    window.onfocus = null;
                    GetQuestionBySection($("#hdnsectionId").val());
                }
            });
        }

        else {
            swal("warning", "Please Select Question", "warning");
            window.onkeydown = null;
            window.onfocus = null;
        }
});

$("#btnSubmitRew").click(function () {
    if ($('#ReviewerId').val() == "") {
        swal("warning", "Please Select Reviewer", "warning");
        window.onkeydown = null;
        window.onfocus = null;
    }
    if ($(".quetionlist").is(':checked')) {
        var CheckedList = [];
       
        $("#ASSREWDetailsBody input[type=checkbox]:checked").each(function () {
            var ass = $("#AssesorId option:selected").val() == "" || $("#AssesorId option:selected").val() == null ? 0 : $("#AssesorId option:selected").val();
            var rew = $("#ReviewerId option:selected").val() == "" || $("#ReviewerId option:selected").val() == null ? 0 : $("#ReviewerId option:selected").val();
            debugger
            var row = $(this).closest("tr")[0];
            var obj = { IsAssesor: false, Inspection_Id: $("#hdninspectionId").val() == '' ? null : $("#hdninspectionId").val(), question_Id: row.cells[0].innerHTML.trim(), assessor_Id: ass, reviewer_Id: rew }
            CheckedList.push(obj);
        });

        $.ajax({
            type: "POST",
            dataType: "json",
            data: JSON.stringify(CheckedList),
            contentType: 'application/json',
            url: "/AssesorReviewer/AddEdit",
            success: function (r) {
                swal("Reviewer Added Successfully");
                window.onkeydown = null;
                window.onfocus = null;
                GetQuestionBySection($("#hdnsectionId").val());
            }
        });
    }
    else {
        swal("warning", "Please Select Question", "warning");
        window.onkeydown = null;
        window.onfocus = null;
    }
});

function SetAssRew() {
    debugger
    ASS_REW_Result = [];
    var $rows = $('#ASSREWDetails').find('tbody tr');
    if ($rows.length > 0) {
        isCheck = true;
        $.each($rows, function (key, row) {
            var selval = $("#PIQId_" + key).val();
            var responsetype = $("#PIQId_" + key).attr("data-responsetype");
            if (selval != "" && selval != null && selval != undefined && responsetype == "Dropdown") {
                var seltext = $("#PIQId_" + key + " option:selected").text();
                var seldataId = $("#PIQId_" + key + " option:selected").attr("data-id");
                var seldatadesc = $("#PIQId_" + key + " option:selected").attr("data-desc");
                var ID_Q = $("#PIQId_" + key + " option:selected").attr("data-id_q");

                if (seltext != "") {

                    var myjson = {
                        //"PIQ_HVPQId": key,
                        //"PIQ_HVPQval": selval,
                        //"PIQ_HVPQtext": seltext,
                        //"PIQ_HVPQ_ID_SUB_Q": seldataId,
                        //"PIQ_HVPQ_Q_Desc": seldatadesc,
                        //"IsType": "PIQ",
                        //"ID_Q": ID_Q,
                        vessel_Id: parseInt(vessale),
                        piq_Hvpq_Id: parseInt(ID_Q),
                        response: seldataId
                    }
                    ASS_REW_Result.push(myjson);
                }
            }
            else {
                var isSelect_ = $("#PIQId_" + key).prop("checked");
                var seldataId = null;
                var seldatadesc = $("#PIQId_" + key).attr("data-desc");
                var ID_Q = $("#PIQId_" + key).attr("data-id_q");
                if (isSelect_ == true) {
                    seldataId = $("#PIQId_" + key).attr("data-yes");
                }
                else {
                    seldataId = $("#PIQId_" + key).attr("data-no");

                }
                var myjson = {
                    //"PIQ_HVPQId": key,
                    //"PIQ_HVPQval": isSelect_,
                    //"PIQ_HVPQtext": isSelect_,
                    //"PIQ_HVPQ_ID_SUB_Q": seldataId,
                    //"PIQ_HVPQ_Q_Desc": seldatadesc,
                    //"IsType": "PIQ",
                    //"ID_Q": ID_Q,
                    vessel_Id: parseInt(vessale),
                    piq_Hvpq_Id: parseInt(ID_Q),
                    response: seldataId
                }
                PIQ_HVPQResult.push(myjson);

            }
        });

        return PIQ_HVPQResult;
    }
}

function aaaaa(id) {
    $.ajax({
        type: "GET",
        url: "/Inspection/GetQuestionBySection/" + id,
        success: function (r) {
            $(".bindPartialQuetion").html(r);
        }
    });
}

function GetInspectionApplicableQuestionBySection(id) {
    $.ajax({
        type: "GET",
        url: "/Inspection/GetInspectionApplicableQuestionBySection/" + id,
        success: function (r) {
            $(".bindPartialQuetion").html(r);
        }
    });
}

function GetQuestionBySection(id) {
    debugger
    $("#hdnsectionId").val(id);
    $.ajax({
        type: "GET",
        url: "/AssesorReviewer/GetQuestionBySection/",
        data: { id: id, inspectionId: $("#hdninspectionId").val() },
        success: function (r) {
            $(".bindPartialQuetion").html(r);
        }
    });
}

function CompleteInspection(id) {
    $.ajax({
        type: "GET",
        url: "/InspectionQuestion/CompleteInspection/" + id,
        success: function () {
            alert("Inspection Completed");
            window.location = window.location;
            //$('#completeInspection').attr('disabled', 'disabled');
            window.onkeydown = null;
            window.onfocus = null;
        }
    });
}

$("#selectAll").click(function () {
    $(".quetionlist").prop('checked', $(this).prop('checked'));
});


