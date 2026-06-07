using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Persistence
{
    public class RedisVerificationCodeStore : IVerificationCodeStore
    {
        private readonly IDistributedCache _cache;

        public RedisVerificationCodeStore(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task DeleteCode(string phoneNumber, CancellationToken ct)
        {
            await _cache.RemoveAsync($"verification:{phoneNumber}", ct);
        }

        public async Task SaveCode(string phoneNumber, string code, CancellationToken ct)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await _cache.SetStringAsync($"verification:{phoneNumber}", code, cacheOptions, ct);
        }

        public async Task<bool> VerifyCode(string phoneNumber, string codeFromUser, CancellationToken ct)
        {
            var codeFromCache = await _cache.GetStringAsync($"verification:{phoneNumber}", ct);

            if (codeFromCache is null)
                return false;

            return codeFromCache == codeFromUser;
        }
    }
}
