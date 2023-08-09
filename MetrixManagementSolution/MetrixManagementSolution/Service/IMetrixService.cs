using Domain;
using System.Text.RegularExpressions;

namespace TestTask.Service
{
    public interface IMetrixService
    {
        public Task SaveMetrix(string metrixJson);

        public Task SendTimeToClients(int time);
    }
}
