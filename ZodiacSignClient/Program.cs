using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZodiacSignClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new ZodiacSignsService.ZodiacSignsServiceClient(channel);

            var date = "";
            var isValid = false;
            while(!isValid)
            {
                Console.WriteLine("Insert birth date (dd/mm/yyyy): ");
                date = Console.ReadLine();

                isValid = Regex.IsMatch(date, @"(?:(?:31(\/)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})");
                if (!isValid)
                    Console.WriteLine("Invalid Format!");
            }

            var zodiacSignReply = await client.SendAsync(new ZodiacRequest { BirthDate = date });

            Console.WriteLine($"Zodiac sign for you is {zodiacSignReply.Sign}");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
