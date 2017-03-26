using Store.Interface.Domain;

namespace Store.DataMock
{
    public class Wallet : IWallet
    {

        private static decimal m_totalMoneyAmount = 150m;

        public decimal CurrentAmount
        {
            get
            {
                return m_totalMoneyAmount;
            }
        }

        public void DeductAmount(decimal amount)
        {
            m_totalMoneyAmount -= amount;
        }
    }
}
