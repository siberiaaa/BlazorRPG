using blazorpg.Data.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Text; //encoding


namespace blazorpg.Data.Services;

public class CharacterService
{
    public async Task<Response<List<Character>>> GetCharacters()
    {
        Response<List<Character>> response = new Response<List<Character>>();
        List<Character> ListCharacter = new List<Character>();

        try
        {
            response = await Consumer.Execute<List<Character>>("https://localhost:7082/api/Character", methodHttp.GET, ListCharacter);
        }
        catch(Exception ex)
        {

        }
        return response;
    }
    public async Task<Response<string>> DeleteCharacter(string characterId)
    {
        Response<string> response = new Response<string>();
        try
        {
            response = await Consumer.Execute<string>($"https://localhost:7082/api/Character?id={characterId}", methodHttp.DELETE, null);
        }
        catch(Exception ex)
        {

        }
        return response;
    }

    public async Task<Response<Character>> UpdateCharacter(string characterId, Character character)
    {
        Response<Character> response = new Response<Character>();
        try
        {
            response = await Consumer.Execute<Character>($"https://localhost:7082/api/Character?id={characterId}", methodHttp.PUT, character);
        }
        catch (Exception ex)
        {

        }
        return response;
    }

    public async Task<Response<string>> AddCharacter(Character character)
    {
        Response<string> response = new Response<string>();
        try
        {
            response.Message = (await Consumer.Execute<Character>("https://localhost:7082/api/Character", methodHttp.GET, character)).Message;
            
            //!return response.Message;

            // string apiUrl = "https://localhost:7082/api/Character";  //7128
            // var data = new StringContent(JsonConvert.SerializeObject(character), Encoding.UTF8, "application/json");

            // HttpClient httpClient = new HttpClient();
            // //data.Headers.Add("Authorization", "value"); //header example
            // HttpResponseMessage response = await httpClient.PostAsync(apiUrl, data);
            // string responseContent = await response.Content.ReadAsStringAsync();
            // if (response.IsSuccessStatusCode)
            // {
            //     return OkResponse.yes;

            // }
            // else
            // {
            //     return OkResponse.no;
            // }

            
        }
        catch
        {

        }

        return response;    
    }

    public async Task<Response<Character>> HealCharacter(string characterId)
    {
        Response<Character> response = new Response<Character>();
        try
        {
            response = await Consumer.Execute<Character>($"https://localhost:7082/api/Character?id={characterId}", methodHttp.POST, null);
        }
        catch (Exception ex)
        {

        }
        return response;
    }

    public async Task<Response<string>> AttackEnemy(string characterId, string enemyId)
    {
        Response<string> response = new Response<string>();
        try
        {
            response = await Consumer.Execute<string>($"https://localhost:7082/api/Character?idCharacter={characterId}&idEnemy={enemyId}", methodHttp.POST, null);
        }
        catch (Exception ex)
        {

        }
        return response;

    }



}
