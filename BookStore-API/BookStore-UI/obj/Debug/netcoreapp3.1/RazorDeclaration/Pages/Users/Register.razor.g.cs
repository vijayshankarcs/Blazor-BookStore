#pragma checksum "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\Pages\Users\Register.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "503ddc7329d25f8caa3f8fc0642674a13c179c2b"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BookStore_UI.Pages.Users
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\_Imports.razor"
using BookStore_UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\_Imports.razor"
using BookStore_UI.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\Pages\Users\Register.razor"
using BookStore_UI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\Pages\Users\Register.razor"
using BookStore_UI.Contracts;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Register")]
    public partial class Register : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 41 "C:\MydeskTop\Development\LAB\c#\Blazor\Core\BookStore-API\BookStore-UI\Pages\Users\Register.razor"
       
    private RegistrationModel model = new RegistrationModel();
    bool isFailed = false;
    private async Task HandleRegistration()
    {
        var response = await _authRepo.Register(model);

        if (response)
        {
            _navMan.NavigateTo("/");
        }
        else
        {
            isFailed = true;
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager _navMan { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IAuthenticationRepository _authRepo { get; set; }
    }
}
#pragma warning restore 1591
