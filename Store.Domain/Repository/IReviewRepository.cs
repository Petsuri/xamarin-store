using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public interface IReviewRepository
    {

        Task<IEnumerable<Review>> loadReviewsAsync(int itemId);
        Task saveReviewAsync(int itemId, Review review);

    }
}
