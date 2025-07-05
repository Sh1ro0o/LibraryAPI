namespace LibraryAPI.Interface.Service
{
    public interface ICookieService
    {
        public void CreateSessionCookie(string sessionToken, HttpRequest request, HttpResponse response);
        public void CreateRefreshCookie(string refreshToken, HttpRequest request, HttpResponse response);
    }
}
