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
        public string UserType { get; set; }
    }

    // Admin class
    public class Admin : User
    {
        public string AdminLevel { get; set; }
    }

    // Member class
    public class Member : User
    {
        public string MembershipDate { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Read new JSON file
            string json = File.ReadAllText("usertypes.json");
            var jArray = JArray.Parse(json);

            List<User> users = new List<User>();

            foreach (var item in jArray)
            {
                string userType = item["UserType"]?.ToString();

                if (userType == "Admin")
                {
                    Admin admin = item.ToObject<Admin>();
                    users.Add(admin);
                }
                else if (userType == "Member")
                {
                    Member member = item.ToObject<Member>();
                    users.Add(member);
                }
            }

            // Print user data
            foreach (var user in users)
            {
                Console.WriteLine($"Type: {user.UserType}");
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Age: {user.Age}");
                Console.WriteLine($"City: {user.City}");

                if (user is Admin admin)
                {
                    Console.WriteLine($"Admin Level: {admin.AdminLevel}");
                }
                else if (user is Member member)
                {
                    Console.WriteLine($"Membership Date: {member.MembershipDate}");
                }

                Console.WriteLine("--------------------------");
            }
        }
    }
}


