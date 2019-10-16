using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DietaProjecao.Models
{
    class TacoService
    {
        private const string API_URI = "https://taco-food-api.herokuapp.com/api/v1/food";

        public async Task<List<Alimento>> GetListAlimentosAsync()
        {
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Add("Accept", "application/json");

                var resultado = await cliente.GetStringAsync(API_URI);

                var alimentos = JsonConvert.DeserializeObject<List<Alimento>>(resultado);

                return alimentos;
            }
        }

        public async Task<Alimento> GetAlimentoAsync(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Add("Accept", "application/json");

                var resultado = await cliente.GetStringAsync(API_URI + $"/{id}");

                var alimento = JsonConvert.DeserializeObject<List<Alimento>>(resultado);

                return alimento.FirstOrDefault();
            }
        }
    }
}
