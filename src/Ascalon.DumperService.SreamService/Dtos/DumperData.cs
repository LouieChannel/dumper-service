
using System.Collections.Generic;

namespace Ascalon.DumperService.SreamService.Dtos
{
    public class DumperData
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }

        public string Label { get; set; }

        public List<List<float>> Array { get; set; } = new List<List<float>>();
    }
}
