using System;
using RickAndMortySolution.Models;
using System.Threading.Tasks;

namespace RickAndMortySolution.IServices
{
	public interface IRickMortyApi
	{
		public Task<CharactersRequest> GetAll(int? page = null);
        public Task<Character> Get(int id);
    }
}

