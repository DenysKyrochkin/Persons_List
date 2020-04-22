using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Zodiac.Exceptions;


namespace Zodiac.Models
{
    public class Person
    {
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; } 

        public bool IsAdult { get; set; }
        public string SunSign { get; set; }
        public string ChineseSign { get; set; }
        public bool IsBirthday { get; set; }

        [JsonIgnore] 
        public int Id { get; set; }
        
        public Person() { }
        public Person(string name, string surname, string email, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Birthday = birthday;
            IsAdult = GetAge() >= 18;
            SunSign = GetSunZodiac();
            ChineseSign = GetChineseZodiac();
            IsBirthday = IsBirthdayToday();
        }

        public Person(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Birthday = default;
            IsAdult = GetAge() >= 18;
            SunSign = GetSunZodiac();
            ChineseSign = GetChineseZodiac();
            IsBirthday = IsBirthdayToday();
        }

        public Person(string name, string surname, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Email = default;
            IsAdult = GetAge() >= 18;
            SunSign = GetSunZodiac();
            ChineseSign = GetChineseZodiac();
            IsBirthday = IsBirthdayToday();
        }

        public int GetAge()
        {
            DateTime today = DateTime.Today;
            int age = 
            today.Year - Birthday.Year - 1 +
                ((today.Month > Birthday.Month || today.Month == Birthday.Month && today.Day >= Birthday.Day) ? 1 : 0);

            if (age > 135) throw new OldBirthException();
            if (age < 0) throw new NotBirthException();

            return age;
        }

        private bool IsBirthdayToday()
        {
            DateTime today = DateTime.Today;
            if (today.Month == Birthday.Month && today.Day == Birthday.Day)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetSunZodiac()
        {
            int month = Birthday.Month;
            int day = Birthday.Day;
            switch (month)
            {
                case 1:
                    if (day < 20)
                        return "Capricorn";
                    else
                        return "Aquarius";

                case 2:
                    if (day < 19)
                        return "Aquarius";
                    else
                        return "Pisces";

                case 3:
                    if (day < 21)
                        return "Pisces";
                    else
                        return "Aries";

                case 4:
                    if (day < 20)
                        return "Aries";
                    else
                        return "Taurus";

                case 5:
                    if (day < 21)
                        return "Taurus";
                    else
                        return "Gemini";

                case 6:

                    if (day < 21)
                        return "Gemini";
                    else
                        return "Cancer";

                case 7:
                    if (day < 23)
                        return "Cancer";
                    else
                        return "Leo";


                case 8:
                    if (day < 23)
                        return "Leo";
                    else
                        return "Virgo";

                case 9:
                    if (day < 23)
                        return "Virgo";
                    else
                        return "Libra";

                case 10:
                    if (day < 23)
                        return "Libra";
                    else
                        return "Scorpio";

                case 11:

                    if (day < 22)
                        return "Scorpio";
                    else
                        return "Sagittarius";

                case 12:
                    if (day < 22)
                        return "Sagittarius";
                    else
                        return "Capricorn";
                default:
                    return "";
                }
            }

        private string GetChineseZodiac()
        {
            int year = Birthday.Year;
            if (year % 12 == 0) { return "Monkey"; }
            else if (year % 12 == 1) { return "Rooster"; }
            else if (year % 12 == 2) { return "Dog"; }
            else if (year % 12 == 3) { return "Pig"; }
            else if (year % 12 == 4) { return "Rat"; }
            else if (year % 12 == 5) { return "Ox"; }
            else if (year % 12 == 6) { return "Tiger"; }
            else if (year % 12 == 7) { return "Rabbit"; }
            else if (year % 12 == 8) { return "Dragon"; }
            else if (year % 12 == 9) { return "Snake"; }
            else if (year % 12 == 10) { return "Horse"; }
            else { return "Sheep"; }
        }
    }
}
