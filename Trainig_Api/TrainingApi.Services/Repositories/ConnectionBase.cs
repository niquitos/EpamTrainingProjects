namespace TrainingApi.Services.Repositories
{
    public class ConnectionBase
    {
        public string DataConnection { get; }

        public ConnectionBase(string dataConnection)
        {
            DataConnection = dataConnection;
        }
    }
}
