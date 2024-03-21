using System.Security.Cryptography;
using blazorpg.Data.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace blazorpg.Data.Services;

public class CharacterTypeService
{
    public readonly ProtectedLocalStorage _protectedLocalStorage;
    public CharacterTypeService(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }
    public async Task<Response<List<CharacterType>>> GetCharacterTypes()
    {
        Response<List<CharacterType>> response = new Response<List<CharacterType>>();
        List<CharacterType> ListCharacterType = new List<CharacterType>();

        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<List<CharacterType>, List<CharacterType>>("https://localhost:7082/api/CharacterType", methodHttp.GET, ListCharacterType, token);
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
    public async Task<Response<string>> DeleteCharacterType(string characterTypeId)
    {
        Response<string> response = new Response<string>();
        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<string, string>($"https://localhost:7082/api/CharacterType/{characterTypeId}", methodHttp.DELETE, null, token);
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

    public async Task<Response<CharacterType>> UpdateCharacterType(string characterTypeId, CharacterType characterType)
    {
        Response<CharacterType> response = new Response<CharacterType>();
        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<CharacterType, CharacterType>($"https://localhost:7082/api/CharacterType/{characterTypeId}", methodHttp.PUT, characterType, token);
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

    public async Task<Response<CharacterType>> AddCharacterType(CharacterType characterType)
    {
        Response<CharacterType> response = new Response<CharacterType>();
        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<CharacterType, CharacterType>("https://localhost:7082/api/CharacterType", methodHttp.POST, characterType, token);
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
}