using Api.Domain.Interfaces.Services.Users;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            // transient - para cada operação que tiver injeção de dependencia ele cria uma instancia de User
            // scoped - significa que se ele entrar numa operação ele pode usar 10x a mesma instancia, no caso por ciclo de vida
            // singleton - serviço vai ser executado uma vez só
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}
