var PIQ_HVPQ = [];
var PIQ_HVPQResult = [];
var ID_QArray = [];

$(document).ready(function () {
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

    GetPIQLoad();
});

function GetPIQLoad() {
    debugger;
    $("#PIQ").empty();
    $("#PIQ").load("/PIQHVPQ/GetPIQLoad/", function () {
        $('#PIQDetails').DataTable({
            'lengthChange': false,
            'searching': true,
            'ordering': false,
            'info': true,
            'autoWidth': false,
           /* "scrollY": "520px",*/
            "scrollX": false,
            "scrollCollapse": true,
            "paging": false,
            "responsive": true,
            //"columnDefs": [
            //    { "width": "40%", "targets": 0 },
            //    { "width": "60%", "targets": 1 }
            //]

        });
        $('.select2').select2();
        $('.select2').width("100%");

        if (PIQ_HVPQResult != null) {
            if (PIQ_HVPQResult.length > 0) {
                $.each(PIQ_HVPQResult, function (m, red) {
                    if (red.IsType == "PIQ") {
                        $("#PIQId_" + red.PIQ_HVPQId).val(red.PIQ_HVPQval);
                        $("#PIQId_" + red.PIQ_HVPQId).select2().trigger('change');

                        if (red.PIQ_HVPQtext == "YES") {
                            $("#yesNoChkid_" + red.PIQ_HVPQId).prop("checked", true);
                        }
                        if (red.PIQ_HVPQtext == "NO") {
                            $("#yesNoChkid_" + red.PIQ_HVPQId).prop("checked", false);
                        }

                    }
                });
            }
        }
        SetPIQHVPQ();

    });

}

function GetHVPQLoad() {

    $("#HVPQ").empty();
    $("#HVPQ").load("/PIQHVPQ/GetHVPQLoad/", function () {
        $('#HVPQDetails').DataTable({
            'lengthChange': true,
            'searching': true,
            'ordering': false,
            'info': true,
            'autoWidth': false,
            /* "scrollY": "520px",*/
            "scrollX": false,
            "scrollCollapse": true,
            "paging": false,
            "responsive": true,
            "columnDefs": [
                { "width": "50%", "targets": 0 },
                { "width": "50%", "targets": 1 }
            ]
        });
        $('.select2').select2();
        $('.select2').width("100%");

        //if (PIQ_HVPQResult != null) {
        //    if (PIQ_HVPQResult.length > 0) {
        //        $.each(PIQ_HVPQResult, function (m, red) {
        //            if (red.IsType == "HVPQ") {
        //                $("#HVPQId_" + red.PIQ_HVPQId).val(red.PIQ_HVPQval);
        //                $("#HVPQId_" + red.PIQ_HVPQId).select2().trigger('change');

        //                if (red.PIQ_HVPQtext == "YES") {
        //                    $("#hvyesNoChkid_" + red.PIQ_HVPQId).prop("checked", true);
        //                }
        //                if (red.PIQ_HVPQtext == "NO") {
        //                    $("#hvyesNoChkid_" + red.PIQ_HVPQId).prop("checked", false);
        //                }


        //            }
        //        });
        //    }
        //}
        // SetPIQHVPQ();

    });
}

