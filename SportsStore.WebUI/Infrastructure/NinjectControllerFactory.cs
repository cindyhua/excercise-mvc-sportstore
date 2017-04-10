using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.Domain.Concrete;
using System.Configuration;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBinds();
            
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBinds()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product {Name="Kayak",Category="Watersports", Price=275M},
            //    new Product {Name="Lifejacket",Category="Watersports",Price=48.95M},
            //    new Product {Name="Soccer ball",Category="Soccer",Price=19.50M},
            //    new Product {Name="Corner flag",Category="Soccer",Price=34.95M}

            //}.AsQueryable());
            //ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"]=="true"?"true":"false")
            };
            ninjectKernel.Bind<IOrderProcessor>()
                .To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }
    }
}