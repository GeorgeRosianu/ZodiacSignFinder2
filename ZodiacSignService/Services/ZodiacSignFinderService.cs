using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZodiacSignService.Services
{
    public class ZodiacSignFinderService : ZodiacSignsService.ZodiacSignsServiceBase
    {
        private readonly ILogger<ZodiacSignFinderService> _logger;
        public ZodiacSignFinderService(ILogger<ZodiacSignFinderService> logger)
        {
            _logger = logger;
        }

        public override async Task<ZodiacResponse> Send(ZodiacRequest request, ServerCallContext context)
        {
            var month = Int32.Parse(Regex.Match(request.BirthDate, @"\/\d{1,2}").Value.Remove(0, 1));
            switch (month)
            {
                case 12:
                case 1:
                case 2:
                    {
                        using var channel = GrpcChannel.ForAddress("https://localhost:5005");
                        var client = new WinterSignsClient.WinterZodiacSigns.WinterZodiacSignsClient(channel);

                        var birthDate = request.BirthDate;
                        var zodiacSignReply = await client.FindZodiacSignAsync(new WinterSignsClient.WinterZodiacRequest { BirthDate = birthDate });

                        return (new ZodiacResponse() { Sign = zodiacSignReply.Sign.Name });
                    }
                case 3:
                case 4:
                case 5:
                    {
                        using var channel = GrpcChannel.ForAddress("https://localhost:5003");
                        var client = new SpringSignsClient.SpringZodiacSigns.SpringZodiacSignsClient(channel);

                        var birthDate = request.BirthDate;
                        var zodiacSignReply = await client.FindZodiacSignAsync(new SpringSignsClient.SpringZodiacRequest { BirthDate = birthDate });

                        return (new ZodiacResponse() { Sign = zodiacSignReply.Sign.Name });
                    }
                case 6:
                case 7:
                case 8:
                    {
                        using var channel = GrpcChannel.ForAddress("https://localhost:5004");
                        var client = new SummerSignsClient.SummerZodiacSigns.SummerZodiacSignsClient(channel);

                        var birthDate = request.BirthDate;
                        var zodiacSignReply = await client.FindZodiacSignAsync(new SummerSignsClient.SummerZodiacRequest { BirthDate = birthDate });

                        return (new ZodiacResponse() { Sign = zodiacSignReply.Sign.Name });
                    }
                case 9:
                case 10:
                case 11:
                    {
                        using var channel = GrpcChannel.ForAddress("https://localhost:5002");
                        var client = new FallSignsClient.FallZodiacSigns.FallZodiacSignsClient(channel);

                        var birthDate = request.BirthDate;
                        var zodiacSignReply = await client.FindZodiacSignAsync(new FallSignsClient.FallZodiacRequest { BirthDate = birthDate });

                        return (new ZodiacResponse() { Sign = zodiacSignReply.Sign.Name });
                    }
                default:
                    {
                        using var channel = GrpcChannel.ForAddress("https://localhost:5002");
                        var client = new FallSignsClient.FallZodiacSigns.FallZodiacSignsClient(channel);

                        var birthDate = request.BirthDate;
                        var zodiacSignReply = await client.FindZodiacSignAsync(new FallSignsClient.FallZodiacRequest { BirthDate = birthDate });

                        return (new ZodiacResponse() { Sign = zodiacSignReply.Sign.Name });
                    }
            }
        }
    }
}
