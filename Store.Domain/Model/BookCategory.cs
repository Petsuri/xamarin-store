﻿
namespace Store.Model
{
    public class BookCategory
    {
        public enum Category
        {
            Recommendation,
            Manga
        }

        public Category SelectedCategory { get; }
        public string CategoryName { get; }

        public BookCategory(Category c)
        {
            SelectedCategory = c;
            CategoryName = c.ToString();
        }

    }
}
