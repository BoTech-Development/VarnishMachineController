using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embedded_Home_Screen.Models.Git
{
    public class Release
    {
        public string assets_url {  get; set; }
        public int id { get; set; }
        public string name { get; set; } // Version Number
        public bool prerelease { get; set; }
        public DateTime published_at { get; set; }
        public string body { get; set; }
    }
}
