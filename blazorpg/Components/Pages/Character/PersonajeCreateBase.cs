using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;
using AppBlazor.Data.Models;

namespace AppBlazor.Components
{
    public class PersonajeCreateBase : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        public Personaje personaje = new();
        public string mensaje = "";
        public async Task Post()
        {

            string apiUrl = "https://localhost:7128/api/Personaje?nombre=Steve&estamina";
            var data = new StringContent(JsonConvert.SerializeObject(personaje), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();
            data.Headers.Add("Authorization", "valor");
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, data);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                // Process the response data here
                Navigation.NavigateTo("/personajes");

            }
            else
            {
                mensaje = responseContent;
            }

        }
    }
}