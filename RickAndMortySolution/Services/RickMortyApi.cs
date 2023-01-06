using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RickAndMortySolution.IServices;
using RickAndMortySolution.Models;

namespace RickAndMortySolution.Services
{
    public class RickMortyApi : IRickMortyApi
    {

        private readonly IConfiguration configuration;
        private HttpClient httpClient = new HttpClient();

        public RickMortyApi(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient.BaseAddress = new Uri(configuration.GetSection("AppSettings:RickMortyUrl").Value);
            this.httpClient.DefaultRequestHeaders.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Method that search for a single character in rick and m. Api rest.
        /// </summary>
        /// <param name="id">The id of the character to search for.</param>
        /// <returns>
        /// A Task with the result of 'Character' of the id searched.
        /// </returns>
        public async Task<Character> Get(int id)
        {
            try
            {
                using var client = httpClient;
                HttpResponseMessage Res = await client.GetAsync($"/api/character/{id}");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Character>(EmpResponse);

                    return response;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Method that search for a list of characters in rick and m. Api, by providing a index of page.
        /// </summary>
        /// <param name="page">
        /// The page of the list of characters to add in query param request,
        /// by defualt null will not add the query param 'page'</param>
        /// <returns>
        /// A task of the result of the request 'CharacterRequest' wich contains the dto info (pages, actual page, next url)
        /// and the body, list of Characters
        /// </returns>
        public async Task<CharactersRequest> GetAll(int? page = null)
        {
            try
            {
                using var client = httpClient;
                HttpResponseMessage Res = await client.GetAsync($"/api/character{(page is not null ? "?page=" + page : "")}");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<CharactersRequest>(EmpResponse);

                    return response;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

