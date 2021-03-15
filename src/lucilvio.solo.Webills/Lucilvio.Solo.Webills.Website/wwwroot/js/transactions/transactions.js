(function ($) {
    const categories = {};

    const getIncomeCategories = () => $.getJSON("transactions/incomecategories");    
    const getExpenseCategories = () => $.getJSON("transactions/expensecategories");    

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

                    getIncomeCategories().done(function (result) {
                        categories.incomes = result.categories;
                        fillCategoriesSelect(categories.incomes);
                    });
                }
                else if (selectedRadio.value === "expense") {
                    if (categories.expenses)
                        return fillCategoriesSelect(categories.expenses);

                    getExpenseCategories().done(function (result) {
                        categories.expenses = result.categories;
                        fillCategoriesSelect(categories.expenses);
                    });
                }
            }
        });
    }

    $(document).ready(() => {
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
    });
}(jQuery));