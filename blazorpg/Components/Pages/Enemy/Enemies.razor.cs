using blazorpg.Components.Pages.Character;
using blazorpg.Data.Services;
using Microsoft.AspNetCore.Components;

namespace blazorpg.Components.Pages.Enemy
{
    public partial class Enemies : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public EnemyService enemyService { get; set; }

        public List<Data.Models.Enemy>? ListEnemy { get; set; }

        private EnemyCreate enemyCreate;
        public string message = "";

        protected override async Task OnParametersSetAsync()
        {
            ListEnemy = new List<Data.Models.Enemy>();

            var response = await enemyService.GetEnemies();

            if (response.Ok)
            {
                ListEnemy = response.Data;
            }
            else
            {
                message = response.Message;
            }
        }

        public async Task Remove(string id)
        {
            var response = await enemyService.DeleteEnemy(id);

                await GetAll();
  
            //Modal response.Message
        }

        public async Task Update(string id, Data.Models.Enemy enemy)
        {
            var response = await enemyService.UpdateEnemy(id, enemy);
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
            var response = await enemyService.GetEnemies();

            if (response.Ok)
            {
                ListEnemy = response.Data;
            }
            else
            {
                message = response.Message;
            }
        }
    }
}
