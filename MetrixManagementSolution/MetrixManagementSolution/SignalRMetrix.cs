using Domain;
using MetrixManagementSolution.Builders;
using MetrixTask.Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestTask;
using TestTask.Service;

namespace TestTask
{
    public class SignalRMetrix : Hub
    {
        private readonly IMetrixService MetrixService;


        public SignalRMetrix(IMetrixService metrixService) 
        {
            MetrixService = metrixService;
        }

        public async Task SaveMetrix(string metrixJson)
        {
            await MetrixService.SaveMetrix(metrixJson);
        }
    }
}

