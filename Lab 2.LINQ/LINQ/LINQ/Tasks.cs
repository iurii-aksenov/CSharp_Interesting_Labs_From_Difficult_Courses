using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LINQ
{
    namespace Tasks
    {
        public class Tasks
        {
            static void Main(string[] args) { }

            public string Task1(string sourceText)
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>
                {
                    ["this"] = "эта",
                    ["dog"] = "собака",
                    ["eats"] = "ест",
                    ["too"] = "слишком",
                    ["much"] = "много",
                    ["vegetables"] = "овощей",
                    ["after"] = "после",
                    ["lunch"] = "обеда"
                };

                var transalatedText = sourceText.Split(' ').AsParallel().Select(x => dictionary[x.ToLower()].ToUpper() + " ")
                    .ToList();
                return transalatedText.AsParallel().Select(x => (transalatedText.IndexOf(x) + 1) % 3 == 0 ? x + "\n" : x)
                    .Aggregate((x, y) => x + y);

            }

            public int[] Task2(int k, int[] a)
            {
                return a.Skip(k).Where(x => x > 9 && x < 100 && x % 2 != 0).OrderByDescending(x => x).ToArray();
            }

            public char[] Task3(int k, string[] a)
            {
                return a.Where(x => x.Length >= k).Aggregate((x, y) => x + y).ToCharArray().Reverse().ToArray();
            }

            public List<string> Task4(int k, List<Client> clients)
            {
                var client = clients.Where(x => x.Id == k);
                if (!client.Any()) return new List<string> { "Нет данных." };

                IEnumerable<string> result =
                    from item in client
                    group item by item.Year
                    into years
                    select new { Monts = years.Count(x => x.Duration > 15), Year = years.Key }
                    into mountsYear
                    orderby mountsYear.Monts descending, mountsYear.Year
                    select $"Mounts when duration >15:{mountsYear.Monts} | Year:{mountsYear.Year}";

                return result.ToList();
            }

            public XDocument Task5(string path)
            {
                XDocument xdoc = XDocument.Load(path);

                var root = xdoc.Root;
                root.Elements().ToList().Where(firstLevel =>
                {
                    firstLevel.Elements().ToList().Where(secondLevel => !secondLevel.HasElements).Remove();
                    return !firstLevel.HasElements;
                }).Remove();

                return xdoc;
            }
        }
    }
}
