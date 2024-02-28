using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using AppBlazor.Data.Models;
using Newtonsoft.Json;

namespace AppBlazor.Components
{
    public class PersonajesBase : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation {get;set;}
        public List<Personaje>? lstPersonaje {get;set;}

        protected override async Task OnInitializedAsync()
        {
            lstPersonaje = new List<Personaje>();
            
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7128/api/Personaje";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                lstPersonaje = JsonConvert.DeserializeObject<List<Personaje>>(responseContent); 
                // Process the response data here
            }
        }

        public void Create(){
            Navigation.NavigateTo("/personaje/create");
        }
    }
}