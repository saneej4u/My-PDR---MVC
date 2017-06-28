(function () {

    var OnLoad = function () {
        $("#cp0").addClass("in");
        $("#cpSuccessFactor0").addClass("in");

        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-bottom-full-width",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        $('#toggle-colleuage').bootstrapToggle({
            on: 'Colleauge Sign',
            off: 'Clear Sign'
        });

        $('#toggle-manager').bootstrapToggle({
            on: 'Manager Sign',
            off: 'Clear Sign'
        });



        //$(".tile").height($("#tile1").width());
        //$(".carousel").height($("#tile1").width());
        //$(".item").height($("#tile1").width());

        //$(window).resize(function () {
        //    if (this.resizeTO) clearTimeout(this.resizeTO);
        //    this.resizeTO = setTimeout(function () {
        //        $(this).trigger('resizeEnd');
        //    }, 10);
        //});

        //$(window).bind('resizeEnd', function () {
        //    $(".tile").height($("#tile1").width());
        //    $(".carousel").height($("#tile1").width());
        //    $(".item").height($("#tile1").width());
        //});


       

        $(".progress-bar").loading();
      


    }

    // Events 

    $(document).on('click', '#calculateObjectiveRating', function () {

        var objViewModel = $("#objDDLForm").serialize();
        var url = '/PDReview/SaveOverallObjectiveDDL';

        $.ajax({
            url: url,
            data: objViewModel,
            type: 'GET',
            dataType: 'json',
            success: function (data, status) {
                if (status == 'success') {
                    $("#objoverallContainer").html(data.OverallObjectiveRatings);

                    if (data.IsModelValid) {
                        toastr.success('Overall rating for objectives is successfully saved.')
                    }
                    else
                    {
                        toastr.error('Overall rating for objectives is failed to save.')
                    }
                }
            },
            error: function (textStatus, errorThrown) {
                toastr.error(textStatus)
            },
            complete: function () {


            }
        });
        return false;

    });

    $(document).on('click', '#calculateSuccessFactorRating', function () {

        var succViewModel = $("#successFactorDDLForm").serialize();
        var url = '/PDReview/SaveOverallSuccessFactorDDL';
        $.ajax({
            url: url,
            data: succViewModel,
            type: 'GET',
            dataType: 'json',
            success: function (data, status) {
                if (status == 'success') {
                    $("#successfactorContainer").html(data.OverallSuccessFactorRatings);

                    if (data.IsModelValid) {
                        toastr.success('Overall rating for success factor is successfully saved.')
                    }
                    else {
                        toastr.error('Overall rating for success factor is failed to save.')
                    }
                }
            },
            error: function (textStatus, errorThrown) {
                toastr.error(textStatus)
            },
            complete: function () {


            }

        });

        return false;
    });

    $(document).on('click', '#calculateAnnualRating', function () {

        var selectedValue = $(this).val();
        var selecetedPDRId = $("#PDRId").val();
        var objViewModel = $("#overallAnnualDDLForm").serialize();

        var url = '/PDReview/SaveOverallAnnualRatingsDDL';

        $.ajax({
            url: url,
            data: objViewModel,
            type: 'GET',
                dataType: 'json',
                success: function(data, status) {
                    if(status == 'success') {
                        $("#overAllAnnualContainer").html(data.OverallAnnualRatings);

                    if (data.IsModelValid) {
                        toastr.success('Overall annuall rating is successfully saved.')
                    }
                        else
                            {
                        toastr.error('Overall annual rating is failed to save.')
                        }
                     }
                },
                error: function (textStatus, errorThrown) {
                        toastr.error(textStatus)
                },
                        complete : function () {


                }
        });
        return false;

       });

    $(document).on('change', '#toggle-colleuage', function () {

        var selecetedPDRId = $("#PDRId").val();
        var checkBoxValue = $(this).prop('checked');

        var url = '/PDReview/ColleagueSignature';
        $.ajax({
            url: url,
            data: { pdrId: selecetedPDRId, checkBoxValue: checkBoxValue },
            type: 'GET',
            async: true,
            global: false,
            success: function (data, status) {
                $("#colleagueSignedTextBoxContainer").html(data);
                if (status == 'success') {
                    toastr.success('Colleage signed saved.')
                }
            },
            error: function (textStatus, errorThrown) {
                alert("Error: " + errorThrown);
                alert("Status: " + textStatus);

                toastr.error(textStatus)
            },
            complete: function () {

                OnLoad();
            }

        });


    });

    $(document).on('change', '#toggle-manager', function () {

        var selecetedPDRId = $("#PDRId").val();
        var checkBoxValue = $(this).prop('checked');

        var url = '/PDReview/ManagerSignature';
        $.ajax({
            url: url,
            data: { pdrId: selecetedPDRId, checkBoxValue: checkBoxValue },
            type: 'GET',
            async: true,
            global: false,
            success: function (data, status) {
                $("#managerSignedTextBoxContainer").html(data);
                if (status == 'success') {
                    toastr.success('Manager signed saved.')
                }
            },
            error: function (textStatus, errorThrown) {
                alert("Error: " + errorThrown);
                alert("Status: " + textStatus);

                toastr.error(textStatus)
            },
            complete: function () {

                OnLoad();
            }

        });


    });

  
    OnLoad();

    $(document).on('click', '.taskStatus', function () {       
        var taskId = $(this).data("taskid");
        var checkboxValue = $(this).prop('checked');
        var url = '/PDReview/UpdateTaskStatus';
        $.ajax({
            url: url,
            data: {task: taskId, statusValue: checkboxValue },
            type: 'GET',
            async: true,
            global: false,
            success: function (data, status) {
                if (status == 'success') {
                    $("#toDoPartialViewContainer").html(data);
                }
            },
            error: function (textStatus, errorThrown) {
                toastr.error(textStatus)
                alert(errorThrown);
            },
            complete: function () {


            }
        });


    });

    $('#todoinputtasktextbox').keypress(function (e) {
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            var selectedValue = $(this).val();

            var url = '/PDReview/SaveToDoList';
            $.ajax({
                url: url,
                data: { task: selectedValue },
                dataType: 'json',
                type: 'POST',
                async: true,
                global: false,
                success: function (data, status) {
                    if (status == 'success') {
                        $("#toDoPartialViewContainer").html(data.toDoPartialView);
                        $("#todoinputtasktextbox").val("");

                        if (data.isemptyTask) {
                            toastr.error('Task cannot be empty.')
                        }
                        else {
                            if (data.isMoreThanFive) {
                                toastr.warning('Cannot create more than 5 task.')
                            }
                            else {
                                toastr.success('Task Created.')
                            }
                        }

                    }
                },
                error: function (textStatus, errorThrown) {
                    toastr.error(textStatus)
                },
                complete: function () {


                }

            });
        }
    });

    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.accessRequestHub;
    // Create a function that the hub can call back to display messages.
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        $('#unLockRequestAccessOrDenyContainer').html(message);
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
          

            var selecetedPDRId = $("#PDRId").val();

            var url = '/PDReview/UnLockRequest';
            $.ajax({
                url: url,
                data: { pdrId: selecetedPDRId },
                dataType: 'json',
                type: 'POST',
                async: true,
                global: false,
                success: function (data, status) {
                    if (status == 'success') {
                        $("#requestUnlockContainerOuter").html(data.requesUnlockPartialView);

                        // Call the Send method on the hub.
                        chat.server.send(data.sentTo, data.unLockRequestAccessOrDeny);
                    }
                },
                error: function (textStatus, errorThrown) {
                    toastr.error(textStatus)
                },
                complete: function () {


                }

            });
        });
    });


    function colleaguePDRCreatedSuccess(data, status) {
        if (status == 'success') {
            if ($(data).find('.input-validation-error').length > 0) {
                toastr.error('Failed to create PDR.')
            }
            else {
                toastr.success('PDR created successfully.')
            }
        }
    }

}(jQuery));