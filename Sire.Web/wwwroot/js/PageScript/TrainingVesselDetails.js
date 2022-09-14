$(document).ready(function () {
    $("#Operator_Id").change(function () {

        $("#Vessel_Id").empty();
        debugger;
        $.ajax({
            type: "POST",
            url: "/TrainingVesselDetails/GetVessel/",
            dataType: "json",
            data: { id: $("#Operator_Id").val() },

            success: function (data) {


                debugger;
                $("#Vessel_Id").empty();
                $("#Vessel_Id").append('<option data-Id="' + 0 + '">Select</option>');
                if (data != null && data.length > 0) {
                    var k = 0;
                    $.each(data, function (i, items) {
                        debugger
                        k = k + 1;
                        console.log(items);
                        $("#Vessel_Id").append('<option value="' + items.id + '" data-Id="' + k + '">' + items.name + '</option>');
                    });

                }

            },
            error: function (ex) {
                alert("Failed to Vessel" + ex);
            }
        });
        return false;
    });
});



function GetSubMaster() {
    debugger;

    var Vessel_Id = $("#Vessel_Id option:selected").val();
    if (Vessel_Id != "" && Vessel_Id != "Select") {
        $.ajax({
            type: "POST",
            url: "/TrainingVesselDetails/GetVesselDetails/" + Vessel_Id,
            data: '{"id":' + Vessel_Id + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log(result);

                var str = '<tr><td><input type="text" class="form-control inp font-weight-bold text-center"  value="' + result.name + '" data-id="' + result.id + '" readonly  style="width:720px;"/></td>';
                str = str + '<td><input type="text" class="form-control inp font-weight-bold text-center"  value="' + result.vessel_Type + '" readonly style="width:720px;" /><input type="hidden" name="isdelete" value="' + result.isDeleted + '" /> </td></tr>';

                $("#veselTableDetails > tbody").append(str);
                console.log(str);
            }
        });
    }
}

