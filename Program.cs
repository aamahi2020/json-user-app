using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonExample
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText("user.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Age: {user.Age}");
                Console.WriteLine($"City: {user.City}");
                Console.WriteLine("-----------------------");
            }
        }
    }
}

