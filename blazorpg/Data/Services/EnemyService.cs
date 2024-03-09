using blazorpg.Data.Models;
using Core.Entities;

namespace blazorpg.Data.Services;

public class EnemyService
{
    public async Task<Response<List<Enemy>>> GetEnemies()
    {
        Response<List<Enemy>> response = new Response<List<Enemy>>();
        List<Enemy> ListEnemies = new List<Enemy>();

        try
        {
            response = await Consumer.Execute<List<Enemy>>("https://localhost:7082/api/Enemy", methodHttp.GET, ListEnemies);
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
            response = await Consumer.Execute<string>($"https://localhost:7082/api/Enemy?id={enemyId}", methodHttp.DELETE, null);
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
            response = await Consumer.Execute<Enemy>($"https://localhost:7082/api/Enemy?id={enemyId}", methodHttp.PUT, enemy);
        }
        catch (Exception ex)
        {

        }
        return response;
    }

    public async Task<Response<string>> AddEnemy(Enemy enemy)
    {
        Response<string> response = new Response<string>();
        try
        {
            response.Message = (await Consumer.Execute<Enemy>("https://localhost:7082/api/Enemy", methodHttp.GET, enemy)).Message;

        }
        catch
        {

        }

        return response;
    }
}