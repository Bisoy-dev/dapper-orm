using System.Data.SqlClient;

namespace PersonDetailWithQrCodeApp.Extensions;

public static class SqlServerExtension
{
    public static IServiceCollection AddSqlServerConnection(this IServiceCollection services)
    {
        const string CONNECTION_NAME = "DefaultConnection";
        services.AddScoped<SqlConnection>(serviceProvider =>
        {
            var confirguration = serviceProvider.GetService<IConfiguration>();
            return new SqlConnection(confirguration.GetConnectionString(CONNECTION_NAME));
        });

        return services;
    }
}