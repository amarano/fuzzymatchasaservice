using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fuzzymatchasaservice.core.Match;
using Microsoft.AspNetCore.Http;

namespace fuzzymatchasaservice.core.Middleware
{
    public class FuzzyMatchMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MatchConfiguration _config;

        public FuzzyMatchMiddleware(MatchConfiguration config)
        {
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestPath = context.Request.Path;
            if (!requestPath.HasValue) return;
            var segments = requestPath.Value.Split('/');
            var typeName = segments.Last();

            if (_config.Matchers.GetMatchers<>())

        }
    }
}
