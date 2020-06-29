using BookStore.Data.Models.Bases;
using System.ComponentModel;

namespace BookStore.Web.Models.Publishers
{
    public partial class PublisherModel : BaseEntityModel
    {
        [DisplayName("Name")]
        public string PublisherName { get; set; }

        [DisplayName("Address")]
        public string PublisherAddress { get; set; }
    }
}
