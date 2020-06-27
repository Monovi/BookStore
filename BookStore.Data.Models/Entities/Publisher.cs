using BookStore.Data.Models.Bases;

namespace BookStore.Data.Models.Entities
{
    public partial class Publisher : BaseEntity
    {
        public string PublisherName { get; set; }

        public string PublisherAddress { get; set; }
    }
}
