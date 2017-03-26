using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.LocalDatabase
{
    [Table("UserLibrary")]
    public class Book
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
