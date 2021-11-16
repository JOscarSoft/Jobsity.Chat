using Microsoft.Extensions.Configuration;

namespace Jobsity.Chat.Core.Helpers
{
    public static class Helper
    {
        public static string GetConnection(IConfiguration configuration, string connectionString)
        {
            return configuration[connectionString] ?? configuration.GetConnectionString(connectionString);
        }
    }
}