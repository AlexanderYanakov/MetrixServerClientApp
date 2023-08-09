using Domain;

namespace MetrixManagementSolution.Builders
{
    public class MetrixBuilder
    {
        public static Dictionary<Metrix, List<DiskSpace>> build(FullMetrixInfo fullMetrixInfo)
        {
            Metrix metrix = new Metrix(
                fullMetrixInfo.IPAddress, 
                fullMetrixInfo.CPUUsage, 
                fullMetrixInfo.RAM.Total, 
                fullMetrixInfo.RAM.Free);

            List<DiskSpace> diskSpaces = fullMetrixInfo.DiskSpaces;

            var responce = new Dictionary<Metrix, List<DiskSpace>>()
            {
                { metrix, diskSpaces }
            };
            return responce;
        }
    }
}
