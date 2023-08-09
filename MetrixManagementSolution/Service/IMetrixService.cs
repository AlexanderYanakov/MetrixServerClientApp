using Domain;
using System.Text.RegularExpressions;

namespace TestTask.Service
{
    public interface IMetrixService
    {
        public Task SaveMetrix(Metrix metrix, List<DiskSpace> diskSpaces);

        public Task SendTimeToClients(int time);
    }
}
