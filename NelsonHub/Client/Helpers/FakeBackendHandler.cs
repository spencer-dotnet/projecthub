using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NelsonHub.Client.Helpers
{
    /// <summary>
    /// In order to run and test the Blazor application without a read backend API,
    /// the example uses a fake backend handler that intercepts the HTTP requests from
    /// the blazor app and sends back "fake" responses. The fake backend handler inherits
    /// from the ASP.NET Core HttpClientHandler class and is configured with the http client
    /// in Program.cs
    /// 
    /// The fake backend checks if the request maches one of the faked routes, at the moment
    /// this includes POST requests to the /users/authenticate route for handling authentication,
    /// and GET requests to the /users froute for getting all users.
    /// 
    /// Requests to the authenticate route are handled by the authenticate() function which
    /// checks the usernae and password against an array of hardcoded users. If the username and
    /// password are correct then an ok response is returned with the user details and a fake jwt
    /// token, otherwise an error response is returned.
    /// 
    /// Requests to the get users route are handled by the getUsers() function which checks if the user
    /// is logged in by calling the new isLoggedIn() helper function. If the user is logged in an ok()
    /// response with hte whole users array is returned, otherwise a 401 Unauthorized response
    /// is returned by calling the new unauthrorized() helper function.
    /// 
    /// If the request doesn't match any of the faked routes it is passed through as a real HTTP request
    /// to the backend API.
    /// </summary>
    public class FakeBackendHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var users = new[]
            {
                new { Id = 1, Username = "test", Password="test", FirstName="test", LastName="User"}
            };

            var path = request.RequestUri.AbsolutePath;
            var method = request.Method;

            if (path == "/users/authenticate" && method == HttpMethod.Post)
            {
                return await authenticate();
            }
            else if (path == "/users" && method == HttpMethod.Get)
            {
                return await getUsers();
            }
            else
            {
                // pass through any requests not handled above
                return await base.SendAsync(request, cancellationToken);
            }

            // route functions

            async Task<HttpResponseMessage> authenticate()
            {
                var bodyJson = await request.Content.ReadAsStringAsync();
                var body = JsonSerializer.Deserialize<Dictionary<string, string>>(bodyJson);
                var user = users.FirstOrDefault(x => x.Username == body["username"] && x.Password == body["password"]);

                if (user == null)
                    return await error("Username or password is incorrect");

                return await ok(new {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastNmae = user.LastName,
                    Token = "fake-jwt-token"
                });
            }

            async Task<HttpResponseMessage> getUsers()
            {
                if (!isLoggedIn()) return await unauthorized();

                return await ok(users);
            }

            // helper functions

            async Task<HttpResponseMessage> ok(object body)
            {
                return await jsonResponse(HttpStatusCode.OK, body);
            }

            async Task<HttpResponseMessage> error(string message)
            {
                return await jsonResponse(HttpStatusCode.BadRequest, new { message });
            }

            async Task<HttpResponseMessage> unauthorized()
            {
                return await jsonResponse(HttpStatusCode.Unauthorized, new { message = "Unauthorized" });
            }

            async Task<HttpResponseMessage> jsonResponse(HttpStatusCode statusCode, object content)
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json")
                };

                // delay to simulate real api call
                await Task.Delay(500);

                return response;
            }

            bool isLoggedIn()
            {
                return request.Headers.Authorization?.Parameter == "fake-jwt-token";
            }
        }
    }
}
