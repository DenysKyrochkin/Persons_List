using System;
using System.Collections.Generic;
using System.Linq;
using Zodiac.Enums;

namespace Zodiac.Models
{
    public static class PersonsLists
    {
        public static List<string> OrderList = new List<string> { "None", "Ascending", "Descending" };      
        public static List<string> BoolList = new List<string> { "None", "True", "False" };
        public static List<string> ChineseZodiacList = InsertAtStart("None", Enum.GetNames(typeof(ChineseZodiac)).ToList());
        public static List<string> SunZodiacList = InsertAtStart("None", Enum.GetNames(typeof(SunZodiac)).ToList());
        public static List<string> InsertAtStart(string value, List<string> list)
        {
            list.Insert(0, value);
            return list;
        }
    }
}
