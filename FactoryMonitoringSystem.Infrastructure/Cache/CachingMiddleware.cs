using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO.Compression;

namespace FactoryMonitoringSystem.Infrastructure.Cache
{
    public class CachingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CachingMiddleware> _logger;
        private readonly ICacheService? _cacheService;
        private const int MaxCacheSize = 1024 * 1024; // 1MB

        public CachingMiddleware(RequestDelegate next, ILogger<CachingMiddleware> logger, IServiceProvider serviceProvider)
        {
            _next = next;
            _logger = logger;
            _cacheService = serviceProvider.GetService<ICacheService>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var cacheKey = GenerateCacheKey(context);

            // Attempt to serve the response from cache
            if (method == "GET" && await TryServeFromCache(context, cacheKey))
            {
                return; // Cache hit, response already written to the client
            }

            // If cache miss, proceed with request and capture response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context); // Process the request

            if (_cacheService != null)
            {
                await HandleCacheOperationAsync(context, method, cacheKey);
            }

            // Copy the response to the original stream
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }


        private async Task HandleCacheOperationAsync(HttpContext context, string method, string cacheKey)
        {
            switch (method)
            {
                case "GET":
                    await SetCacheAsync(context, cacheKey);
                    break;
                case "DELETE":
                case "PUT":
                    InvalidateCache(cacheKey);
                    break;
            }
        }

        private async Task SetCacheAsync(HttpContext context, string cacheKey)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();

            // Only cache if response size is below the limit
            if (responseText.Length <= MaxCacheSize)
            {
                var compressedResponse = Compress(responseText);
                 _cacheService!.SetCacheAsync(cacheKey, compressedResponse);
                _logger.LogInformation("Response cached for key: {CacheKey}", cacheKey);
            }
        }
        private async Task<bool> TryServeFromCache(HttpContext context, string cacheKey)
        {
            if (_cacheService == null) return false;

            // Retrieve the compressed response from cache
            var cachedCompressedResponse =  _cacheService.CheckAndGetCacheAsync(cacheKey);
            if (cachedCompressedResponse ==null || !cachedCompressedResponse.Any()) return false;

            _logger.LogInformation("Serving response from cache for key: {CacheKey}", cacheKey);

            // Decompress the cached response
            var decompressedResponse = Decompress(cachedCompressedResponse);

            // Set response content type and length
            context.Response.ContentType = "application/json"; // Adjust as necessary
            context.Response.ContentLength = decompressedResponse.Length;

            // Write the decompressed response to the client
            await context.Response.WriteAsync(decompressedResponse);
            await context.Response.Body.FlushAsync(); // Ensure data is sent to the client
            return true;
        }

        private void InvalidateCache(string cacheKey)
        {
             _cacheService!.RemoveCacheAsync(cacheKey);
            _logger.LogInformation("Cache invalidated for key: {CacheKey}", cacheKey);
        }
        private string Decompress(byte[] compressedData)
        {
            using var inputStream = new MemoryStream(compressedData);
            using var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);
            using var outputStream = new MemoryStream();

            gzipStream.CopyTo(outputStream);
            outputStream.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(outputStream);
            return reader.ReadToEnd();
        }


        private byte[] Compress(string text)
        {
            using var outputStream = new MemoryStream();
            using (var gzip = new GZipStream(outputStream, CompressionMode.Compress))
            using (var writer = new StreamWriter(gzip))
            {
                writer.Write(text);
            }
            return outputStream.ToArray();
        }

        private string GenerateCacheKey(HttpContext context)
        {
            var requestPath = context.Request.Path.ToString();
            var queryString = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : string.Empty;
            return $"{requestPath}{queryString}";
        }
    }
}
