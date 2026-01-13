using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace AppWeb.Application


{
    public interface IUserContext  // interfejs do pobierania informacji o aktualnym użytkowniku
    {
        CurrentUser GerCurrentUser();
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        public CurrentUser GerCurrentUser()
        {

            var user = _httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("Contex user is not present");
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var name = user.FindFirst(c => c.Type == ClaimTypes.Name)!.Value;
            var phone = user.FindFirst(c => c.Type == ClaimTypes.MobilePhone)?.Value ?? string.Empty;
            return new CurrentUser(id, name, email, phone);


        }

    }
}
