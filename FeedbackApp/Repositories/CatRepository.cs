using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FeedbackApp.Repositories
{
    public class CatRepository : ICatRepository
    {
        public byte[] GetCatPhoto()
        {
            var httpClient = new HttpClient();
            return httpClient.GetByteArrayAsync(new Uri($"http://thecatapi.com/api/images/get?format=src&type=jpg")).Result;
        }
    }
}
