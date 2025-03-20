using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Middleware
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LocalizationMiddleware> _logger;

        public LocalizationMiddleware(RequestDelegate next, ILogger<LocalizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var language = context.Request.Headers["Accept-Language"].ToString();

            if (!string.IsNullOrEmpty(language))
            {
                try
                {
                    var culture = new CultureInfo(language);
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                    _logger.LogInformation($"Language set to {culture.Name}");
                }
                catch (CultureNotFoundException)
                {
                    _logger.LogWarning($"Invalid language: {language}. Using default.");
                }
            }

            await _next(context);
        }
    }
}
