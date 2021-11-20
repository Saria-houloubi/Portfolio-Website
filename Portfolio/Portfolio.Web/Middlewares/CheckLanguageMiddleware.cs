using Portfolio.Core.Abstractions;

namespace Portfolio.Web.Middlewares
{
    /// <summary>
    /// Middlware to setup the language for <see cref="DefaultCurrentUser"/>
    /// </summary>
    public class CheckLanguageMiddleware
    {
        #region Properties
        private static string _langCookieKey = "u.lang";
        public RequestDelegate _next { get; private set; }
        public IDateTimeProvider _datetimeProvider { get; private set; }
        #endregion

        #region Constructer
        public CheckLanguageMiddleware(RequestDelegate next, IDateTimeProvider timeProvider)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _datetimeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        }
        #endregion

        public Task InvokeAsync(HttpContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            //Get the scoped instance
            var curerntUser = context.RequestServices.CreateScope().ServiceProvider.GetService<ICurrentUserState>();

            if (curerntUser is null)
                throw new ArgumentNullException(nameof(curerntUser));

            //Try to get the language from cookie
            if (context.Request.Cookies.TryGetValue(_langCookieKey, out var _lang))
            {
                curerntUser.Language = _lang ?? "en";
            }
            else
            {
                //Try to get what is prefered language from browser
                var accLang = context.Request.Headers.AcceptLanguage.FirstOrDefault()?.Substring(0, 2);

                if (!string.IsNullOrEmpty(accLang))
                    curerntUser.Language = accLang;

                //Set what ever is the default language
                context.Response.Cookies.Append(_langCookieKey, curerntUser.Language, new CookieOptions
                {
                    Expires = _datetimeProvider.NowOffSet.AddDays(30),
                });
            }

            return _next.Invoke(context);
        }
    }

    public static class CheckLanguageMiddlewareWebApplicationExtensions
    {
        public static void UseCheckLanguage(this WebApplication app)
        {
            app.UseMiddleware<CheckLanguageMiddleware>();
        }
    }
}
