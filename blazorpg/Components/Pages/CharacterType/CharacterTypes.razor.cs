using Microsoft.AspNetCore.Components;
using blazorpg.Data.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace blazorpg.Components.Pages.CharacterType;

public partial class CharacterTypes : ComponentBase
{
    [Inject]
    public NavigationManager Navigation { get; set; }
    [Inject]
    public CharacterTypeService CharacterTypeService { get; set; }

    public List<Data.Models.CharacterType>? ListCharacterType { get; set; }

    private CharacterTypeCreate characterTypeCreate;

    protected override async Task OnInitializedAsync()
    {
        ListCharacterType = new List<Data.Models.CharacterType>();
        ListCharacterType = (await CharacterTypeService.GetCharacterTypes()).Data;
    }

    //public void Create()
    //{
    //    Navigation.NavigateTo("/charactertype/create");
    //}

    public async Task Remove(string id)
    {
        var response = await CharacterTypeService.DeleteCharacterType(id);
    
            await GetAll(); //ay dios
        

        //Modal response.Message
    }

    public async Task Update(string id, Data.Models.CharacterType character)
    {
        var response = await CharacterTypeService.UpdateCharacterType(id, character);
        if (response.Ok)
        {
            await GetAll();
        }

        //Modal response.Message
    }

    public async Task GetAll()
    {
        var response = await CharacterTypeService.GetCharacterTypes();

        ListCharacterType = response.Data;

        //return Task.CompletedTask; ns pq
    }

}
