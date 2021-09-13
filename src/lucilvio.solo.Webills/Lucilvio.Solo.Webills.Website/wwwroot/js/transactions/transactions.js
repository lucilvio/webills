(function ($, transactionsService, messageModule) {
    const categories = {};

    const fillCategoriesSelect = (options) => {
        let categorySelect = $("select[id='category']");

        categorySelect.empty();
        categorySelect.append("<option value=''> Select a transaction category </option>");
        options.forEach(e => categorySelect.append("<option value='" + e + "'>" + e + "</option>"));
    };

    const triggerCheckedRadioChangeEvent = () => $("input:radio[name='type'][checked='checked']").change();

    const bindTransactionTypeRadioChangeEvent = () => {
        $("input:radio[name='type']").change(function () {
            let selectedRadio = $(this)[0];

            if (selectedRadio.checked) {
                if (selectedRadio.value === "income") {
                    if (categories.incomes)
                        return fillCategoriesSelect(categories.incomes);

                    transactionsService.getIncomeCategories().done(function (result) {
                        categories.incomes = result.categories;
                        fillCategoriesSelect(categories.incomes);
                    });
                }
                else if (selectedRadio.value === "expense") {
                    if (categories.expenses)
                        return fillCategoriesSelect(categories.expenses);

                    transactionsService.getExpenseCategories().done(function (result) {
                        categories.expenses = result.categories;
                        fillCategoriesSelect(categories.expenses);
                    });
                }
            }
        });
    }

    $(function() {
        bindTransactionTypeRadioChangeEvent();
        triggerCheckedRadioChangeEvent();

        $("#recurrentCheck").change(function() {
            console.log($(this).is(":checked"));
            if ($(this).is(":checked"))
                $("#recurrencyOptionsPanel").show();
            else
                $("#recurrencyOptionsPanel").hide();
        });
        $("#recurrentCheck").change();

        $(".delete-transaction").on("click", function () {
            let transactionLine = $(this).parent().parent();
            let transactionId = transactionLine.find("#id")[0].value;
            let recurrencyId = transactionLine.find("#recurrency-id")[0].value;
            let transactionType = transactionLine.find("#type")[0].value;

            if (recurrencyId) {
                var yesButton = $("#delete-recurrent-transaction-modal").find(".delete-transaction-button");
                yesButton.data("transaction-id", transactionId);
                yesButton.data("transaction-type", transactionType);

                $("#delete-recurrent-transaction-modal").modal("show");
            }
            else {
                var yesButton = $("#delete-transaction-modal").find(".delete-transaction-button");
                yesButton.data("transaction-id", transactionId);
                yesButton.data("transaction-type", transactionType);

                $("#delete-transaction-modal").modal("show");
            }
        });

        $(".delete-transaction-button").on("click", function () {
            let transactionModal = $(this).parent().parent();
            let transactionId = $(this).data("transaction-id");
            let transactionType = $(this).data("transaction-type");

            transactionsService.deleteTransaction(transactionId, transactionType).done(function (result) {
                transactionModal.modal("hide");
                window.location.reload();

                messageModule.showSuccessMessage(result.message);
            });
        });
    });
}(jQuery, transactionsService(jQuery), messageModule())); 