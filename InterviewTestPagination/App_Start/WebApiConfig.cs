using System.Web.Http;
using InterviewTestPagination.Models;
using InterviewTestPagination.Models.Todo;
using InterviewTestPagination.Utils.Helpers;
using Newtonsoft.Json.Serialization;
using Unity;
using Unity.Lifetime;

namespace InterviewTestPagination {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            // DI
            var container = new UnityContainer();
            container.RegisterType<IModelRepository<Todo>, TodoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IModelService<Todo>, TodoService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