function SetPIQHVPQ(vessale) {
    debugger
    PIQ_HVPQResult = [];
    var $rows = $('#PIQDetails').find('tbody tr');
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
                    PIQ_HVPQResult.push(myjson);
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

function ToggelYesNo(id, chkid) {
    var isSelect_ = $("#" + chkid).prop("checked");
    if (isSelect_) {
        var yesval = $("#" + chkid).attr("data-yes");
        $("#" + id).val(yesval);
        $("#" + id).select2().trigger('change');
    }
    else {
        var noval = $("#" + chkid).attr("data-no");
        $("#" + id).val(noval);
        $("#" + id).select2().trigger('change');
    }
}


function getbackGetPIQ() {
    $.ajax({
        url: '/Dashboard/GetResult/',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: '',
        success: function (data) {
            var VesselId = $("#VesselId").val();
            VesselId = VesselId.trim();
            $("#PIQView").hide();
            $("#HVPQView").hide();
            $("#BAView").hide();
            $(".hid").hide();

            if (VesselId != null && VesselId != "" && VesselId != undefined) {
                $("#PIQView").show();
                if (PIQ_HVPQ != null) {
                    if (PIQ_HVPQ.length > 0) {
                        $("#PIQDetails > tbody").empty();
                        var l = 0;
                        $.each(PIQ_HVPQ, function (i, rec) {
                            if (rec.IsType == "PIQ") {
                                var str = "<tr><td><div  class='qudiv'>" + rec.Q_Description + "</div></td><td><div>";
                                if (rec.DropDownLMaster != null) {
                                    var sel = "";
                                    if (rec.DropDownLMaster.length > 0) {
                                        $.each(rec.DropDownLMaster, function (k, ent) {
                                            sel = sel + "<option value='" + ent.ID_INT + "' data-id='" + ent.ID_SUB_Q + "' data-desc='" + rec.Q_Description + "' data-allow='" + rec.Is_Allow + "'  data-ID_Q='" + rec.ID_Q + "'>" + ent.SUB_Q + "</option>";
                                        });
                                    }
                                    str = str + "<select class='form-control inp select2' id='PIQId_" + l + "'>" + sel + "</select>";
                                }
                                str = str + "</div></td></tr>";
                                $("#PIQDetails > tbody").append(str);
                                $('.select2').select2();
                                $('.select2').width("100%");
                                var seldataAllow = $("#PIQId_" + l + " option:selected").attr("data-allow");
                                var selval = 0;
                                if (seldataAllow != null && seldataAllow != "") {
                                    if (seldataAllow.indexOf(',') >= 0) {
                                        var sptlAllow = seldataAllow.split(',');
                                        selval = sptlAllow[0];
                                    }
                                }
                                //if (selval > 0) {
                                //    $("#PIQId_" + l).val(selval);
                                //    $("#PIQId_" + l).select2().trigger('change');
                                //}

                                if (PIQ_HVPQResult != null) {
                                    if (PIQ_HVPQResult.length > 0) {
                                        $.each(PIQ_HVPQResult, function (m, red) {
                                            if (red.IsType == "PIQ") {
                                                if (red.PIQ_HVPQId == l) {
                                                    $("#PIQId_" + red.PIQ_HVPQId).val(red.PIQ_HVPQval);
                                                    $("#PIQId_" + red.PIQ_HVPQId).select2().trigger('change');
                                                    return true;
                                                }
                                            }
                                        });
                                    }
                                }
                                l = l + 1;
                            }
                        });
                    }
                }
            }
            else {
                $("#PIQView").hide();
                $("#HVPQView").hide();
                $("#BAView").hide();
                $(".hid").hide();
                PIQ_HVPQResult = [];
                ID_QArray = [];
            }
        }
    });


}

function GetBasicAttribute() {
    SetPIQHVPQ();

    $("#BADetails > tbody").empty();
    var VesselId = $("#VesselId").val();
    if (VesselId != "" && VesselId != null && VesselId != undefined) {
        if (PIQ_HVPQResult.length > 0) {
            var oldstatus;
            for (var m = 0; m < PIQ_HVPQResult.length; m++) {
                var oldstatus = false;
                for (var n = 0; n < PIQ_HVPQResult.length; n++) {
                    if (PIQ_HVPQResult[n].ID_Q == PIQ_HVPQResult[m].ID_Q && PIQ_HVPQResult[n].PIQ_HVPQ_ID_SUB_Q == PIQ_HVPQResult[m].PIQ_HVPQ_ID_SUB_Q) {
                        if (PIQ_HVPQResult[n].IsStatus == true) {
                            oldstatus = true;
                        }
                    }
                    if (oldstatus == true) {
                        $.each(PIQ_HVPQResult, function (j, red) {
                            if (red.ID_Q == PIQ_HVPQResult[m].ID_Q && red.PIQ_HVPQ_ID_SUB_Q == PIQ_HVPQResult[m].PIQ_HVPQ_ID_SUB_Q) {
                                PIQ_HVPQResult[j].IsStatus = true;
                            }
                        });
                    }

                }

                if (oldstatus == false) {
                    $.each(PIQ_HVPQResult, function (j, red) {
                        if (red.ID_Q == PIQ_HVPQResult[m].ID_Q) {
                            PIQ_HVPQResult[j].IsStatus = false;
                        }
                    });
                }

            }

            console.log(PIQ_HVPQResult);
        }
    }
}

function SearchBasicAttributes() {
    GetBasicAttribute();
    ID_QArray = [];
    var VesselId = $("#VesselId").val();
    var typeQuestionId = $("#typeQuestionId").val();
    var SectionId = $("#SectionId option:selected").text();
    var strmsg = "";
    if (VesselId == "" || VesselId == null) {
        strmsg += "Please Select Vessel \n";
    }
    if (typeQuestionId == "" || typeQuestionId == null) {
        strmsg += "Please Select Type Of Question \n";
    }

    if (SectionId == "" || SectionId == null) {
        strmsg += "Please Select Section \n";
    }

    if (strmsg == "") {
        var formData = new FormData();
        formData.append("VesselId", VesselId);
        formData.append("SectionId", SectionId);
        if (PIQ_HVPQResult.length > 0) {
            PIQ_HVPQResult.some(function (entry) {
                if (entry.ID_Q != null && entry.ID_Q != "") {
                    if (entry.IsStatus == false) {
                        var myjson = {
                            "ID_Q": entry.ID_Q
                        }
                        ID_QArray.push(myjson);
                    }
                }
            });

            console.log(JSON.stringify(ID_QArray));

            formData.append("data", JSON.stringify(ID_QArray));

            $.ajax({
                url: '/Dashboard/BasicAttributes/',
                type: 'POST',
                dataType: 'json',
                data: formData,
                contentType: false,
                processData: false,
                success: function (record) {
                    console.log(JSON.stringify(record));
                    if (record != null) {
                        $("#BADetails > tbody").empty();
                        if (record.length > 0) {
                            var l = 0;
                            $.each(record, function (i, rec) {
                                var colorCode = rec.Question_Type == "Core" ? "#fff9a0" : (rec.Question_Type == "Rotational 1" ? "#d7e6ff" : (rec.Question_Type == "Rotational 2" ? "#b3d0ff" : "#fff"));

                                var str = "<tr style='background-color:" + colorCode + ";'><td><div>" + rec.Q_NO + "</div></td><td><div style='width:350px;' class='qudiv font-weight-bold'>";
                                if (typeQuestionId == "Full") {
                                    str = str + rec.Question;
                                }
                                else {
                                    str = str + rec.Short_Name;
                                }

                                str = str + "</div></td><td><div style='width:70px;'>" + rec.Hardware_RT + "</div></td><td><div style='width:70px;'>" + rec.Human_RT + "</div></td><td><div style='width:70px;'>" + rec.Process_RT + "</div></td></tr>";
                                $("#BADetails > tbody").append(str);
                                l = l + 1;
                            });
                        }
                        else {
                            swal("warning", "No Record Found", "warning");
                            window.onkeydown = null;
                            window.onfocus = null;
                        }
                    }
                    else {
                        swal("warning", "No Record Found", "warning");
                        window.onkeydown = null;
                        window.onfocus = null;
                    }
                }
            });

        }
    }
    else {
        swal("warning", strmsg, "warning");
        window.onkeydown = null;
        window.onfocus = null;
        return false;
    }

}

function getbackGetHVPQ() {
    $.ajax({
        url: '/Dashboard/GetResult/',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: '',
        success: function (data) {
            var VesselId = $("#VesselId").val();
            VesselId = VesselId.trim();
            if (VesselId != null && VesselId != "" && VesselId != undefined) {
                if (PIQ_HVPQ != null) {
                    if (PIQ_HVPQ.length > 0) {
                        $("#HVPQDetails > tbody").empty();
                        var l = 0;
                        $.each(PIQ_HVPQ, function (i, rec) {
                            if (rec.IsType == "HVPQ") {
                                var str = "<tr><td><div class='qudiv'>" + rec.Q_Description + "</div></td><td><div style='width100px;'>";
                                if (rec.DropDownLMaster != null) {
                                    var sel = "";
                                    if (rec.DropDownLMaster.length > 0) {
                                        $.each(rec.DropDownLMaster, function (k, ent) {
                                            sel = sel + "<option value='" + ent.ID_INT + "' data-id='" + ent.ID_SUB_Q + "' data-desc='" + rec.Q_Description + "'  data-allow='" + rec.Is_Allow + "'  data-ID_Q='" + rec.ID_Q + "'>" + ent.SUB_Q + "</option>";
                                        });
                                    }
                                    str = str + "<select class='form-control inp select2' id='HVPQId_" + l + "'>" + sel + "</select>";
                                }
                                str = str + "</div></td></tr>";
                                $("#HVPQDetails > tbody").append(str);
                                $('.select2').select2();
                                $('.select2').width("100%");


                                if (PIQ_HVPQResult != null) {
                                    if (PIQ_HVPQResult.length > 0) {
                                        $.each(PIQ_HVPQResult, function (m, red) {
                                            if (red.IsType == "HVPQ") {
                                                if (red.PIQ_HVPQId == l) {
                                                    $("#HVPQId_" + red.PIQ_HVPQId).val(red.PIQ_HVPQval);
                                                    $("#HVPQId_" + red.PIQ_HVPQId).select2().trigger('change');
                                                    return true;
                                                }
                                            }
                                        });
                                    }
                                }
                                l = l + 1;
                            }
                        });
                    }
                }
            }
        }
    });


}




$("body").on("click", "#btnSubmitPIQ", function () {
    debugger
    var vessale = $("#VesselId option:selected").val();
    var data = SetPIQHVPQ(vessale);

    //Send the JSON array to Controller using AJAX.
    $.ajax({
        type: "POST",
        dataType:"json",
        data: JSON.stringify(data),
        contentType: 'application/json',
        url: "/PIQHVPQ/SavePIQ",
        success: function (r) {
            alert(r + " record(s) inserted.");
        }
    });
});


$("body").on("click", "#btnSubmitHVPQ", function () {
    debugger
    var vessale = $("#VesselId option:selected").val();
    var data = SetPIQHVPQ(vessale);

    //Send the JSON array to Controller using AJAX.
    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        contentType: 'application/json',
        url: "/PIQHVPQ/SaveHVPQ",
        success: function (r) {
            alert(r + " record(s) inserted.");
        }
    });
});




