(function ($, blockModule) {
    "strict";

    $(function () {
        blockModule.unblockScreen();

        $("a:not(.no-block)").on("click", function () {
            blockModule.blockScreen();
        });
    });

    $("form").not(".no-block").on("submit", function () {
        blockModule.blockScreen();
    });

    $(document).ajaxStart(blockModule.blockScreen).ajaxStop(blockModule.unblockScreen);
})(jQuery, blockModule);