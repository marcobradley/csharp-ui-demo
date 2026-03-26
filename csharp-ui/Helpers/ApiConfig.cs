namespace csharp_ui.Helpers
{
    public static class ApiConfig
    {
        public static string ApiBaseUrl
        {
            get
            {
                var url = Environment.GetEnvironmentVariable("API_BASE_URL");
                return string.IsNullOrEmpty(url) ? "http://localhost:8080" : url;
            }
        }
    }
}