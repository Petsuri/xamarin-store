
namespace Store.Model
{
    public class BookPreview
    {
        public int Id { get; private set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public decimal UserScore { get; set; }
        public BookCategory Category { get; set; }

        public BookPreview(int id)
        {
            this.Id = id;
        }

    }
}
