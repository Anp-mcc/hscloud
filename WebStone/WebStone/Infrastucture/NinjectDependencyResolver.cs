using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using Ninject;
using WebStone.Controllers;

namespace WebStone.Infrastucture
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly StandardKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBinding();
        }

        private void AddBinding()
        {
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}