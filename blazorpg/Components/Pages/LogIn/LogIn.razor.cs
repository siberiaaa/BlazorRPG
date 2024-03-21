using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;
using blazorpg.Data.Services;

namespace blazorpg.Components.Pages.LogIn;

public partial class LogIn : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }
    [Inject]
    public UserService UserService { get; set; }

    public User user = new();

    public string message = "";
    public bool login { get; set; } = true;

    public void ComponentLogin()
    {
        login = true;
    }
    public void ComponentSignin()
    {
        login = false;
    }
    public async Task Login()
    {
        message = "";
        Response<string> respuesta = await UserService.Login(user);

        if (respuesta.Ok)
        {
            
            Navigation.NavigateTo("/", forceLoad: true);
        }
        else
        {
            message = respuesta.Data;
        }

    }
    public async Task Signin()
    {

        message = "";
        Response<User> respuesta = await UserService.Signin(user);

        if (respuesta.Ok)
        {
            Navigation.NavigateTo("/", forceLoad: true);
            login = true;
        }
        else
        {
            message = respuesta.Message;
        }
    }
}
