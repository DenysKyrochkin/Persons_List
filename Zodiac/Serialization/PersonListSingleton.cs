using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Zodiac.Models;
using Zodiac.Generator;

namespace Zodiac.PersonList
{
    public class PersonListSingleton
    {
        public static readonly string SerializationPath = @"Serialization/result.json";
        public List<Person> PersonList { get; set; }
        public List<Person> HelperList { get; set; }
        public PersonListSingleton()
        {
            try
            {
                PersonList = GetPersonList();
            }
            catch (JsonException)
            {
                PersonList = new List<Person>();

                for (int i = 0; i < 50; i++)
                {
                    PersonList.Add(GeneratorPerson.GeneratePerson());
                }

                Serialize().Wait();
            }

            HelperList = PersonList;
        }

        public List<Person> GetPersonList()
        {
            string jsonString = File.ReadAllText(SerializationPath);
            return JsonSerializer.Deserialize<List<Person>>(jsonString);
        }

        public List<Person> ProcessList(int id, string filter, string order)
        {
            List<Person> tempList = PersonList;
            switch (id)
            {
                case 1:
                    tempList = tempList.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToList();
                    if (order != "None")
                    {
                        tempList = order == "Ascending"
                            ? tempList.OrderBy(p => p.Name).ToList()
                            : tempList.OrderByDescending(p => p.Name).ToList();
                    }

                    break;
                case 2:
                    tempList = tempList.Where(p => p.Surname.ToLower().Contains(filter.ToLower())).ToList();
                    if (order != "None")
                    {
                        tempList = order == "Ascending"
                            ? tempList.OrderBy(p => p.Surname).ToList()
                            : tempList.OrderByDescending(p => p.Surname).ToList();
                    }

                    break;
                case 3:
                    tempList = tempList.Where(p => p.Email.ToLower().Contains(filter.ToLower())).ToList();
                    if (order != "None")
                    {
                        tempList = order == "Ascending"
                            ? tempList.OrderBy(p => p.Email).ToList()
                            : tempList.OrderByDescending(p => p.Email).ToList();
                    }

                    break;
                case 4:
                    if (filter != "")
                    {
                        DateTime date = DateTime.ParseExact(filter, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        tempList = tempList.Where(p => p.Birthday.Equals(date)).ToList();
                    }

                    if (order != "None")
                    {
                        tempList = order == "Ascending"
                            ? tempList.OrderBy(p => p.Birthday.Year)
                                .ThenBy(p => p.Birthday.DayOfYear).ToList()
                            : tempList.OrderByDescending(p => p.Birthday.Year)
                                .ThenBy(p => p.Birthday.DayOfYear).ToList();
                    }

                    break;
                case 5:
                    if (filter != "None")
                    {
                        bool adult = filter == "True";
                        tempList = tempList.Where(p => p.IsAdult.Equals(adult)).ToList();
                    }

                    if (order != "None")
                    {
                        tempList = order == "Ascending"
                            ? tempList.OrderBy(p => p.IsAdult).ToList()
                            : tempList.OrderByDescending(p => p.IsAdult).ToList();
                    }

                    break;
                case 6:
                    if (filter != "None")
                    {
                        tempList = tempList.Where(p => p.SunSign.Equals(filter)).ToList();
                    }

                    if (order != "None")
                    {
                        tempList = order == "Ascending"
                            ? tempList.OrderBy(p => p.SunSign).ToList()
                            : tempList.OrderByDescending(p => p.SunSign).ToList();
                    }

                    break;
                case 7:
                    if (filter != "None")
                    {
                        tempList = tempList.Where(p => p.ChineseSign.Equals(filter)).ToList();
                    }

                    if (order != "None")
                    {
                        tempList = order == "Ascending"
                            ? tempList.OrderBy(p => p.ChineseSign).ToList()
                            : tempList.OrderByDescending(p => p.ChineseSign).ToList();
                    }

                    break;
                case 8:
                    if (filter != "None")
                    {
                        bool birthday = filter == "True";
                        tempList = tempList.Where(p => p.IsBirthday.Equals(birthday)).ToList();
                    }

                    if (order != "None")
                    {
                        tempList = order == "Ascending"
                            ? tempList.OrderBy(p => p.IsBirthday).ToList()
                            : tempList.OrderByDescending(p => p.IsBirthday).ToList();
                    }

                    break;
            }

            return HelperList = tempList;
        }

        public async Task Serialize()
        {
            await using FileStream fs = File.Create(SerializationPath);
            await JsonSerializer.SerializeAsync(fs, PersonList, new JsonSerializerOptions {WriteIndented = true});
        }
    }
}
