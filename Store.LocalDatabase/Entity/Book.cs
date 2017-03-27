using SQLite;

namespace Store.LocalDatabase.Entity
{
    [Table("UserLibrary")]
    internal class Book
    {
        public enum UserLibraryType
        {
            Purchased,
            InWishList
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int BookId { get; set; }
        public UserLibraryType BookType { get; set; }

    }
}
