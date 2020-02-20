namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    enum Recurrency
    {
        Daily = 1,
        Weekly = 7,
        Biweekly = 14,
        Monthly = 30,
        Bimonthly = 60,
        Trimonthly = 90,
        Quarterly = 120,
        Semiannualy = 182,
        Annual = 365
    }
}