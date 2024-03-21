using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;

namespace blazorpg.Components.Layout;

public partial class MainLayout
{
    [Inject]
    public NavigationManager Navigation { get; set; }
    [Inject]
    ProtectedLocalStorage ProtectedLocalStorage { get; set; }
    string? token = "";

    private bool isConnected;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            isConnected = true;
            await LoadStateAsync();
            StateHasChanged();
        }
    }

    private async Task LoadStateAsync()
    {
        try{
            var jwt = await ProtectedLocalStorage.GetAsync<string>("jwt");
            token = jwt.Success ? jwt.Value : "";
        }
        catch(CryptographicException ex)
        {
            logout();
        }
        
    }

    public async void logout()
    {
        await ProtectedLocalStorage.DeleteAsync("jwt");
        Navigation.NavigateTo("/", forceLoad: true);
    }


}
