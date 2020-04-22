using System;
using Zodiac.Models;

namespace Zodiac.Generator
{
    public class GeneratorPerson
    {
        public static string[] Names =
        {
            "Anton", "Dmytro", "Vladyslav", "Julia", "Katerhina", 
            "Anastasia","Oleksander", "Lubomyr", "Myroslava", "Victoria", 
            "Kolya", "Myhailo", "Aglaya", "Ihor", "Polina"
        };

        public static string[] Surnames =
        {
            "Didula", "Pavlenko", "Katrichenko", "Drozdenko", "Koval",
            "Antonovych", "Tar", "Mosiichuk", "Avrammenko", "Babych",
            "Vasylko", "Kvych", "Kozak", "Stecko", "Pidmohylenko"
        };

        public static string[] Mail =
        {
            "ukma.edu.ua", "lnu.edu.ua", "ucu.edu.ua", "kpi.edu.ua", "znu.edu.ua"
        };

        public static DateTime GenerateDate()
        {
            Random random = new Random();

            int month = random.Next(11) + 1;
            int year = random.Next(39) + 1980;
            int day = random.Next(28) + 1; ;

            return new DateTime(year, month, day);
        }

        public static Person GeneratePerson()
        {
            Random random = new Random();
            string name = Names[random.Next(15)];
            string surname = Surnames[random.Next(15)];
            DateTime date = GenerateDate();

            Person person = new Person(name, surname, date);
            person.Email = $"{surname}{random.Next(100)}@{Mail[random.Next(5)]}";

            return person;
        }
    }
}
