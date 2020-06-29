using BookStore.Data.Models.Bases;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace BookStore.Web.Models.Books
{
    public partial class BookModel : BaseEntityModel
    {
        public BookModel()
        {
            this.AvailableAuthors = new List<SelectListItem>();
            this.AvailablePublishers = new List<SelectListItem>();
        }

        [DisplayName("Title")]
        public string BookTitle { get; set; }

        [DisplayName("Author")]
        public int? AuthorId { get; set; }
        [DisplayName("Author")]
        public string AuthorName { get; set; }

        [DisplayName("Publisher")]
        public int? PublisherId { get; set; }
        [DisplayName("Publisher")]
        public string PublisherName { get; set; }


        public IList<SelectListItem> AvailableAuthors { get; set; }

        public IList<SelectListItem> AvailablePublishers { get; set; }
    }
}
