using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json.Linq;


namespace blazorpg.Components.Pages.Counter;

public partial class Counter : ComponentBase
{
	public int currentCount = 0;

	[Inject]
	ProtectedSessionStorage ProtectedSessionStore { get; set; }

	[Parameter]
	public Action<int> OnMultipleOfTwoAction { get; set; }

	[Parameter]
	public EventCallback<int> OnMultipleOfThree { get; set; }

	public async Task IncrementCount()
	{
		currentCount++;
		if (currentCount % 2 == 0)
			OnMultipleOfTwoAction?.Invoke(currentCount);

		if (currentCount % 3 == 0)
			await OnMultipleOfThree.InvokeAsync(currentCount);


	}

	public async Task storesomething()
	{
		await ProtectedSessionStore.SetAsync("lalalala", "aaaaaa");
		Console.WriteLine("listo sett");


	}

	public async Task getsomething()
	{
		var jwt = await ProtectedSessionStore.GetAsync<string>("lalalala");
		Console.WriteLine("listo gett");
		Console.WriteLine(jwt.Success);




	}



}