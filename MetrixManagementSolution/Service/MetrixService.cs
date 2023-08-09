using Domain;
using Microsoft.AspNetCore.SignalR;
using Repository.Repositories.Interfaces;
using System.Text.RegularExpressions;
using TestTask;

namespace TestTask.Service
{
    public class MetrixService : IMetrixService
    {
        private readonly static string TimePath = Directory.GetCurrentDirectory() + "\\time.txt";

        private readonly IMetricRepository _metricRepository;
        private readonly IDiskSpaceRepository _diskSpaceRepository;
        private readonly IHubContext<SignalRMetrix> _hubContext;

        public Task SaveMetrix(Metrix metrix, List<DiskSpace> diskSpaces)
        {
            throw new NotImplementedException();
        }

        public async Task SendTimeToClients(int time)
        {
            StreamReader reader = new StreamReader(TimePath);
            string content = reader.ReadToEnd();
            reader.Close();

            content = Regex.Replace(content, content, time.ToString());

            StreamWriter writer = new StreamWriter(TimePath);
            writer.Write(content);
            writer.Close();

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", getTime());
        }

        public static int getTime()
        {
            if (!File.Exists(TimePath))
            {
                File.Create(TimePath).Dispose();

                using (TextWriter tw = new StreamWriter(TimePath))
                {
                    tw.WriteLine("3");
                }
            }
            return Convert.ToInt32(File.ReadAllText(TimePath));
        }
    }
}
