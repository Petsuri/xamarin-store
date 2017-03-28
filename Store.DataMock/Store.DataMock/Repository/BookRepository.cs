using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Model;
using System.IO;
using System.Reflection;
using Store.Interface.Repository;

namespace Store.DataMock
{
    public class BookRepository : IBookRepository
    {

        public BookRepository()
        {
            if (RecommendationPreviewItems == null)
            {
                RecommendationPreviewItems = new List<Book>()
                {
                    CreateRecommendation(1, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg") ,
                    CreateRecommendation(2, "Steve McConnell", "Code Complete, 2nd Edition", "code_complete.jpg") ,
                    CreateRecommendation(3, "Robert C. Martin", "Clean Code", "clean_code.jpg") ,
                    CreateRecommendation(4, "Michael C. Feathers", "Working Effectively With Legacy Code", "legacy_code.jpg") ,
                    CreateRecommendation(5, "Bjarne Stroustrup", "C++ Programming Language", "cplusplus_programming_language.jpg") ,
                    CreateRecommendation(6, "Nicolai M. Josuttis", "The C++ Standard Library", "cplusplus_strandard_library.jpg"),
                    CreateRecommendation(7, "Addy Osmani", "Javascript Design Pattens","javascript_design_patterns.jpg"),
                    CreateRecommendation(8, "Fred Brooks", "The Mythical Man Month", "mythical_man_month.jpg") ,
                    CreateRecommendation(9, "John Z. Sonmez", "Soft Skills", "soft_skills.jpg") ,
                    CreateRecommendation(10, "Dale Carnegie", "How to Win Friend And Influence People", "how_to_win_friends.jpg") ,
                    CreateRecommendation(11, "Maria S. Barbo", "Pokémon Handbook", "pokemon_handbook.jpg") ,
                    CreateRecommendation(12, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
                    CreateRecommendation(13, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
                    CreateRecommendation(14, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
                    CreateRecommendation(15, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
                    CreateRecommendation(16, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
                    CreateRecommendation(17, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
                    CreateRecommendation(18, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg")

                };
            }

            if (MangaPreviewItems == null)
            {
                MangaPreviewItems = new List<Book>()
                {
                    CreateManga(20, "尾田 栄一郎", "One Piece Volume 21", "Volume_21.png"),
                    CreateManga(21, "尾田 栄一郎", "One Piece Volume 53", "Volume_53.jpg"),
                    CreateManga(22, "尾田 栄一郎", "One Piece Volume 58", "Volume_58.jpg"),
                    CreateManga(23, "尾田 栄一郎", "One Piece Volume 65", "Volume_65.png"),
                    CreateManga(24, "尾田 栄一郎", "One Piece Volume 70", "Volume_70.png"),
                    CreateManga(25, "尾田 栄一郎", "One Piece Volume 71", "Volume_71.jpg"),
                    CreateManga(26, "尾田 栄一郎", "One Piece Volume 75", "Volume_75.png"),
                    CreateManga(27, "尾田 栄一郎", "One Piece Volume 80", "Volume_80.jpg")
                };
            }

        }

        private static List<Book> MangaPreviewItems;
        private static List<Book> RecommendationPreviewItems;


        private static Book CreateRecommendation(int id, string author, string name, string imageFileName)
        {
            var book = new Book(id)
            {
                Image = LoadRecommendationImage(imageFileName),
                Author = author,
                Name = name,
                Price = RandomPrice(),
                PurchasedCount = RandomPurchasedCount(),
                UserScore = RandomScore(),
                ReviewerCount = RandomReviewerCount(),
                Description = GetBookDescription(),
                PublishedDate = RandomDate(),
                Category = new BookCategory(BookCategory.Category.Recommendation)
            };

            return book;
        }



        private static Book CreateManga(int id, string author, string name, string imageFileName)
        {

            var manga = new Book(id)
            {
                Image = LoadMangaImage(imageFileName),
                Author = author,
                Name = name,
                Price = RandomPrice(),
                PurchasedCount = RandomPurchasedCount(),
                UserScore = RandomScore(),
                ReviewerCount = RandomReviewerCount(),
                Description = GetBookDescription(),
                PublishedDate = RandomDate(),
                Category = new BookCategory(BookCategory.Category.Manga)
            };

            return manga;

        }



        private static Random m_random = new Random();

        private static decimal RandomScore()
        {
            return (decimal)(5 * m_random.NextDouble());

        }

        private static DateTime RandomDate()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(m_random.Next(range));
        }

        private static long RandomReviewerCount()
        {
            return (long)(1000 * m_random.NextDouble());
        }

        private static long RandomPurchasedCount()
        {
            return (long)(10000 * m_random.NextDouble());
        }

        private static decimal RandomPrice()
        {
            return (decimal)(100 * m_random.NextDouble());
        }

        private static byte[] LoadRecommendationImage(string fileName)
        {

            var assembly = typeof(BookRepository).GetTypeInfo().Assembly;            
            using (var ms = new MemoryStream())
            {
                
                using (Stream fileStream = assembly.GetManifestResourceStream("Store.DataMock.Images.Book." + fileName))
                {

                    fileStream.CopyTo(ms);
                    return ms.ToArray();

                }
            }
            
        }

        private static byte[] LoadMangaImage(string fileName)
        {

            var assembly = typeof(BookRepository).GetTypeInfo().Assembly;
            using (var ms = new MemoryStream())
            {

                using (Stream fileStream = assembly.GetManifestResourceStream("Store.DataMock.Images.Manga." + fileName))
                {

                    fileStream.CopyTo(ms);
                    return ms.ToArray();

                }
            }

        }

        private static string GetBookDescription()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus a condimentum libero. Sed sed sem in mauris mattis placerat. Integer id pulvinar risus. Praesent ultricies porta tortor, id commodo erat cursus quis. Nullam a cursus enim, non facilisis est. Sed euismod sagittis ligula, id tempor turpis ornare ut. Donec eget porttitor nunc. Donec euismod viverra elit eu pharetra.";
        }

        public async Task<IEnumerable<BookPreview>> LoadPreviewBookAsync(int startFrom, int takeCount, BookCategory.Category category)
        {
            await Task.Delay(500);

            if (category == BookCategory.Category.Manga)
            {
                return MangaPreviewItems.Skip(startFrom).Take(takeCount).ToList();
            }
            else
            {
                return RecommendationPreviewItems.Skip(startFrom).Take(takeCount).ToList();
            }

        }

        public async Task<Book> LoadAsync(int id)
        {
            await Task.Delay(500);

            if(id == 12)
            {
                throw new Exception("Jep, virhe tapahtui. Testataan virheenkäsittelyä");
            }

            Book item = MangaPreviewItems.FirstOrDefault(book => book.Id == id);
            if (item == null)
            {
                item = RecommendationPreviewItems.First(book => book.Id == id);
            }

            return item;
        }

        public async Task<IEnumerable<Book>> LoadAsync(IEnumerable<int> ids)
        {
            await Task.Delay(500);

            var foundBooks = new List<Book>();
            foundBooks.AddRange(MangaPreviewItems.Where(book => ids.Contains(book.Id)));
            foundBooks.AddRange(RecommendationPreviewItems.Where(book => ids.Contains(book.Id)));

            return foundBooks;
        }

        public async Task<IEnumerable<int>> LoadAllPreviewBookIdsAsync(BookCategory.Category category)
        {
            await Task.Delay(250);

            if (category == BookCategory.Category.Manga)
            {
                return MangaPreviewItems.Select(book => book.Id).ToList();
            }
            else
            {
                return RecommendationPreviewItems.Select(book => book.Id).ToList();
            }

        }
    
    }
}
