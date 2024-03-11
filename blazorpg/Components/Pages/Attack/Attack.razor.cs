using blazorpg.Components.Pages.Character;
using blazorpg.Components.Shared;
using blazorpg.Data.Models;
using blazorpg.Data.Services;
using Microsoft.AspNetCore.Components;

namespace blazorpg.Components.Pages.Attack
{
    public partial class Attack
    {
		[Inject]
		public CharacterService CharacterService { get; set; }
		[Inject]
		public EnemyService EnemyService { get; set; }
		public List<Data.Models.Character>? ListCharacter { get; set; }
		public List<Data.Models.Enemy>? ListEnemy { get; set; }

		public int idEnemy { get; set; }
		public int idCharacter { get; set; }
		public Modal ModalDialog;
		public string AttackResult;

		protected override async Task OnInitializedAsync()
		{
			ListCharacter = new List<Data.Models.Character>();
			ListCharacter = (await CharacterService.GetCharacters()).Data;

			ListEnemy = new List<Data.Models.Enemy>();
			ListEnemy = (await EnemyService.GetEnemies()).Data;
		}

		public async Task StartAttack()
		{
			AttackResult = "";
			Response<string> respuesta = await CharacterService.AttackEnemy(idCharacter.ToString(), idEnemy.ToString());

			if (respuesta.Ok)
			{
				AttackResult = respuesta.Data;
				ModalDialog.Open();
			}
			else
			{
				AttackResult = respuesta.StatusCode;
				ModalDialog.Open();
			}




		}

	}
}
