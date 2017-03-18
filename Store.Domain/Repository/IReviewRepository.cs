using Store.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository
{
    public interface IReviewRepository
    {

        Task<IEnumerable<Review>> LoadReviewsAsync(int itemId);
        Task SaveReviewAsync(int itemId, Review review);

    }
}
