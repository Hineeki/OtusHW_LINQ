using System;
using System.Globalization;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace OtusHW_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person[] people = {new Person("Prokhorov","Vasya",38), new Person("Vasilev","Pavel",26), new Person("Likhanov","Ilya",27), new Person("Levin", "Aleksander", 41),
                            new Person("Pravin","Roman",19), new Person("Volkov","Andrey",22), new Person("Zakharov","Yan",56), new Person("Ivanov","Ivan",44),
                            new Person("Petrov","Petr",33), new Person("Sidorov","Vladimir",55),};
            var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            Console.WriteLine(string.Join(",", arr.Top(22)));
            var result = people.Top(30, person => person.age);

            foreach (var person in result)
            {
                Console.WriteLine($"{person.lastName} {person.firstName}, {person.age} years old");
            }
        }
    }

    public class Person
    {
        private string _firstName;
        private string _lastName;
        private int _age;
        public Person(string lastName, string fitstName, int age)
        {
            _age = age;
            _firstName = lastName;
            _lastName = fitstName;
        }

        public string firstName { get { return _firstName; } }
        public string lastName { get { return _lastName; } }
        public int age { get { return _age; } }
    }
    public static class MyExtension
    {
        public static IEnumerable<T> Top<T>(this IEnumerable<T> collection, double perseent)
        {
            if (perseent < 1 || perseent > 100)
            {
                throw new MyException("Введён неудовлетворительный процент.");
            }
            var sort = collection.OrderByDescending(i => i).ToList();
            var value = (int)Math.Ceiling(sort.Count * perseent / 100.0);

            return sort.Take((int)value);
        }
        public static IEnumerable<T> Top<T, TKey>(this IEnumerable<T> collection, int perseent, Func<T, TKey> key)
        {
            if (perseent < 1 || perseent > 100)
            {
                throw new MyException("Введён неудовлетворительный процент.");
            }

            var sort = collection.OrderByDescending(key).ToList();
            var value = (int)Math.Ceiling(sort.Count * perseent / 100.0);
            return sort.Take(value);
        }
        public class MyException : Exception
        {
            public MyException(string message) : base(message) { }
        }
    }
}