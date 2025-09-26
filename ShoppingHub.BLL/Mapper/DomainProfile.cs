using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingHub.BLL.ModelVm.order;
using ShoppingHub.DAL.Entities;

namespace ShoppingHub.BLL.Mapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile() {
            CreateMap<Order,getAllOrdersVM>().ReverseMap();
        }
    }
}
