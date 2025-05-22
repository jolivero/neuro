using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Neuro.AI.Graph.Connectors
{
    public class ManufacturingConnector
    {
        private readonly string cnnString;

        public ManufacturingConnector(IConfiguration config)
        {
            cnnString = config.GetConnectionString("Cnn_Manufacturing")!;
        }

        public IDbConnection Connect() => new SqlConnection(cnnString);
    }
}
