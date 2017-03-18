namespace Store.Domain
{
    public interface IWallet
    {
        decimal CurrentAmount { get; }
        void DeductAmount(decimal amount);
    }
}
