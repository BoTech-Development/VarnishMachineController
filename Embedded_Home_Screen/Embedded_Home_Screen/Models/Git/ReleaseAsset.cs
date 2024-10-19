using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embedded_Home_Screen.Models.Git
{
    /// <summary>
    /// Model for one file.
    /// </summary>
    public class ReleaseAsset
    {
        public string browser_download_url {  get; set; }
        public int size { get; set; } // in Bytes
    }
}
