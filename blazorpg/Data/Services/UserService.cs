using System.Security.Cryptography;
using blazorpg.Data.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace blazorpg.Data.Services;

public class UserService
{
	public readonly ProtectedLocalStorage _protectedLocalStorage;
	public UserService(ProtectedLocalStorage protectedLocalStorage)
	{
		_protectedLocalStorage = protectedLocalStorage;
	}


	public async Task<Response<User>> Signin(User user)
	{
		Response<User> response = new Response<User>();
		try
		{
			response = await Consumer.Execute<User, User>($"https://localhost:7082/api/User", methodHttp.POST, user);
		}
		catch (Exception ex)
		{

		}
		return response;
	}

	public async Task<Response<User>> ModifyUser(User user)
	{
		Response<User> response = new Response<User>();
		try
		{
			var jwt = await _protectedLocalStorage.GetAsync<string>("jwt");
			string token = jwt.Success ? jwt.Value : "";
			response = await Consumer.Execute<User, User>($"https://localhost:7082/api/User/{user.ID}", methodHttp.PUT, user, token);
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
	public async Task<Response<string>> Login(User user)
	{
		Response<string> response = new Response<string>();
		try
		{
			response = await Consumer.Execute<User, string>($"https://localhost:7082/api/User/Login", methodHttp.POST, user);

			if (response.Ok)
			{
				await _protectedLocalStorage.SetAsync("jwt", response.Data);
			}
		}
		catch (Exception ex)
		{

		}
		return response;

	}
	public async void Logout()
	{
		await _protectedLocalStorage.DeleteAsync("jwt");
	}
}