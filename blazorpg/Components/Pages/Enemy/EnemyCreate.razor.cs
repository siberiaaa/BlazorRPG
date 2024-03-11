using blazorpg.Data.Models;
using blazorpg.Data.Services;
using Microsoft.AspNetCore.Components;

namespace blazorpg.Components.Pages.Enemy;

    public partial class EnemyCreate : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public EnemyService EnemyService { get; set; }
        [Parameter]
        public EventCallback UpdateList { get; set; }

        public Data.Models.Enemy enemy = new();

        public string message = "";
        public async Task Post()
        {
            message = "";

            Response<Data.Models.Enemy> respuesta = await EnemyService.AddEnemy(enemy);

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
