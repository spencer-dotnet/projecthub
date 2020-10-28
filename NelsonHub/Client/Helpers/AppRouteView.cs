//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Rendering;
//using NelsonHub.Client.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Threading.Tasks;

//namespace NelsonHub.Client.Helpers
//{
//    /// <summary>
//    /// The app route view component is used inside the app component and renders the page component
//    /// for the current route along with its layout.
//    /// 
//    /// If the page component for the route contains an authorize attribute (@attribute [Authorize])
//    /// then the user must be logged in, otherwise they will be redirected to the login page.
//    /// 
//    /// The app route view extends the built in ASP.NET Core RouteView component and uses the base class
//    /// to render the page by calling base.Render(builder).
//    /// 
//    /// https://jasonwatmore.com/post/2020/08/13/blazor-webassembly-jwt-authentication-example-tutorial
//    /// 
//    /// </summary>
//    public class AppRouteView : RouteView
//    {
//        [Inject]
//        public NavigationManager NavigationManager { get; set; }

//        [Inject]
//        public IAuthenticationService AuthenticationService {get; set;}

//        protected override void Render(RenderTreeBuilder builder)
//        {
//            var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;
//            if (authorize && AuthenticationService.User == null)
//            {
//                var returnUrl = WebUtility.UrlEncode(new Uri(NavigationManager.Uri).PathAndQuery);
//                NavigationManager.NavigateTo($"login?returnUrl={returnUrl}");
//            }
//            else
//            {
//                base.Render(builder);
//            }
//        }
//    }
//}
