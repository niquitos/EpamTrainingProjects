using TrainingApi.Services.DomainModels;

namespace TrainingApi.Services.Messages
{
    public interface IEmployeeUpdateSender
    {
        void SendEmployee(EmployeeDomainModel employee);
    }
}