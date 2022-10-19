var Question = [];
var QuestionResult = [];
$("#id").val("0");

$(document).ready(function () {
    GetQuestion($("#hdnTraningId").val());
    const measure = $('#measure')
    const ammount = $('#num')
    const timer = $('#timer')
    const s = $(timer).find('.seconds')
    const m = $(timer).find('.minutes')
    const h = $(timer).find('.hours')

    var seconds = 0
    var minutes = 0
    var hours = 0

    var interval = null;

    var clockType = undefined;




    $('button#stop-timer').on('click', function () {
        pauseClock()
    })

    $('button#reset-timer').on('click', function () {
        restartClock()
    })

    $('button#resume-timer').on('click', function () {
        $('button#resume-timer').fadeOut(100)
        $('button#reset-timer').fadeOut(100)

        cronometer();


    })

    function pad(d) {
        return (d < 10) ? '0' + d.toString() : d.toString()
    }

    function startClock() {
        hasStarted = false
        hasEnded = false

        seconds = 0
        minutes = 0
        hours = 0

        switch ($(measure).val()) {
            case 's':
                if ($(ammount).val() > 3599) {
                    let hou = Math.floor($(ammount).val() / 3600)
                    hours = hou
                    let min = Math.floor(($(ammount).val() - (hou * 3600)) / 60)
                    minutes = min;
                    let sec = ($(ammount).val() - (hou * 3600)) - (min * 60)
                    seconds = sec
                }
                else if ($(ammount).val() > 59) {
                    let min = Math.floor($(ammount).val() / 60)
                    minutes = min
                    let sec = $(ammount).val() - (min * 60)
                    seconds = sec
                }
                else {
                    seconds = $(ammount).val()
                }
                break
            case 'm':
                if ($(ammount).val() > 59) {
                    let hou = Math.floor($(ammount).val() / 60)
                    hours = hou
                    let min = $(ammount).val() - (hou * 60)
                    minutes = min
                }
                else {
                    minutes = $(ammount).val()
                }
                break
            case 'h':
                hours = $(ammount).val()
                break
            default:
                break
        }


        refreshClock()

        $('.input-wrapper').slideUp(350)
        setTimeout(function () {
            $('#timer').fadeIn(350)
            $('#stop-timer').fadeIn(350)

        }, 350)

        cronometer();


    }

    function restartClock() {
        clear(interval)
        hasStarted = false
        hasEnded = false

        seconds = 0
        minutes = 0
        hours = 0

        $(s).text('00')
        $(m).text('00')
        $(h).text('00')

        $(timer).find('span').removeClass('red')

        $('#timer').fadeOut(350)
        $('#stop-timer').fadeOut(100)
        $('button#resume-timer').fadeOut(100)
        $('button#reset-timer').fadeOut(100)
        setTimeout(function () {
            $('.input-wrapper').slideDown(350)
        }, 350)
    }

    function pauseClock() {
        clear(interval)
        $('#resume-timer').fadeIn()
        $('#reset-timer').fadeIn()
    }

    var hasStarted = false
    var hasEnded = false
    if (hours == 0 && minutes == 0 && seconds == 0 && hasStarted == true) {
        hasEnded = true
    }


    function cronometer() {
        hasStarted = true
        interval = setInterval(() => {
            if (seconds < 59) {
                seconds++
                refreshClock()
            }
            else if (seconds == 59) {
                minutes++
                seconds = 0
                refreshClock()
            }

            if (minutes == 60) {
                hours++
                minutes = 0
                seconds = 0
                refreshClock()
            }

        }, 1000)
    }

    function refreshClock() {
        $(s).text(pad(seconds))
        $(m).text(pad(minutes))
        if (hours < 0) {
            $(s).text('00')
            $(m).text('00')
            $(h).text('00')
        } else {
            $(h).text(pad(hours))
        }

        if (hours == 0 && minutes == 0 && seconds == 0 && hasStarted == true) {
            hasEnded = true
            alert('The Timer has Ended !')
        }
    }

    function clear(intervalID) {
        clearInterval(intervalID)
        console.log('cleared the interval called ' + intervalID)
    }

})

