using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using Moq;
using Ninject;
using Domain.Concrete;
using WebUI.Infrastructure.Abstract;
using WebUI.Infrastructure.Concrete;

namespace GameStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IShippingDetailRepository>().To<EFShippingDetailRepository>();
            kernel.Bind<IPartRepository>().To<EFPartRepository>();
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}