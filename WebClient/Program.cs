using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebClient
{
    static class Program
    {
        const string server_adress = "https://localhost:5001";
        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri(server_adress),
        };
        static Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите ID клиента и нажмите Enter: ");

               

                var id = Console.ReadLine();

                Console.WriteLine("Введите 1 если нужно запросить пользователя с текущим ID");
                Console.WriteLine("Введите 2 если нужно создать нового пользователя со случайными данными");
                
                var str = Console.ReadLine(); 
                var way = Int32.Parse(str);

                if (way == 1)
                {
                    var customer = GetCustomerById(id);

                    if (customer != null)
                    {
                        Console.WriteLine($"ID пользователя: {customer.Id}");
                        Console.WriteLine($"Имя и фамилия клиента: {customer.Firstname} {customer.Lastname}");
                    }

                }
                if (way == 2)
                {
                    var customer = RandomCustomer();
                    customer.Id = Convert.ToInt64(id);
                    AddCustomer(customer);

                }
                Console.WriteLine("=============================================================================");
            }
        }

        private static void AddCustomer(CustomerCreateRequest request)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response = httpClient.PostAsync(
                "customers",
                jsonContent).Result;

            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine("Статус код: " + response.StatusCode);

        }
        private static Customer GetCustomerById(string id)
        {
            using var client = new HttpClient();

            var result =  client.GetAsync(Program.server_adress + $"/customers/{id}").Result;

            Console.WriteLine("Статус код: " + result.StatusCode);

            if(result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Пользователь успешно получен с сервера!");

                var res = result.Content.ReadAsStringAsync().Result;
                var customer = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer>(res);
                return customer;
            }

            return null;
        }
      
        private static CustomerCreateRequest RandomCustomer()
        {
            string[] firstNames = new string[5] {"Андрей","Иван","Пётр","Лев","Сергей"};
            string[] lastNames = new string[5] { "Иванов", "Петров", "Сидоров", "Глазев", "Перфилов" };
            
            Random r = new Random();

            var createCustomerRequest = new CustomerCreateRequest(
            firstNames[r.Next(0, 5)],
            lastNames[r.Next(0, 5)]
            );

            return createCustomerRequest;
        }
    }
}