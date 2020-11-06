var messageModule = function ($) {
    "strict";

    function showErrorMessage(msg) {
        toastr.options = {
            "closeButton": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "150000",
            "hideDuration": "1000",
            "timeOut": "22000",
            "extendedTimeOut": "5000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        toastr.error(msg, "Sorry, something goes wrong....<br />")
    }

    function showSuccessMessage(msg) {
        toastr.options = {
            "closeButton": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "4000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        toastr.success(msg, "Yey!<br />");
    }

    return {
        showErrorMessage: showErrorMessage,
        showSuccessMessage: showSuccessMessage
    }

}(jQuery);