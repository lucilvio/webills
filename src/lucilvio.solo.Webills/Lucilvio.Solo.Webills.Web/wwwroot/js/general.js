(function ($, blockModule) {
    "strict";

    $(function () {
        blockModule.unblockScreen();

        $("a").on("click", function () {
            blockModule.blockScreen();
        });
    });

    $("form").on("submit", function () {
        blockModule.blockScreen();
    });


    $(document).ajaxStart(blockModule.blockScreen).ajaxStop(blockModule.unblockScreen);

})(jQuery, blockModule);