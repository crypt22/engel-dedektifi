using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using EEE.Controllers;
using EEE.Domain.Repositories;
using EEE.Domain.Services;

namespace EEE.App_Start
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;
        public WindsorControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("Controller for '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }

            return (IController)_kernel.Resolve(controllerType);
        }
    }

    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Types.FromThisAssembly().Pick()
                                .If(Component.IsInSameNamespaceAs<BaseController>())
                                .If(t => t.Name.EndsWith("Controller"))
                                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                                .LifestylePerWebRequest());
        }
    }

    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IFormsAuthenticationService>().ImplementedBy<FormsAuthenticationService>().LifestylePerWebRequest());
            container.Register(Component.For(typeof(IEntityRepository<>)).ImplementedBy(typeof(EntityRepository<>)).LifestylePerWebRequest());
        }
    }
}