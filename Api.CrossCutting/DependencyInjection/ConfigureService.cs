using Api.Domain.Interfaces.Services.Users;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            // singleton - Significa que apenas uma única instância será criada. Essa instância é compartilhada entre todos os componentes
            // que exigem isso. A mesma instância é, portanto, usada sempre.

            // scoped - Uma instância é criada uma vez por escopo. Um escopo é criado em cada solicitação para o aplicativo (cada pedido é um escopo),
            // portanto, todos os componentes registrados como scoped serão criados uma vez por solicitação.

            // transient - Os componentes são criados toda vez que são solicitados e nunca são compartilhados.
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}
