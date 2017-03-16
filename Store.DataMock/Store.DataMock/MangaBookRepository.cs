using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Model;
using System.Reflection;
using System.IO;
using Store.Repository;

namespace Store.DataMock
{
    public class MangaBookRepository : IBookRepository
    {

        private readonly List<Book> MangaPreviewItems = new List<Book>()
        {
            createManga(20, "尾田 栄一郎", "One Piece Volume 21", "Volume_21.png"),
            createManga(21, "尾田 栄一郎", "One Piece Volume 53", "Volume_53.jpg"),
            createManga(22, "尾田 栄一郎", "One Piece Volume 58", "Volume_58.jpg"),
            createManga(23, "尾田 栄一郎", "One Piece Volume 65", "Volume_65.png"),
            createManga(24, "尾田 栄一郎", "One Piece Volume 70", "Volume_70.png"),
            createManga(25, "尾田 栄一郎", "One Piece Volume 71", "Volume_71.jpg"),
            createManga(26, "尾田 栄一郎", "One Piece Volume 75", "Volume_75.png"),
            createManga(27, "尾田 栄一郎", "One Piece Volume 80", "Volume_80.jpg")
        };


        private static Book createManga(int id, string author, string name, string imageFileName)
        {

            var manga = new Book(id)
            {
                Image = loadImage(imageFileName),
                Author = author,
                Name = name,
                Price = randomPrice(),
                PurchasedCount = randomPurchasedCount(),
                UserScore = randomScore(),
                ReviewerCount = randomReviewerCount(),
                Description = getBookDescription(),
                PublishedDate = randomDate(),
                Category = new BookCategory(BookCategory.Category.Manga)
            };

            return manga;

        }

        private static Random m_random = new Random();

        private static decimal randomScore()
        {
            return (decimal)(5 * m_random.NextDouble());

        }

        private static DateTime randomDate()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(m_random.Next(range));
        }

        private static long randomReviewerCount()
        {
            return (long)(1000 * m_random.NextDouble());
        }

        private static long randomPurchasedCount()
        {
            return (long)(10000 * m_random.NextDouble());
        }

        private static decimal randomPrice()
        {
            return (decimal)(100 * m_random.NextDouble());
        }

        private static byte[] loadImage(string fileName)
        {

            var assembly = typeof(RecommendationBookRepository).GetTypeInfo().Assembly;
            using (var ms = new MemoryStream())
            {

                using (Stream fileStream = assembly.GetManifestResourceStream("Store.DataMock.Images.Manga." + fileName))
                {

                    fileStream.CopyTo(ms);
                    return ms.ToArray();

                }
            }

        }

        private static string getBookDescription()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus a condimentum libero. Sed sed sem in mauris mattis placerat. Integer id pulvinar risus. Praesent ultricies porta tortor, id commodo erat cursus quis. Nullam a cursus enim, non facilisis est. Sed euismod sagittis ligula, id tempor turpis ornare ut. Donec eget porttitor nunc. Donec euismod viverra elit eu pharetra.";
        }

        public async Task<IEnumerable<BookPreviewItem>> loadPreviewItemsAsync(int startFrom, int takeCount)
        {
            await Task.Delay(500);
            return MangaPreviewItems.Skip(startFrom).Take(takeCount).ToList();
        }

        public async Task<Book> loadAsync(int id)
        {
            await Task.Delay(500);
            return MangaPreviewItems.Where(m => m.Id == id).First();
        }

        public async Task<IEnumerable<int>> loadAllPreviewItemIdsAsync()
        {
            await Task.Delay(250);
            return MangaPreviewItems.Select(book => book.Id).ToList();
        }
    }
}
