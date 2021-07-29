using AutoMapper;
using TrainingApi.Models;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Mapping
{
    public class EmployeeModelToDtoProfile : Profile
    {
        public EmployeeModelToDtoProfile()
        {
            CreateMap<EmployeeModel, EmployeeDomainModel>();
        }
    }
}
