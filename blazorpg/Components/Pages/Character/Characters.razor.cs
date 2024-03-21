using Microsoft.AspNetCore.Components;
using blazorpg.Data.Services;

namespace blazorpg.Components.Pages.Character;

public partial class Characters : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }
    [Inject]
    public CharacterService CharacterService { get; set; }
    public List<Data.Models.Character>? ListCharacter { get; set; }

    public string message = "";

    private CharacterCreate characterCreate;

    protected override async Task OnParametersSetAsync()
    {
        ListCharacter = new List<Data.Models.Character>();
        var response = await CharacterService.GetCharacters();

        if (response.Ok)
        {
            ListCharacter = response.Data;
        }
        else
        {
            message = response.Message;
        }
    }

    public async Task Remove(string id)
    {
        var response = await CharacterService.DeleteCharacter(id);

        await GetAll();

    }

    public async Task Update(string id, Data.Models.Character character)
    {
        var response = await CharacterService.UpdateCharacter(id, character);
        if (response.Ok)
        {
            await GetAll();
        }
        else
        {
            message = response.Message;
        }

        //Modal response.Message
    }

    public async Task GetAll()
    {
        var response = await CharacterService.GetCharacters();


        if (response.Ok)
        {
            ListCharacter = response.Data;
        }
        else
        {
            message = response.Message;
        }



        //return Task.CompletedTask; ns pq
    }

}
