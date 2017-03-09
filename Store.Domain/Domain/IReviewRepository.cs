using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain
{
    public interface IReviewRepository
    {

        Task<IEnumerable<Review>> loadReviews(int itemId);
        void saveReview(int itemId, Review review);

    }
}
