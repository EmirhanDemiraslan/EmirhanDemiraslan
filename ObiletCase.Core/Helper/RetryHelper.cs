using System;
using System.Threading.Tasks;

namespace ObiletCase.Core.Helper
{
    public static class RetryHelper
    {
        public static async Task<T> RetryOnExceptionAsync<T>(int maxRetries, Func<Task<T>> operation, Func<Task> onRetry)
        {
            int attempt = 0;
            while (true)
            {
                try
                {
                    return await operation();
                }
                catch (Exception ex)
                {
                    attempt++; 
                    if (attempt >= maxRetries)
                    {
                        throw;
                    }

                    await onRetry();
                }
            }
        }
    }
}

