using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Net.Http;

namespace NotifyDbEntries
{
    class Program
    {
        static void Inserted()
        {
            new Task(DownloadPageAsync).Start();
        }

        static async void DownloadPageAsync()
        {
            string page = "http://localhost/presense/home/reload";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
            }
        }
    }
}
