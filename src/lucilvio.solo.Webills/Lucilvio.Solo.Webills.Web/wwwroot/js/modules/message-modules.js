"strict";

var messageModule = function ($) {
    "strict";

    function showErrorMessage(msg) {
        toastr.options = {
            "closeButton": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "showDuration": "50000",
            "hideDuration": "1000",
            "timeOut": "12000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        toastr.error(msg, "Ups! Something goes wrong....")
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

        toastr.success(msg, "Yey!");
    }

    return {
        showErrorMessage: showErrorMessage,
        showSuccessMessage: showSuccessMessage
    }

}(jQuery);