using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace PB.WebService
{
    /// <summary>
    /// Set global prefix to all routes
    /// </summary>
    public class VersionRouteConvention : IApplicationModelConvention
    {
        private readonly string _version;
        public VersionRouteConvention(string version)
        {
            _version = version;
        }
        public void Apply(ApplicationModel application)
        {
            var prefix = new AttributeRouteModel(new RouteAttribute($"api/{_version}"));
            foreach (var controller in application.Controllers)
            {
                var routeSelector = controller.Selectors.FirstOrDefault(x => x.AttributeRouteModel != null);

                if (routeSelector != null)
                {
                    routeSelector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(prefix,
                        routeSelector.AttributeRouteModel);
                }
                else
                {
                    controller.Selectors.Add(new SelectorModel { AttributeRouteModel = prefix });
                }
            }
        }
    }
}
