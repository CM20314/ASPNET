﻿namespace CM20314.Authentication
{
    /// <summary>
    /// All requests pass through this middleware to check the API key is valid before the request is processed
    /// </summary>
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor for API key middleware
        /// </summary>
        /// <param name="next">Method to process HTTP request</param>
        /// <param name="configuration">Configuration to access API key</param>
        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        /// <summary>
        /// Checks if incoming request has correct API key
        /// </summary>
        /// <param name="context">Contains request data (including headers)</param>
        /// <returns>Invocation task</returns>
        public async Task Invoke(HttpContext context)
        {
            // Header name is x-api-key
            var apiKey = context.Request.Headers["x-api-key"];

            // Validate the API key against the one stored in configuration
            var storedApiKey = _configuration["ApiSettings:ApiKey"];
            if (apiKey != storedApiKey)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }

            await _next.Invoke(context);
        }
    }

}
