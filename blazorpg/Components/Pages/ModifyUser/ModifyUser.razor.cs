using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;
using blazorpg.Data.Services;

namespace blazorpg.Components.Pages.Character;

public partial class ModifyUser : ComponentBase
{
    public Data.Models.User user = new();

    public string message = "";
    public async Task ModifyUser()
    {
        

    }
}
