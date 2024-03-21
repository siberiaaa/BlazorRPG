using System.Security.Cryptography;
using blazorpg.Data.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace blazorpg.Data.Services;

public class EnemyService
{
    public readonly ProtectedLocalStorage _protectedLocalStorage;
    public EnemyService(ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
    }
    public async Task<Response<List<Enemy>>> GetEnemies()
    {
        Response<List<Enemy>> response = new Response<List<Enemy>>();
        List<Enemy> ListEnemies = new List<Enemy>();

        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<List<Enemy>, List<Enemy>>("https://localhost:7082/api/Enemy", methodHttp.GET, ListEnemies, token);
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
    public async Task<Response<string>> DeleteEnemy(string enemyId)
    {
        Response<string> response = new Response<string>();
        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<string, string>($"https://localhost:7082/api/Enemy/{enemyId}", methodHttp.DELETE, null, token);
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

    public async Task<Response<Enemy>> UpdateEnemy(string enemyId, Enemy enemy)
    {
        Response<Enemy> response = new Response<Enemy>();
        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<Enemy, Enemy>($"https://localhost:7082/api/Enemy/{enemyId}", methodHttp.PUT, enemy, token);
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

    public async Task<Response<Enemy>> AddEnemy(Enemy enemy)
    {
        Response<Enemy> response = new Response<Enemy>();
        try
        {
            var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
            string token = jwt.Success ? jwt.Value : "";
            response = await Consumer.Execute<Enemy, Enemy>("https://localhost:7082/api/Enemy", methodHttp.POST, enemy, token);

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