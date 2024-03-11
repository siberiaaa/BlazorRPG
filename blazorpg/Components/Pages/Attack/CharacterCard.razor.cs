using Microsoft.AspNetCore.Components;

namespace blazorpg.Components.Pages.Attack
{
    public partial class CharacterCard
    {
        [Parameter]
        public blazorpg.Data.Models.Character character { get; set; }
        [Parameter]
        public blazorpg.Data.Models.Enemy enemy { get; set; }


    }
}
