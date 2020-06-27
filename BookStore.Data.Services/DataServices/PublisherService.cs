using BookStore.Data.Caching;
using BookStore.Data.Models.Entities;
using BookStore.Data.Services.DataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Services.DataServices
{
    public partial class PublisherService : IPublisherService
    {
        private const string PUBLISHER_BY_ID_KEY = "BookStore.Publishers.id-{0}";
        private const string PUBLISHER_PATTERN_KEY = "BookStore.Publishers.";

        private readonly IRepository<Publisher> _publisherRepository;
        private readonly MemoryCacheManager _memoryCacheManager;

        public PublisherService(IRepository<Publisher> publisherRepository,
            MemoryCacheManager memoryCacheManager)
        {
            this._publisherRepository = publisherRepository;
            this._memoryCacheManager = memoryCacheManager;
        }

        public async Task<Publisher> InsertPublisher(Publisher publisher)
        {
            if (publisher == null)
                throw new ArgumentNullException("publisher");

            await _publisherRepository.InsertAsync(publisher);
            _memoryCacheManager.RemoveByPattern(PUBLISHER_PATTERN_KEY);
            return publisher;
        }

        public async Task<Publisher> UpdatePublisher(Publisher publisher)
        {
            if (publisher == null)
                throw new ArgumentNullException("publisher");

            await _publisherRepository.UpdateAsync(publisher);
            _memoryCacheManager.RemoveByPattern(PUBLISHER_PATTERN_KEY);
            return publisher;
        }

        public async Task<Publisher> DeletePublisher(Publisher publisher, bool isHardDelete = false)
        {
            if (publisher == null)
                throw new ArgumentNullException("publisher");

            if (!isHardDelete)
            {
                publisher.IsDeleted = true;
                await UpdatePublisher(publisher);
                return publisher;
            }
            else
            {
                Publisher _publisher = publisher;
                await _publisherRepository.DeleteAsync(publisher);
                _memoryCacheManager.RemoveByPattern(PUBLISHER_PATTERN_KEY);
                return _publisher;
            }
        }

        public async Task<Publisher> GetPublisherById(int publisherId)
        {
            if (publisherId <= 0)
                return null;

            string key = string.Format(PUBLISHER_BY_ID_KEY, publisherId);
            return await _memoryCacheManager.Get(key, async () => await _publisherRepository.GetByIdAsync(publisherId));
        }

        public async Task<List<Publisher>> GetPublishersAsync(string publisherName = null, bool showDeleted = false)
        {
            IQueryable<Publisher> query = _publisherRepository.Table;
            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(publisherName))
                query = query.Where(x => x.PublisherName.Contains(publisherName));

            List<Publisher> publishers = await query.OrderBy(x => x.PublisherName).ToListAsync();
            return publishers;
        }

        public List<Publisher> GetPublishers(string publisherName = null, bool showDeleted = false)
        {
            IQueryable<Publisher> query = _publisherRepository.Table;
            if (!showDeleted)
                query = query.Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(publisherName))
                query = query.Where(x => x.PublisherName.Contains(publisherName));

            List<Publisher> publishers = query.OrderBy(x => x.PublisherName).ToList();
            return publishers;
        }
    }
}
