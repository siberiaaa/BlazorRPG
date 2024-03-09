using Microsoft.AspNetCore.Components;

namespace blazorpg.Components.Pages.Counter;

public partial class Counter : ComponentBase
{
	public int currentCount = 0; 

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
}