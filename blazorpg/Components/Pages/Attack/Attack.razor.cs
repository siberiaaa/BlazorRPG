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
		public string message = "";

		protected override async Task OnParametersSetAsync()
		{
			ListCharacter = new List<Data.Models.Character>();
			var responsec = await CharacterService.GetCharacters();

			if (responsec.Ok)
			{
				ListCharacter = responsec.Data;
			}
			else
			{
				message = responsec.Message;
			}

			ListEnemy = new List<Data.Models.Enemy>();
			var responsee = await EnemyService.GetEnemies();

			if (responsee.Ok)
			{
				ListEnemy = responsee.Data;
			}
			else
			{
				message = responsee.Message;
			}
		}

		public async Task StartAttack()
		{
			AttackResult = "";
			Response<string> response = await CharacterService.AttackEnemy(idCharacter.ToString(), idEnemy.ToString());

			if (response.Ok)
			{
				AttackResult = response.Data;
				ModalDialog.Open();
			}
			else
			{
				message = response.Message;
				AttackResult = response.StatusCode;
				ModalDialog.Open();
			}




		}

	}
}
