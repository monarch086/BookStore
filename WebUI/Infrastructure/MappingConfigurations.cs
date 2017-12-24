using AutoMapper;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System.Linq;
using WebUI.Models;

namespace WebUI.Infrastructure
{
    public static class MappingConfigurations
    {
        public static void Configure()
        {
            IRepository repository = new EFRepository();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductSmallViewModel>()
                    .ForMember(ps => ps.Product,
                        m => m.MapFrom(p => p))
                    .ForMember(ps => ps.ImagePath,
                        m => m.MapFrom(p => repository.Images.FirstOrDefault(i => i.ProductID == p.ProductId).Path));
            });
        }
    }
}