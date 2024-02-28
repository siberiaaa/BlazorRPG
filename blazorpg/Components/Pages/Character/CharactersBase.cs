using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;

namespace blazorpg.Components;

public class CharactersBase : ComponentBase
{
    [Inject]
    public NavigationManager Navigation {get;set;}

    public List<Character>? ListCharacter {get;set;}

        protected override async Task OnInitializedAsync()
        {
            ListCharacter = new List<Character>();
            
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7128/api/Character";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                ListCharacter = JsonConvert.DeserializeObject<List<Character>>(responseContent); 
                // Process the response data here
            }
        }

        public void Create(){
            Navigation.NavigateTo("/characters/create");
        }
























    public string ID {get; set;}
    public string Name {get; set;}
    public string Level {get; set;}
    public string HP {get; set;}
    public string MP {get; set;}
    public List<Character> CharacterList { get; set; } = new List<Character>();

    // protected override async Task OnInitializedAsync()
    // {
    //     CharacterList = new List<Character>(){new Character};
    // }

    public void Add()
    {
        //Navigation.NavigateTo("/character/add");

        CharacterList.Add(new Character{
            ID = int.Parse(this.ID),
            Name = this.Name,
            Level = int.Parse(this.Level),
            HP = int.Parse(this.HP),
            MP = int.Parse(this.MP)
        });
    }
    public void Modify(int id)
    {
        int i = CharacterList.FindIndex(c => c.ID == id);
        CharacterList[i].Name = this.Name;
        CharacterList[i].Level = int.Parse(this.Level);
        CharacterList[i].HP = int.Parse(this.HP);
        CharacterList[i].MP = int.Parse(this.MP);
    }
    public void Delete(int id)
    {
        int i = CharacterList.FindIndex(c => c.ID == id);
        CharacterList.RemoveAt(i);
    }
}
