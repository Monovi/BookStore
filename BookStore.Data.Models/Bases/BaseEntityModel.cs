namespace BookStore.Data.Models.Bases
{
    public abstract partial class BaseEntityModel
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
