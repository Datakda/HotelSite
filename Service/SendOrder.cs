using HotelSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelSite.Service
{
    public static class SendOrder
    {
       public static string d;

        public static async Task<string> SendOrderServer(string _data)
        {

            
                HttpClient client = new HttpClient();
                using HttpContent content = new StringContent(_data, Encoding.UTF8, "application/json");
                using var response = await client.PostAsync("https://localhost:44391/api/Order/", content);
                return await response.Content.ReadAsStringAsync();
            

           
        }

    }
}
