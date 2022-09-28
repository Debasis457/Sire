$(document).ready(function () {
    $("#Vessel_Id").change(function () {

        debugger;

        var Vessel_Id = $("#Vessel_Id option:selected").val();
        if (Vessel_Id != "" && Vessel_Id != "Select") {
            $.ajax({
                type: "POST",
                url: "/User/GetVesselDetails/" + Vessel_Id,
                data: '{"id":' + Vessel_Id + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                
                success: function (result) {
                    debugger;
                    console.log(result);

                    var imo = result.imo;
                   /* $("#Status").val('<input type="text" class="form-control inp font-weight-bold text-center"  value="' + result.imo + '" data-id="' + result.id + '" readonly  style="width:720px;"/>');
*/
                    $("#Status").val(imo);
                    console.log(result.imo);
                    //console.log(str);
                }
            });
        }
      
    });
});

/*
function GetVesselDetails() {
    debugger;

    var Vessel_Id = $("#Vessel_Id option:selected").val();
    if (Vessel_Id != "" && Vessel_Id != "Select") {
        $.ajax({
            type: "POST",
            url: "/User/GetVesselDetails/" + Vessel_Id,
            data: '{"id":' + Vessel_Id + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log(result);

                $("#VesselIMo").append('<input type="text" class="form-control inp font-weight-bold text-center"  value="' + result.IMO + '" data-id="' + result.id + '" readonly  style="width:720px;"/>');
               
               
                console.log(str);
            }
        });
    }
}*/