$('button#start-cronometer').on('click', function (event) {
    debugger;
    var Operatorid = $("#hdnOperatorId").val();
    var TrainingID = $("#hdnTraningId").val();
    var OperatorIdList = [];
    $.ajax({
        url: "/TrainingQuestion/GetDifference",
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: "{'Operator_id':'" + Operatorid + "'}",
        success: function (record) {
            /*  alert(r + " record(s) inserted.");*/
            debugger;
            console.log(record);
            debugger;
            if (record != 0) {
                //  (window.confirm("Training for this vessel already exists. Do you still want to proceed with a new Training?"));


                var message = "";
                var message = confirm("Training for this vessel already exists. Do you still want to proceed with a new Training??");
                if (message == true) {
                    ////return message;
                    //$.ajax({
                    //    url: "/Training/Index/" + TrainingID,
                    //    type: 'POST',
                    //    dataType: 'json',
                    //    contentType: 'application/json; charset=utf-8',
                    //    data: '{"Id":' + TrainingID + '}',
                    //    success: function (r) {
                    //        alert("record inserted");
                    //    }
                    //});

                    var tabElement = $('#pills-tab a[href="#OnGoingTraining"]');
                    var tab = new bootstrap.Tab(tabElement[0]);
                    tab.show();
                    GetOnGoingTrainingQuestions(TrainingID);
                    return false;
                }
                else {
                    return false;
                }


            }




            //debugger;
            //if (data.Operator_id == Operatorid)
            //{
            //    swal("warning", "Training for this vessel already exists. Do you still want to proceed with a new Training?", "warning");
            //     window.onkeydown = null;
            //    window.onfocus = null;
            //}



        }
    });


    //clockType = 'cronometer'
    //if ($(ammount).val() != '' && $(measure).val() == 0) {
    //    alert('Select the Unit')
    //} else if ($(ammount).val() > -1) {
    //    startClock()
    //}
})

function GetQuestion(id) {
    debugger;
    $("#QueLibrary").empty();


    $("#QueLibrary").load("/TrainingQuestion/GetQuestion/" + id, function () {

    });

}
function GetRenkBaseQuestion() {
    debugger;
    $("#RankBaseQue").empty();

    $("#RankBaseQue").load("/TrainingQuestion/GetRenkBaseQuestion/", function () {


    });
}

function GetOnGoingTrainingQuestions(id) {
    debugger;
    $("#OnGoingTraining").empty();

    $("#OnGoingTraining").load("/TrainingQuestion/GetOnGoingTrainingQuestions/" + id, function () {


    });
}

function GetApplicableQuestions(id) {
    debugger;
    $("#ApplicableQue").empty();

    $("#ApplicableQue").load("/TrainingQuestion/GetApplicableQuestions/" + id, function () {

    });
}

function GetCIVQquestion() {
    debugger;
    $("#CIVQ").empty();

    $("#CIVQ").load("/TrainingQuestion/GetCIVQquestion/", function () {

    });
}

function GetTagQuestion() {
    debugger;
    $("#TagQuestion").empty();

    $("#TagQuestion").load("/TrainingQuestion/GetTagQuestion/", function () {

    });

}

function GetQuestionByRank() {

    /*   var traningId = $("#hdnTraningId").val();*/
    var Rank_Id = $("#Rank_Id option:selected").val();
    if (Rank_Id != "" && Rank_Id != "Select") {
        $.ajax({
            type: "POST",
            url: "/TrainingQuestion/GetQuestionByRank/" + Rank_Id,
            data: '{"id":' + Rank_Id + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (result) {
                debugger;
                console.log(result);
                GetRenkBaseQuestion();
                $("#bindPartialQuetion").html(result);
            }
        });
    }
}