using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Ads.Repository;
using Ads.Services;
using Ninject.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Ads.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Ads.App_Start.NinjectWebCommon), "Stop")]

namespace Ads.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using Ads.Models;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Bind(o => o.FromAssemblyContaining<ArticleRepository>()
                    .SelectAllClasses()
                    .WhichAreNotGeneric()
                    .InheritedFrom(typeof(IRepository<>))
                    .BindAllInterfaces()
                );
                //para añadir el contexto
                kernel.Bind<DbAdsContext>().ToSelf().InRequestScope(); //genera una sola instancia

                kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
                kernel.Bind<IUserStore<ApplicationUser>>()
                    .To<UserStore<ApplicationUser>>()
                    .InRequestScope()
                    .WithConstructorArgument("context", kernel.Get<ApplicationDbContext>());
                kernel.Bind<UserManager<ApplicationUser>>()
                    .ToSelf().InRequestScope();
                kernel.Bind<IAuthenticationManager>()
                    .ToMethod(
                        m => HttpContext.Current.GetOwinContext().Authentication
                    ).InRequestScope();

                kernel.Bind<IRoleStore<IdentityRole, string>>()
                    .To<RoleStore<IdentityRole>>()
                    .InRequestScope()
                    .WithConstructorArgument("context", kernel.Get<ApplicationDbContext>());
                kernel.Bind<RoleManager<IdentityRole>>().ToSelf().InRequestScope();

                //se le indica mvc que use ese resolver para crear los controllers
                DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
