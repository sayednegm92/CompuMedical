using Microsoft.AspNetCore.Http;
using System;

namespace CompuMedical.MVC.Helpers;

public static class CookieHelper
{
    public static void SetCookie(this HttpResponse response, string key, string value, int? expireMinutes = null)
    {
        CookieOptions option = new CookieOptions();

        if (expireMinutes.HasValue)
            option.Expires = DateTime.Now.AddMinutes(expireMinutes.Value);
        else
            option.Expires = DateTime.Now.AddHours(1); // default 1 hour

        option.HttpOnly = true; // protect from JS access (XSS)
        option.Secure = true;   // only HTTPS

        response.Cookies.Append(key, value, option);
    }

    public static string? GetCookie(this HttpRequest request, string key)
    {
        request.Cookies.TryGetValue(key, out string? value);
        return value;
    }

    public static void RemoveCookie(this HttpResponse response, string key)
    {
        response.Cookies.Delete(key);
    }
}
