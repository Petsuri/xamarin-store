using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Model;
using Store.Interface.Repository;

namespace Store.DataMock
{
    public class BookReviewRepository : IReviewRepository
    {

        private static Random m_random = new Random();
        private static Dictionary<int, List<Review>> m_bookReviews = new Dictionary<int, List<Review>>();


        public async Task<IEnumerable<Review>> LoadReviewsAsync(int itemId)
        {
            if (!m_bookReviews.ContainsKey(itemId))
            {
                m_bookReviews.Add(itemId, CreateReviews(itemId));
            }

            await Task.Delay(250);
            return  m_bookReviews[itemId];

        }

        private List<Review> CreateReviews(int itemId)
        {
            var reviews = new List<Review>()
            {
                new Review(itemId) { UserName = "Jianmei Shi", Date = RandomDate(), Score = RandomScore(), Text = LoremIpsum() },
                new Review(itemId) { UserName = "Petri Miiki", Date = RandomDate(), Score = RandomScore(), Text = LoremIpsum() },
                new Review(itemId) { UserName = "Petri Miiki", Date = RandomDate(), Score = RandomScore(), Text = LoremIpsum() },
                new Review(itemId) { UserName = "Petsuri Miikuki", Date = RandomDate(), Score = RandomScore(), Text = LoremIpsum() }
            };

            return reviews;
        }

        private string LoremIpsum()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus a condimentum libero. Sed sed sem in mauris mattis placerat. Integer id pulvinar risus. Praesent ultricies porta tortor, id commodo erat cursus quis. Nullam a cursus enim, non facilisis est. Sed euismod sagittis ligula, id tempor turpis ornare ut. Donec eget porttitor nunc. Donec euismod viverra elit eu pharetra.";
        }

        private static int RandomScore()
        {
            return (int)(5 * m_random.NextDouble());

        }

        private static DateTime RandomDate()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(m_random.Next(range));
        }



        public async Task SaveReviewAsync(int itemId, Review review)
        {

            await Task.Delay(250);
            m_bookReviews[itemId].Add(review);
            
        }
    }
}
