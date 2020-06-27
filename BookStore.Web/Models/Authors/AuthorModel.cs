using BookStore.Data.Models.Bases;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models.Authors
{
    public partial class AuthorModel : BaseEntityModel
    {
        [DisplayName("Author Name")]
        public string AuthorName { get; set; }

        [DisplayName("Birth Date")]
        [UIHint("Date")]
        public DateTime? BirthDate { get; set; }
    }
}
