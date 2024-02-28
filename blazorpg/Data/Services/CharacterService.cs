using blazorpg.Data.Models;
using Newtonsoft.Json;
using System.Text; //encoding


namespace blazorpg.Data.Services;

public class CharacterService
{
    public async Task<List<Character>> GetCharacters()
    {
        HttpClient httpClient = new HttpClient();

        string apiUrl = "https://localhost:7082/api/Character";
        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();

            List<Character> ListCharacter = JsonConvert.DeserializeObject<List<Character>>(responseContent); 

            return ListCharacter;
        }
        else
        {
            return new List<Character>();
        }
    }

    public async Task<OkResponse> AddCharacter(Character character)
    {
        string apiUrl = "https://localhost:7082/api/Character";  //7128
        var data = new StringContent(JsonConvert.SerializeObject(character), Encoding.UTF8, "application/json");

        HttpClient httpClient = new HttpClient();
        //data.Headers.Add("Authorization", "value"); //header example
        HttpResponseMessage response = await httpClient.PostAsync(apiUrl, data);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return OkResponse.yes;

        }
        else
        {
            return OkResponse.no;
        }
    }
}
