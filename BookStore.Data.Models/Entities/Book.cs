using BookStore.Data.Models.Bases;

namespace BookStore.Data.Models.Entities
{
    public partial class Book : BaseEntity
    {
        public string BookTitle { get; set; }

        public int AuthorId { get; set; }

        public int PublisherId { get; set; }


        public virtual Author Author { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}
