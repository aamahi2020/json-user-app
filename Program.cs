using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonExample
{
    // Base class
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
    }

    // Derived Admin class
    public class Admin : User
    {
        public List<string> Permissions { get; set; }
    }

    // Derived RegularUser class
    public class RegularUser : User
    {
        public string MembershipLevel { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_types.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("JSON file not found: " + filePath);
                return;
            }

            string jsonContent = File.ReadAllText(filePath);

            List<User> users = new List<User>();

            try
            {
                JArray dataArray = JArray.Parse(jsonContent);

                foreach (var item in dataArray)
                {
                    string role = item["Role"]?.ToString();

                    if (role == "Admin")
                    {
                        Admin admin = item.ToObject<Admin>();
                        users.Add(admin);
                    }
                    else if (role == "User")
                    {
                        RegularUser user = item.ToObject<RegularUser>();
                        users.Add(user);
                    }
                    else
                    {
                        User baseUser = item.ToObject<User>();
                        users.Add(baseUser);
                    }
                }

                // Print all users
                foreach (var user in users)
                {
                    Console.WriteLine($"Role: {user.Role}");
                    Console.WriteLine($"Name: {user.Name}");
                    Console.WriteLine($"Age: {user.Age}");
                    Console.WriteLine($"City: {user.City}");

                    if (user is Admin admin)
                    {
                        Console.WriteLine("Permissions: " + string.Join(", ", admin.Permissions));
                    }
                    else if (user is RegularUser regUser)
                    {
                        Console.WriteLine("Membership Level: " + regUser.MembershipLevel);
                    }

                    Console.WriteLine(new string('-', 40));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deserializing JSON: " + ex.Message);
            }
        }
    }
}

