using blazorpg.Data.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text; //encoding


namespace blazorpg.Data.Services;

public class CharacterService
{
    public readonly ProtectedLocalStorage _protectedLocalStorage;
    public CharacterService(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }


    public async Task<Response<List<Character>>> GetCharacters()
    {
        Response<List<Character>> response = new Response<List<Character>>();
        List<Character> ListCharacter = new List<Character>();

        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<List<Character>, List<Character>>("https://localhost:7082/api/Character", methodHttp.GET, ListCharacter, token);
        }
        catch(CryptographicException ex)
        {
            await _protectedLocalStorage.DeleteAsync("jwt");
        }
        catch (Exception ex)
        {

        }
        return response;
    }
    public async Task<Response<string>> DeleteCharacter(string characterId)
    {
        Response<string> response = new Response<string>();
        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<string, string>($"https://localhost:7082/api/Character/{characterId}", methodHttp.DELETE, null, token);
        }
        catch(CryptographicException ex)
        {
            await _protectedLocalStorage.DeleteAsync("jwt");
        }
        catch (Exception ex)
        {

        }
        return response;
    }

    public async Task<Response<Character>> UpdateCharacter(string characterId, Character character)
    {
        Response<Character> response = new Response<Character>();
        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<Character, Character>($"https://localhost:7082/api/Character/{characterId}", methodHttp.PUT, character, token);
        }
        catch(CryptographicException ex)
        {
            await _protectedLocalStorage.DeleteAsync("jwt");
        }
        catch (Exception ex)
        {

        }
        return response;
    }

    public async Task<Response<Character>> AddCharacter(Character character)
    {
        Response<Character> response = new Response<Character>();
        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<Character, Character>("https://localhost:7082/api/Character", methodHttp.POST, character, token);


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
        catch(CryptographicException ex)
        {
            await _protectedLocalStorage.DeleteAsync("jwt");
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
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<Character, Character>($"https://localhost:7082/api/Character/Heal?id={characterId}", methodHttp.POST, null, token);
        }
        catch(CryptographicException ex)
        {
            await _protectedLocalStorage.DeleteAsync("jwt");
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
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<string, string>($"https://localhost:7082/api/Character/AttackEnemy?idCharacter={characterId}&idEnemy={enemyId}", methodHttp.POST, "", token);
        }
        catch(CryptographicException ex)
        {
            await _protectedLocalStorage.DeleteAsync("jwt");
        }
        catch (Exception ex)
        {

        }
        return response;

    }



}
