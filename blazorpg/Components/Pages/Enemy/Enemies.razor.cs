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

        protected override async Task OnInitializedAsync()
        {
            ListEnemy = new List<Data.Models.Enemy>();
            ListEnemy = (await enemyService.GetEnemies()).Data;
        }

        //public void Create(){
        //    Navigation.NavigateTo("/characters/create");
        //}

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

            //Modal response.Message
        }

        public async Task GetAll()
        {
            var response = await enemyService.GetEnemies();

            ListEnemy = response.Data;

            //return Task.CompletedTask; ns pq
        }
    }
}
