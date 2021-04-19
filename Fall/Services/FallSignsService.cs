using Fall.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Fall.Services
{
    public class FallSignsService : FallZodiacSigns.FallZodiacSignsBase
    {
        private readonly ZodiacOperations zodiacOperations = new ZodiacOperations();
        private readonly ILogger<FallSignsService> _logger;
        public FallSignsService(ILogger<FallSignsService> logger)
        {
            _logger = logger;
        }

        public override Task<FallZodiacResponse> FindZodiacSign(FallZodiacRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, "Checked sign");

            var signs = zodiacOperations.GetAllSigns();
            Console.WriteLine(signs);

            return Task.FromResult(new FallZodiacResponse
            {
                Sign = GetZodiacSignName(request.BirthDate, signs)
            });
        }

        private ZodiacSign GetZodiacSignName(string date, List<ZodiacSign> signs)
        {
            var dayStr = Regex.Match(date, @"\d{1,2}").Value;
            var monthStr = Regex.Match(date, @"\/\d{1,2}").Value.Remove(0, 1);

            var day = Int32.Parse(dayStr);
            var month = Int32.Parse(monthStr);

            foreach (var sign in signs)
            {
                if (sign.Equals(signs[0]) || sign.Equals(signs[3]))
                {
                    if (month == sign.StartMonth && day >= sign.StartDay && day <= sign.EndDay)
                        return sign;
                }
                else
                {
                    if (month == sign.StartMonth)
                    {
                        if (day >= sign.StartDay)
                            return sign;
                    }
                    else if (month == sign.EndMonth)
                    {
                        if (day <= sign.EndDay)
                            return sign;
                    }
                }
            }
            return null;
        }
    }
}
