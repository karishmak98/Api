﻿using Newtonsoft.Json;
using ParkWeb_12.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkWeb_12.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAsync(string url, T objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
                var client = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    return true;
                else
                   return false;
            
        }

        public async Task<bool> DeleteAsync(string url, int id)
        {

             var request=new HttpRequestMessage(HttpMethod.Delete,url+id.ToString());
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response=await client.SendAsync(request);
            if(response.StatusCode==System.Net.HttpStatusCode.OK)
                    return true;
            else
                return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var request=new HttpRequestMessage(HttpMethod.Get,url);
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if(response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            return null;
        }

        public async Task<T> GetAsync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url + id.ToString());
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;
        }

        public async Task<bool> UpdateAsync(string url, T objToUpdate)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            if(objToUpdate!=null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(objToUpdate), Encoding.UTF8, "application/json");
            }
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response=await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                return true;
            else
                return false;
        }
    }
}
