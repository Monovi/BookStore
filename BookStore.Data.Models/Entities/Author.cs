using BookStore.Data.Models.Bases;
using System;

namespace BookStore.Data.Models.Entities
{
    public partial class Author : BaseEntity
    {
        public string AuthorName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
