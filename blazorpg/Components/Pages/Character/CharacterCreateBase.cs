using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;
using blazorpg.Data.Services;

namespace blazorpg.Components;

public class CharacterCreateBase : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }
    [Inject]
    public CharacterService CharacterService { get; set; }

    public Character character = new();
    public string message = "";
    public async Task Post()
    {

        OkResponse added = await CharacterService.AddCharacter(character);

        if (added == OkResponse.yes)
        {
            Navigation.NavigateTo("/characters");
        }
        else
        {
            message = "a";
        }

    }
}
