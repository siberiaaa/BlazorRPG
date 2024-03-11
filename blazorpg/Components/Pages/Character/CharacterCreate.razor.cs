using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;
using blazorpg.Data.Services;

namespace blazorpg.Components.Pages.Character;

public partial class CharacterCreate : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }
    [Inject]
    public CharacterService CharacterService { get; set; } 
    [Parameter]
    public EventCallback UpdateList { get; set; }

    public Data.Models.Character character = new();

    public string message = "";
    public async Task Post()
    {
        message = "";
        Response<Data.Models.Character> respuesta = await CharacterService.AddCharacter(character);

        if (respuesta.Ok)
        {
            await UpdateList.InvokeAsync();
            //Navigation.NavigateTo("/characters");
        }
        else
        {
            message = respuesta.Message;
        }

    }
}
