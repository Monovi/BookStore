using BookStore.Data.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Data.Services.DataServices.Interfaces
{
    public partial interface IPublisherService
    {
        Task<Publisher> InsertPublisher(Publisher publisher);

        Task<Publisher> UpdatePublisher(Publisher publisher);

        Task<Publisher> DeletePublisher(Publisher publisher, bool isHardDelete = false);

        Task<Publisher> GetPublisherById(int publisherId);

        Task<List<Publisher>> GetPublishersAsync(string publisherName = null, bool showDeleted = false);

        List<Publisher> GetPublishers(string publisherName = null, bool showDeleted = false);
    }
}
