using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Model;
using Store.Repository;

namespace Store.DataMock
{
    public class BookReviewRepository : IReviewRepository
    {

        private static Random m_random = new Random();
        private static Dictionary<int, List<Review>> m_bookReviews = new Dictionary<int, List<Review>>();


        public async Task<IEnumerable<Review>> loadReviewsAsync(int itemId)
        {
            if (!m_bookReviews.ContainsKey(itemId))
            {
                m_bookReviews.Add(itemId, createReviews(itemId));
            }

            await Task.Delay(250);
            return  m_bookReviews[itemId];

        }

        private List<Review> createReviews(int itemId)
        {
            var reviews = new List<Review>()
            {
                new Review(itemId) { UserName = "Jianmei Shi", Date = randomDate(), Score = randomScore(), Text = loremIpsum() },
                new Review(itemId) { UserName = "Petri Miiki", Date = randomDate(), Score = randomScore(), Text = loremIpsum() },
                new Review(itemId) { UserName = "Petri Miiki", Date = randomDate(), Score = randomScore(), Text = loremIpsum() },
                new Review(itemId) { UserName = "Petsuri Miikuki", Date = randomDate(), Score = randomScore(), Text = loremIpsum() }
            };

            return reviews;
        }

        private string loremIpsum()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus a condimentum libero. Sed sed sem in mauris mattis placerat. Integer id pulvinar risus. Praesent ultricies porta tortor, id commodo erat cursus quis. Nullam a cursus enim, non facilisis est. Sed euismod sagittis ligula, id tempor turpis ornare ut. Donec eget porttitor nunc. Donec euismod viverra elit eu pharetra.";
        }

        private static int randomScore()
        {
            return (int)(5 * m_random.NextDouble());

        }

        private static DateTime randomDate()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(m_random.Next(range));
        }



        public async Task saveReviewAsync(int itemId, Review review)
        {

            await Task.Delay(250);
            m_bookReviews[itemId].Add(review);
            
        }
    }
}
