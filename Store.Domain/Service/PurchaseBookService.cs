using Store.Domain;
using Store.Model;
using Store.Repository;
using System;

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

        public void purchase(Book selectedBook)
        {

            if (!isMoneyEnoughForPurchase(selectedBook))
            {
                throw new ArgumentException("No money to buy selected book", nameof(selectedBook));
            }

            m_wallet.deductAmount(selectedBook.Price);
            m_purchasedBooks.addAsync(selectedBook);

        }
        
        public bool isMoneyEnoughForPurchase(Book selectedBook)
        {
            return selectedBook.Price <= m_wallet.CurrentAmount;
        }

    }
}
