using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;
using blazorpg.Data.Services;

namespace blazorpg.Components.Pages.ModifyUser;

public partial class ModifyUser : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }
    [Inject]
    public UserService UserService { get; set; }
    public User user = new();
    public string message = "";
    public async Task Modify()
    {
        message = "";
        Response<User> respuesta = await UserService.ModifyUser(user);

        if (respuesta.Ok)
        {
            Navigation.NavigateTo("/", forceLoad: true);
        }
        else
        {
            message = respuesta.Message;
        }


    }
}
