using Store.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void deductAmount(decimal amount)
        {
            m_totalMoneyAmount -= amount;
        }
    }
}
