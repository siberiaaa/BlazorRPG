using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;
using blazorpg.Data.Services;
using blazorpg.Components.Pages.Character;

namespace blazorpg.Components.Pages.LogIn;

public partial class LogIn : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }
    [Inject]
    public UserService UserService { get; set; }

    public Data.Models.User user = new();

    public string message = "";
    public bool login { get; set; }

    public LogIn()
    {
        login = true;
    }

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
            Navigation.NavigateTo("/");
        }
        else
        {
            message = respuesta.Message;
        }

    }
    public async Task Signin()
    {

        message = "";
        Response<User> respuesta = await UserService.Signin(user);

        if (respuesta.Ok)
        {
            Navigation.NavigateTo("/");
            login = true;
        }
        else
        {
            message = respuesta.Message;
        }
    }
}
