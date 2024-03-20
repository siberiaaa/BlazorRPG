using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using blazorpg.Data.Models;
using blazorpg.Data.Services;

namespace blazorpg.Components.Layout;

public partial class MainLayout
{

    [Inject]
    ProtectedSessionStorage ProtectedSessionStore { get; set; }
    string? token;

    protected override async Task OnInitializedAsync()
    {
        var jwt = await ProtectedSessionStore.GetAsync<string>("jwt");
        token = jwt.Success ? jwt.Value : "";
    }


}
