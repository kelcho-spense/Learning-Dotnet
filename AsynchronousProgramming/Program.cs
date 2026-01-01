using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsynchronousProgramming
{
    class Program
    {
        // Create a single HttpClient instance to reuse across calls (singleton pattern)
        private static readonly HttpClient _httpClient;

        // Static constructor to initialize the HttpClient
        static Program()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"),
                Timeout = TimeSpan.FromSeconds(30)
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Main Method Started......");

            try
            {
                // get all users
                Console.WriteLine("\nAll User info:");
                string users = await FetchUsersAsync();
                Console.WriteLine(users);

               // get a user with id
                Console.Write("Enter the id to search users: ");
                string id = Console.ReadLine();

                string userJson = await FetchUserAsync(id);
                Console.WriteLine("\nUser infor:");
                Console.WriteLine(userJson);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nMain Method End");
            Console.ReadKey();
        }

        public static async Task<string> FetchUsersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("users");
            response.EnsureSuccessStatusCode();

            string users = await response.Content.ReadAsStringAsync();
            return users;
        }

        public static async Task<string> FetchUserAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID must be provided", nameof(id));

            HttpResponseMessage response = await _httpClient.GetAsync($"users/{id}");
            response.EnsureSuccessStatusCode();

            string user = await response.Content.ReadAsStringAsync();
            return user;
        }
    }
}