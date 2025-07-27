//using Autofac;
//using AutoMapper;
//using Module = System.Reflection.Module;

//namespace NeuroEase.Core.Helpers
//{
//    public class MapperConfigModule : Module
//    {
//        protected override void Load(ContainerBuilder builder)
//        {
//            builder.Register(context =>
//            {
//                var config = new MapperConfiguration(cfg =>
//                {
//                    cfg.AddMaps(typeof(MappingProfile).Assembly); // ثبت همه پروفایل‌ها
//                });

//                return config.CreateMapper();
//            })
//            .As<IMapper>()
//            .InstancePerLifetimeScope();
//        }
//    }
//}
