using blazorpg.Data.Models;

namespace blazorpg.Data.Services;

public class CharacterTypeService
{
    public async Task<Response<List<CharacterType>>> GetCharacterTypes()
    {
        Response<List<CharacterType>> response = new Response<List<CharacterType>>();
        List<CharacterType> ListCharacterType = new List<CharacterType>();

        try
        {
            response = await Consumer.Execute<List<CharacterType>>("https://localhost:7082/api/CharacterType", methodHttp.GET, ListCharacterType);
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
            response = await Consumer.Execute<string>($"https://localhost:7082/api/CharacterType/{characterTypeId}", methodHttp.DELETE, null);
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
            response = await Consumer.Execute<CharacterType>($"https://localhost:7082/api/CharacterType/{characterTypeId}", methodHttp.PUT, characterType);
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
            response = (await Consumer.Execute<CharacterType>("https://localhost:7082/api/CharacterType", methodHttp.POST, characterType));

        }
        catch
        {

        }

        return response;
    }
}