using Store.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Model;
using System.IO;
using System.Reflection;

namespace Store.DataMock
{
    public class BookRepository : IBookRepository
    {

        private readonly List<Book> PreviewItems = new List<Book>()
        {
            create(1, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg") ,
            create(2, "Steve McConnell", "Code Complete, 2nd Edition", "code_complete.jpg") ,
            create(3, "Robert C. Martin", "Clean Code", "clean_code.jpg") ,
            create(4, "Michael C. Feathers", "Working Effectively With Legacy Code", "legacy_code.jpg") ,
            create(5, "Bjarne Stroustrup", "C++ Programming Language", "cplusplus_programming_language.jpg") ,
            create(6, "Nicolai M. Josuttis", "The C++ Standard Library", "cplusplus_strandard_library.jpg"),
            create(7, "Addy Osmani", "Javascript Design Pattens","javascript_design_patterns.jpg"),
            create(8, "Fred Brooks", "The Mythical Man Month", "mythical_man_month.jpg") ,
            create(9, "John Z. Sonmez", "Soft Skills", "soft_skills.jpg") ,
            create(10, "Dale Carnegie", "How to Win Friend And Influence People", "how_to_win_friends.jpg") ,
            create(11, "Maria S. Barbo", "Pokémon Handbook", "pokemon_handbook.jpg") ,
            create(12, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
            create(13, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
            create(14, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
            create(15, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
            create(16, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
            create(17, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg"),
            create(18, "Napoleon Hill", "Think and Grow Rich", "think_and_grow_rich.jpg")

        };

        private static Book create(int id, string author, string name, string imageFileName)
        {
            var book = new Book(id)
            {
                ImageFile = loadImage(imageFileName),
                Author = author,
                Name = name,
                Price = randomPrice(),
                PurchasedCount = randomPurchasedCount(),
                UserScore = randomScore(),
                ReviewerCount = randomReviewerCount(),
                Description = getBookDescription(),
                PublishedDate = randomDate()
            };

            return book;
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

        private static string getBookDescription()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus a condimentum libero. Sed sed sem in mauris mattis placerat. Integer id pulvinar risus. Praesent ultricies porta tortor, id commodo erat cursus quis. Nullam a cursus enim, non facilisis est. Sed euismod sagittis ligula, id tempor turpis ornare ut. Donec eget porttitor nunc. Donec euismod viverra elit eu pharetra.";
        }

        public async Task<IEnumerable<BookPreviewItem>> loadPreviewItems(int startFrom, int takeCount)
        {
            await Task.Delay(500);
            return PreviewItems.Skip(startFrom).Take(takeCount).ToList();
        }

        public async Task<Book> load(int id)
        {
            await Task.Delay(500);
            return PreviewItems[id - 1];
        }

        public async Task<IEnumerable<int>> loadAllPreviewItemIds()
        {
            await Task.Delay(250);
            return PreviewItems.Select(book => book.Id).ToList();
        }
    }
}
