(($, blockModule) => {
    "strict";

    $(function () {
        blockModule.unblockScreen();

        $("a:not(.no-block)").on("click", function () {
            blockModule.blockScreen();
        });
    });

    $("form").not(".no-block").on("submit", function () {
        if (!$(this).valid())
            return false;

        blockModule.blockScreen();
    });

    $(document).ajaxStart(blockModule.blockScreen).ajaxStop(blockModule.unblockScreen);
})(jQuery, blockModule);