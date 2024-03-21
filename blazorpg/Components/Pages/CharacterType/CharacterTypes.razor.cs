using Microsoft.AspNetCore.Components;
using blazorpg.Data.Services;

namespace blazorpg.Components.Pages.CharacterType;

public partial class CharacterTypes : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }
    [Inject]
    public CharacterTypeService CharacterTypeService { get; set; }

    public List<Data.Models.CharacterType>? ListCharacterType { get; set; }

    private CharacterTypeCreate characterTypeCreate;
    public string message = "";

    protected override async Task OnParametersSetAsync()
    {
        ListCharacterType = new List<Data.Models.CharacterType>();
        var response = await CharacterTypeService.GetCharacterTypes();

        if (response.Ok)
        {
            ListCharacterType = response.Data;
        }
        else
        {
            message = response.Message;
        }
    }

    public async Task Remove(string id)
    {
        var response = await CharacterTypeService.DeleteCharacterType(id);

        await GetAll(); //ay dios

    }

    public async Task Update(string id, Data.Models.CharacterType character)
    {
        var response = await CharacterTypeService.UpdateCharacterType(id, character);
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
        var response = await CharacterTypeService.GetCharacterTypes();

        if (response.Ok)
        {
            ListCharacterType = response.Data;
        }
        else
        {
            message = response.Message;
        }



        //return Task.CompletedTask; ns pq
    }

}
