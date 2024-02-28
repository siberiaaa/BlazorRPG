using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;
using blazorpg.Data.Services;

namespace blazorpg.Components;

public class CharactersBase : ComponentBase
{
    [Inject]
    public NavigationManager Navigation {get;set;}
    [Inject]
    public CharacterService CharacterService { get; set; }

    public List<Character>? ListCharacter {get;set;}

        protected override async Task OnInitializedAsync()
        {
            ListCharacter = new List<Character>();
            ListCharacter = await CharacterService.GetCharacters();
        }

        public void Create(){
            Navigation.NavigateTo("/characters/create");
        }

}
