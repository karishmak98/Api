using ParkWeb_12.Models;
using ParkWeb_12.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParkWeb_12.Repository
{
    public class TrailRepository:Repository<Trail>,ITrailRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TrailRepository(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
