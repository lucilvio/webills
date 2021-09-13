(function ($, transactionsService, messageModule, blockModule) {

    const data = {
        transactions: [],
        newTransaction: { type: "expense", date: new Date().toLocaleDateString(), category: "", recurrent: false },
        transactionCategories: []
    }

    const methods = {
        loadTransactions() {
            blockModule.blockScreen();

            transactionsService.getTransactions((data) => {
                this.transactions = data;
                console.log(this.transactions);

                blockModule.unblockScreen();
            }, (error) => {
                messageModule.showErrorMessage("Error");
                blockModule.unblockScreen();
            });
        },
        loadIncomeCategories() {
            transactionsService.getIncomeCategories(data => {
                this.transactionCategories = data;
                console.log(this.transactionCategories);
            }, (error) => {
                messageModule.showErrorMessage("Error");
            });
        },
        loadExpenseCategories() {
            transactionsService.getExpenseCategories(data => {
                this.transactionCategories = data;
                console.log(this.transactionCategories);
            }, (error) => {
                messageModule.showErrorMessage("Error");
            });
        },
        addTransaction() {
            transactionsService.addTransaction(this.newTransaction, () => {
                messageModule.showSuccessMessage("Transaction successfully added");
                this.transactions.push(this.newTransaction);
                this.newTransaction = { type: "expense", date: new Date().toLocaleDateString(), category: "", recurrent: false };
            }, (error) => {
                messageModule.showErrorMessage("Error");
                console.log(error);
            })
        },
        deleteTransaction(transaction) {
            blockModule.blockScreen();

            transactionsService.deleteTransaction(transaction, () => {
                messageModule.showSuccessMessage("Successfully deleted");
                this.transactions = this.transactions.filter((value) => value.id !== transaction.id);

                blockModule.unblockScreen();
            }, (error) => {
                messageModule.showErrorMessage("Error");
                console.log(error);

                blockModule.unblockScreen();
            })
        }
    }

    const transactionApp = {
        data() {
            return data;
        },
        methods: methods,
        mounted() {
            this.loadTransactions();
            this.loadExpenseCategories();
        }
    }

    Vue.createApp(transactionApp).mount("#transactions-app")
}(jQuery, transactionsService, messageModule, blockModule));