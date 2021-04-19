using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Winter.DataAccess
{
    public class ZodiacOperations
    {
        private readonly string filePath = "./Resources/winterSigns.txt";

        public ZodiacOperations()
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(
                    "Sagittarius\n1\n12\n21\n12\n" +
                    "Capricorn\n22\n12\n19\n1\n" +
                    "Aquarius\n20\n1\n18\n2\n" +
                    "Pisces\n19\n2\n20\n3\n"
                    );
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }

        public List<ZodiacSign> GetAllSigns()
        {
            var signs = new List<ZodiacSign>();

            string name;
            int startDay;
            int startMonth;
            int endDay;
            int endMonth;
            try
            {
                StreamReader sr = new StreamReader(filePath);

                name = sr.ReadLine();
                startDay = Int32.Parse(sr.ReadLine());
                startMonth = Int32.Parse(sr.ReadLine());
                endDay = Int32.Parse(sr.ReadLine());
                endMonth = Int32.Parse(sr.ReadLine());
                while (name != null)
                {
                    signs.Add(new ZodiacSign() { StartDay = startDay, StartMonth = startMonth, EndDay = endDay, EndMonth = endMonth, Name = name });

                    name = sr.ReadLine();
                    startDay = Int32.Parse(sr.ReadLine());
                    startMonth = Int32.Parse(sr.ReadLine());
                    endDay = Int32.Parse(sr.ReadLine());
                    endMonth = Int32.Parse(sr.ReadLine());
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
            foreach (var item in signs)
            {
                Console.WriteLine($"name: {item.Name}");
            }
            return signs;
        }
    }
}
