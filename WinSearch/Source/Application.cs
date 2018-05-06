using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Modules
{
    class Application
    {
        public string _name { get; set; }
        public string _description { get; set; }
        public string _filePath { get; set; }
        private FileInfo _info;

        public Application(string FilePath)
        {
            _filePath = FilePath;
            _info = GetApplicationInformation();
            
            if (_info != null)
            {
                _name = _info.Name.Replace(".lnk", "");
            }
        }

        private FileInfo GetApplicationInformation()
        {
            try
            {
                return new FileInfo(_filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't get Information from filelink.");
                return null;
            }
        }

        public void StartApplication()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = _filePath;
            proc.Start();
        }
    }
}
