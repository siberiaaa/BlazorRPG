using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;
using blazorpg.Data.Services;

namespace blazorpg.Components.Pages.Character;

public partial class Characters : ComponentBase
{
    [Inject]
    public NavigationManager Navigation {get;set;}
    [Inject]
    public CharacterService CharacterService { get; set; }

    public List<Data.Models.Character>? ListCharacter {get;set;}

    private CharacterCreate characterCreate;

        protected override async Task OnInitializedAsync()
        {
            ListCharacter = new List<Data.Models.Character>();
            ListCharacter = (await CharacterService.GetCharacters()).Data;
        }

        //public void Create(){
        //    Navigation.NavigateTo("/characters/create");
        //}

        public async Task Remove(string id)
        {
            var response = await CharacterService.DeleteCharacter(id);


                await GetAll();
        

        //Modal response.Message
    }

        public async Task Update(string id, Data.Models.Character character)
        {
            var response = await CharacterService.UpdateCharacter(id, character);
            if (response.Ok)
            {
                await GetAll();
            }

        //Modal response.Message
    }

    public async Task GetAll()
        {
            var response = await CharacterService.GetCharacters();

            ListCharacter = response.Data;

            //return Task.CompletedTask; ns pq
        }

}
