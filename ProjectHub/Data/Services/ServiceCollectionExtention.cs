using Microsoft.Extensions.DependencyInjection;

namespace ProjectHub.Data.Services
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBlazorModal(this IServiceCollection services)
		{
			return services.AddScoped<ModalService>();
		}
	}
}