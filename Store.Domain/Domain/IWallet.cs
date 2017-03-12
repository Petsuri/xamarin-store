using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain
{
    public interface IWallet
    {
        decimal CurrentAmount { get; }
        void deductAmount(decimal amount);
    }
}
