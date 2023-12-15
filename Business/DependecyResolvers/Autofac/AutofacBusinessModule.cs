using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependecyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GeneralContentService>().As<IGeneralContentService>().SingleInstance();
            builder.RegisterType<EfGeneralContentDal>().As<IGeneralContentDal>().SingleInstance();

            builder.RegisterType<ProductContentService>().As<IProductContentService>().SingleInstance();
            builder.RegisterType<EfProductContentDal>().As<IProductContentDal>().SingleInstance();

            builder.RegisterType<OrderService>().As<IOrderService>().SingleInstance();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();

            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
