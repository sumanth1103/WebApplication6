using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static string baseUrl = "https://cricapi.com/";
        static void Main(string[] args)
        {
            //getSingleEmp(2);
            getAllEmp();
            /*insertData(new Employee
            {
                Age = 20,
                Name = "sai",
                Gender ="M"
            });*/
            //deleteData(1);
            Console.ReadKey();
        }
        static async void getSingleEmp(int id)
        {
            using(var client= new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/Employees/" + id);
                if(res.IsSuccessStatusCode)
                {
                    var emps = res.Content.ReadAsStringAsync().Result;
                    Employee emp = new Employee();
                    emp = JsonConvert.DeserializeObject<Employee>(emps);
                    if(emp != null)
                    {
                        //Console.WriteLine($"{emp.Eid} {emp.Age} {emp.Name} {emp.Gender}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid id");
                    }
                }
                else
                {
                    Console.WriteLine("Unable to process the request");
                }
            }
        }
        static async void getAllEmp()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/playerStats?apikey=qcwjPPAiF6ZjVkSoe7YjDvJY0n42&pid=35320");
                if (res.IsSuccessStatusCode)
                {
                    var emps = res.Content.ReadAsStringAsync().Result;
                    dynamic dObject = JObject.Parse(emps);
                    Console.WriteLine(dObject.pid);
                    Console.WriteLine("---------------------- Player Name -------------------");
                    Console.WriteLine(dObject.name);
                    Console.WriteLine("---------------------- Player Profile -------------------");
                    Console.WriteLine(dObject.profile);
                    Console.WriteLine("---------------------- Batting Style -------------------");
                    Console.WriteLine(dObject.battingStyle);
                    Console.WriteLine("---------------------- Bowling Style -------------------");
                    Console.WriteLine(dObject.bowlingStyle);
                    Console.WriteLine("---------------------- Major Teams -------------------");
                    Console.WriteLine(dObject.majorTeams);
                    Console.WriteLine("---------------------- Current Age -------------------");
                    Console.WriteLine(dObject.currentAge);
                    Console.WriteLine("---------------------- Born -------------------");
                    Console.WriteLine(dObject.born);
                    Console.WriteLine("---------------------- Full Name -------------------");
                    Console.WriteLine(dObject.fullName);
                    Console.WriteLine("---------------------- Country -------------------");
                    Console.WriteLine(dObject.country);
                    Console.WriteLine("---------------------- Bowling -------------------");
                    foreach(var i in dObject.data.bowling)
                    {
                        Console.WriteLine("---------------" + i.Name + "----------------");
                        foreach (var j in i)
                        { 
                            foreach (var k in j)
                            {
                                Console.WriteLine($" {k.Name} : {k.Value}");
                            }
                        }
                    }
                    
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Unable to process the request");
                }
            }
        }

        static void insertData(Employee emp)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var postResult = client.PostAsJsonAsync<Employee>("api/Employees", emp);
                postResult.Wait();
                var result = postResult.Result;
                if(result.IsSuccessStatusCode)
                {
                    Console.WriteLine("Inserted Succesfully");
                }
                else
                {
                    Console.WriteLine("unable to insert data");
                }
            }
        }
        static void deleteData(int id)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var res = client.DeleteAsync("api/Employees/" + id).Result;
                if(res.IsSuccessStatusCode)
                {
                    Console.WriteLine("Deleted Successfully");
                }
                else
                {
                    Console.WriteLine("Unable to delete");
                }
            }
        }
    }
}
/*foreach (var property in dObject)
                    {
                        //var e = property.data;
                        //Console.WriteLine(property.pid);
                        foreach (var playerModel in property)
                        {
                            Console.WriteLine(playerModel);
                            break;
                        }

                        *//*var playerModel = new Employee
                        {
                            date = e.date,
                            dateTimeGMT = e.dateTimeGMT,
                            team_1 = e.team-1,
                            team_2 = e.team-2
                        };
                        Console.WriteLine($" {playerModel.date} {playerModel.dateTimeGMT} {playerModel.team_1} {playerModel.team_2} ");*//*
                    }*/
/*List<Employee> emp = new JsSerializer().Deserialize<List<Employee>>(emps);
foreach (var e in emp)
{
if (e.match_started == true)
{
    Console.WriteLine($"{e.unique_id} {e.date} {e.dateTimeGMT} {e.team_1} {e.team_2} {e.squad} {e.toss_winner_team} {e.winner_team} {e.match_started} {e.type}");
}
}*/