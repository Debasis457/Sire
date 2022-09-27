var Url_ = "";
var rptName = "";
$(document).ready(function () {
    console.log('Execution Started--------------------------------- ');
    var urlVars = getUrlVars();
    Url_ = urlVars.url;
    rptName = urlVars.reportName;
    if (Url_ != "" || Url_ != undefined) {
        setDivUrl();
    }


    var alrtmsg = $("#msgid").val();
    var msgtype = $("#msgtypeid").val();
    if (alrtmsg != "") {
        swal({
            title: msgtype,
            text: alrtmsg,
            type: msgtype,
            showCancelButton: false,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Ok",
            closeOnConfirm: false,
            closeOnCancel: false

        });
        $("#altmsg").val("");
        console.log('Execution End ---------------------');
    }
    $(".inp").attr('autocomplete', 'off');

    $('#myInspectionModal').on('show.bs.modal', function () {
        $("#InspectionModalBody").load("/VesselPopUp/Index/");
    });
});

function isNumber(evt) {
    var indno = evt.key == "." ? -1 : 0;
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode != 46 || indno != -1) && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function move(e, id) {
    switch (e.which) {
        case 37:
            $(id).closest('td').prev().find('input').focus();
            break;
        case 39:
            $(id).closest('td').next().find('input').focus();
            break;
        case 40:
            index = $(id).closest('td').index();
            $(id).closest('tr').next().find('td').eq(index).find('input').focus();
            e.preventDefault();
            break;
        case 38:
            index = $(id).closest('td').index();
            $(id).closest('tr').prev().find('td').eq(index).find('input').focus();
            e.preventDefault();
            break;
        default:
            return;
    }
}

function reload() {
    
    swal({
        title: 'Confirmation',
        text: 'Are You Sure You Want To Reset?',
        type: 'warning',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Confirm',
        confirmButtonColor: '#8CD4F5',
        cancelButtonText: 'Cancel',
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (isConfirm) {
            
            if (isConfirm) {
                var refreshUrl = $("#refreshUrl").val();
                window.location.href = '' + refreshUrl;
            }
        }
    );
}

function Exit() {
    swal({
        title: 'Confirmation',
        text: 'Are You Sure You Want To Exit?',
        type: 'warning',
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Confirm',
        confirmButtonColor: '#8CD4F5',
        cancelButtonText: 'Cancel',
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (isConfirm) {
            
            if (isConfirm) {
                window.location.href = '/Dashboard/Index/';
            }
        }
    );
}




function getUrlVars() {
    var vars = [], hash;
    //var url = ReplaceAll(window.location.href, '#', '');
    var url = window.location.href;
    var hashes = url.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function ReplaceAll(string, stringToReplace, replaceWith) {
    string = string.split(stringToReplace).join(replaceWith);
    return string;
}

function setDivUrl() {
    //$("#ReportFrameOpen").attr("src", "https://bldb.balco.in/PIVision/#/Displays/34/1200MW_U1_Boiler_Overview");
    $("#ReportFrameOpen").attr("src", Url_);
}

function isNumber(txt, evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 46) {
        //Check if the text already contains the . character
        if (txt.value.indexOf('.') === -1) {
            return true;
        } else {
            return false;
        }
    } if (charCode == 45) {
        //Check if the text already contains the . character
        if (txt.value.indexOf('-') === -1) {
            return true;
        } else {
            return false;
        }
    } else {
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
           
                return false;
        }
    }
    return true;
}



function isNumberDec(el, evt) {
    
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}

function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}

function setMd5Password(event, event2 = null) {
    //event.forEach((evt) => {
    //    $('#' + evt).val($.MD5($('#' + evt).val()));
    //});
    $('#' + event).val($.MD5($('#' + event).val()));
    if (event2 != null) {
        $('#' + event2).val($.MD5($('#' + event2).val()));
    }
    return true;
}


function popup() {
     $('[id*="myModal1"]').modal('show');
   
}


function openForm() {
    debugger;
    $("#VesselList").empty();

    $("#Reversetab").load("/VesselPopUp/Index/", function () {

        //$.ajax({
        //    type: "POST",
        //    url: '/VesselPopUp/GetVesselList/',
        //    data: {},
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (result) {
        //        debugger;
        //        if (result != null) {
        //            if (result.length > 0) {

        //               console.log(result)
        //                $("#" + ddl).empty();
        //                $("#" + ddl).append('<option value="0">Select</option>');
        //                var str = $("#" + ddl).append('<option value="' + result.id + '">' + result.value + '</option>');


        //                $("#VesselList > tbody").append(str);

        //            }
        //        }

        //    },

        //    failure: function (response) {
        //        alert(response.d);
        //    }

        //});
    
      

        
    });
}

function openInspectionModal() {
    debugger;
    //$("#VesselList").empty();
    //$('#myInspectionModal').modal('show');
    $("#InspectionModalBody").load("/VesselPopUp/Index/");
}
 
   
function ShowModelPopUp()
{
        window.open('/VesselPopUp/Index', "WindowPopup", 'width=400px,height=400px,align=centre');  
}

//function isNumberKey(txt, evt) {
//    var charCode = (evt.which) ? evt.which : evt.keyCode;
//    if (charCode == 46) {
//        //Check if the text already contains the . character
//        if (txt.value.indexOf('.') === -1) {
//            return true;
//        } else {
//            return false;
//        }
//    } else {
//        if (charCode > 31 &&
//            (charCode < 48 || charCode > 57))
//            return false;
//    }
//    return true;
//}