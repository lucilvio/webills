const transactionsService = (() => {
    const addTransaction = (transaction, onSuccess, onError) => {
        fetch("api/transactions", { method: "POST", headers: { "Content-Type": "application/json" }, body: JSON.stringify(transaction) })
            .then(function (response) {
                if (response.ok)
                    onSuccess();
                else
                    onError(response.status + " - " + response.statusText);
            })
            .catch(function (error) { onError(error); })
    }

    const deleteTransaction = (transaction, onSuccess, onError) => {
        fetch(`api/transactions/delete/${transaction.type}/${transaction.id}`, { method: "POST" })
            .then(function (response) {
                if (response.ok)
                    onSuccess();
                else
                    onError(response.status + " - " + response.statusText);
            })
            .catch(function (error) { onError(error); })
    }

    const getIncomeCategories = (onSuccess, onError) => {
        fetch("api/transactions/income/categories")
            .then(function (response) {
                return response.json()
            })
            .then((data) => {
                onSuccess(data.categories);
            })
            .catch(function (error) {
                onError(error);
            })
    }

    const getExpenseCategories = (onSuccess, onError) => {
        fetch("api/transactions/expense/categories")
            .then(function (response) {
                return response.json()
            })
            .then((data) => {
                onSuccess(data.categories);
            })
            .catch(function (error) {
                onError(error);
            })
    }

    const getTransactions = (onSuccess, onError) => {
        fetch("api/transactions")
            .then(function (response) {
                return response.json()
            })
            .then((data) => {
                onSuccess(data.transactions);
            })
            .catch(function (error) {
                onError(error);
            })
    }

    return {
        addTransaction: addTransaction,
        deleteTransaction: deleteTransaction,
        getIncomeCategories: getIncomeCategories,
        getExpenseCategories: getExpenseCategories,
        getTransactions: getTransactions
    }
})();