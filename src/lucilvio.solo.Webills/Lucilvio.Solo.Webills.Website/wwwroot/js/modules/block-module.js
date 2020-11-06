var blockModule = function ($) {
    "strict";

    function blockScreen() {
        $.blockUI.defaults.border = 'none';
        $.blockUI({ message: "<h1> <img src='../img/ui/Pulse-1.3s-110px.svg' /> </h1>", css: { border: '0px', backgroundColor: 'none' } });

        $(".blockOverlay").css("z-index", "2001");
        $(".blockUI").css("z-index", "2002");
    }

    function unblockScreen() {
        $.unblockUI();
    }

    return {
        blockScreen: blockScreen,
        unblockScreen: unblockScreen
    }
}(jQuery);