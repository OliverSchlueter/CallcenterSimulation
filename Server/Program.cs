using Server.Clients;
using Server.Utils;

namespace Server;

public static class Program
{
    struct Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public void Print()
        {
            Console.WriteLine($"{FirstName} {LastName} {Age}");
        }
    }
    private static void Main()
    {
        Person p1 = new Person("Oliver", "Schlüter", 19);
        Person p2 = new Person("Harald", "Schlüter", 18);
        
        Cache<string, Person> myCache = new Cache<string, Person>(false);
        myCache.Put(p1.FirstName, p1);
        myCache.Put(p2.FirstName, p2);

        myCache.AddIndex("LastNameIndex", person =>  person.LastName);

        foreach (var person in myCache.GetFromIndex("LastNameIndex", "Schlüter"))
        {
            person.Print();
        }
        
    }
    private static void Main2()
    {
        Console.Title = "CallcenterSimulation | Server";
        
        Console.WriteLine($"Starting server application now (v{ServerMain.Version})");
        ServerMain main = ServerMain.CreateOrGetInstance();
        main.Start();
    }
}
