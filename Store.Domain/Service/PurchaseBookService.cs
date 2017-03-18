using Store.Domain;
using Store.Model;
using Store.Repository;
using System;
using System.Threading.Tasks;

namespace Store.Service
{
    public class PurchaseBookService
    {

        private IWallet m_wallet;
        private IPurchasedBooksRepository m_purchasedBooks;

        public PurchaseBookService(IWallet wallet, IPurchasedBooksRepository purchasedBooks)
        {
            m_wallet = wallet;
            m_purchasedBooks = purchasedBooks;
        }

        public async void PurchaseAsync(Book selectedBook)
        {

            if (!IsMoneyEnoughForPurchase(selectedBook))
            {
                throw new ArgumentException("No money to buy selected book", nameof(selectedBook));
            }

            m_wallet.DeductAmount(selectedBook.Price);
            await m_purchasedBooks.AddAsync(selectedBook);

        }
        
        public bool IsMoneyEnoughForPurchase(Book selectedBook)
        {
            return selectedBook.Price <= m_wallet.CurrentAmount;
        }

        public async Task<bool> IsPurchasedAsync(Book selectedBook)
        {
            return await m_purchasedBooks.IsPurchasedAsync(selectedBook.Id);
        }

    }
}
