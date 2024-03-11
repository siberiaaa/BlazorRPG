using blazorpg.Data.Models;
using blazorpg.Data.Services;
using Microsoft.AspNetCore.Components;

namespace blazorpg.Components.Pages.CharacterType
{
    public partial class CharacterTypeCreate : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public CharacterTypeService CharacterTypeService { get; set; }
        [Parameter]
        public EventCallback UpdateList { get; set; }

        public Data.Models.CharacterType characterType = new();

        public string message = "";
        public async Task Post()
        {
            message = "";

            Response<Data.Models.CharacterType> respuesta = await CharacterTypeService.AddCharacterType(characterType);

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
}
