using Domain;
using MetrixManagementSolution.Builders;
using Microsoft.AspNetCore.SignalR;
using Repository.Repositories.Interfaces;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace TestTask.Service
{
    public class MetrixService : IMetrixService
    {
        private readonly static string TimePath = Directory.GetCurrentDirectory() + "\\time.txt";

        private readonly IMetricRepository _metricRepository;
        private readonly IDiskSpaceRepository _diskSpaceRepository;
        private readonly IHubContext<SignalRMetrix> _hubContext;

        // Method to save metrics in database
        
        public MetrixService(IMetricRepository metricRepository, IDiskSpaceRepository diskSpaceRepository, IHubContext<SignalRMetrix> hubContext)
        {
            _metricRepository = metricRepository;
            _diskSpaceRepository = diskSpaceRepository;
            _hubContext = hubContext;
        }

        public async Task SaveMetrix(string metrixJson)
        {
            var fullMetrixInfo = JsonSerializer.Deserialize<FullMetrixInfo>(metrixJson);
            if (fullMetrixInfo == null)
                return;

            var separateMetrixInfo = MetrixBuilder.build(fullMetrixInfo);
            Metrix metrix = separateMetrixInfo.Keys.FirstOrDefault();
            List<DiskSpace> diskSpaces = separateMetrixInfo.Values.FirstOrDefault();

            var checkMetrix = _metricRepository
                .GetAll()
                .Where(x => x.ip_address == metrix.ip_address)
                .FirstOrDefault();

            var checkDiskSpaces = _diskSpaceRepository
                .GetAll()
                .Where(x => x.ip_address == metrix.ip_address)
                .ToList();

            if (checkMetrix == null)
            {
                await _metricRepository.CreateAsync(metrix);
            }
            else if (checkMetrix != null)
            {
                _metricRepository.Update(metrix, checkMetrix.id);
            }

            if (checkDiskSpaces.Count == 0)
            {
                foreach (var disk in diskSpaces)
                {
                    await _diskSpaceRepository.CreateAsync(disk);
                }
            }
            else if (checkDiskSpaces.Count != 0)
            {
                foreach (var checkDisk in checkDiskSpaces)
                {
                    foreach (var disk in diskSpaces)
                    {
                        if(checkDisk.name == disk.name)
                        {
                            _diskSpaceRepository.Update(disk, checkDisk.id);
                        }
                    }
                }
            }
        }
        // Send time for getting metrics to clients method
        public async Task SendTimeToClients(int time)
        {
            createFile();
            StreamReader reader = new StreamReader(TimePath);
            string content = reader.ReadToEnd();
            reader.Close();

            content = Regex.Replace(content, content, time.ToString());

            StreamWriter writer = new StreamWriter(TimePath);
            writer.Write(content);
            writer.Close();

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", time);
        }

        // Method to get time from file
        public static void createFile()
        {
            if (!File.Exists(TimePath))
            {
                File.Create(TimePath).Dispose();

                using (TextWriter tw = new StreamWriter(TimePath))
                {
                    tw.WriteLine("3");
                }
            }
        }
    }
}
