using System.Net.Http.Headers;
using TalkBackAccessControl.Data.Models;

namespace TalkBackAccessControl.Data.Services
{
    public class ContactApi : IContactApi
    {
        public async void ApiConnection(User user)
        {
            string apiUrl = "https://localhost:44309/api/Contacts/updateStatus?userid={0}&status={1}";

            using HttpClient client = new();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = await client.GetAsync(string.Format(apiUrl, user.Id.ToString(), true));
            if (response.IsSuccessStatusCode)
            {
                _ = await response.Content.ReadAsStringAsync();
            }
        }

    }
}
