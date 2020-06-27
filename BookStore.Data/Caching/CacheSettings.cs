namespace BookStore.Data.Caching
{
    public static class CacheSettings
    {
        /// <summary>
        /// SlidingExpiration in minutes.
        /// </summary>
        public const int CacheItemExpiration = 60; //TODO: Make configurable CacheItemExpiration?
    }
}
