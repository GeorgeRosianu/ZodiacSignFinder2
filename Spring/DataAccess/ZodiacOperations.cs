using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Spring.DataAccess
{
    public class ZodiacOperations
    {
        private readonly string filePath = "./Resources/springSigns.txt";

        public ZodiacOperations()
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(
                    "Pisces\n1\n3\n20\n3\n" +
                    "Aries\n21\n3\n19\n4\n" +
                    "Taurus\n20\n4\n20\n5\n" +
                    "Gemini\n21\n5\n31\n5\n"
                    );
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }

        //public bool addSignList()
        //{
        //    try
        //    {
        //        StreamWriter sw = new StreamWriter(filePath, true);
        //        sw.Write(
        //            "20/01-18.02\nAquarius\n" +
        //            "19/02-20.03\nPisces\n" +
        //            "21/03-19.04\nAries\n" +
        //            "20/04-20.05\nTaurus\n" +
        //            "21/05-20.06\nGemini\n" +
        //            "21/06-22.07\nCancer\n" +
        //            "23/07-22.08\nLeo\n" +
        //            "23/08-22.09\nVirgo\n" +
        //            "23/09-22.10\nLibra\n" +
        //            "23/10-21.11\nScorpio\n" +
        //            "22/11-21.12\nSagittarius\n" +
        //            "22/12-19.01\nCapricorn"
        //            );
        //        sw.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"Exception: {e.Message}");
        //        return false;
        //    }
        //    return true;
        //}

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
