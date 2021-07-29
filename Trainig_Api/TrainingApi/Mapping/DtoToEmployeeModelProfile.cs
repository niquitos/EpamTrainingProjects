using AutoMapper;
using TrainingApi.Models;
using TrainingApi.Services.DomainModels;

namespace TrainingApi.Mapping
{
    public class DtoToEmployeeModelProfile : Profile
    {
        public DtoToEmployeeModelProfile()
        {
            CreateMap<EmployeeDomainModel, EmployeeModel>();
        }
    }
}
