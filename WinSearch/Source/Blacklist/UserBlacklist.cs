using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSearch.Modules.Blacklist
{
    class UserBlacklist : Blacklist
    {
        private string _blacklistPath;

        public UserBlacklist()
        {
            Init();
            _blacklistPath = Directory.GetCurrentDirectory() + "\\blacklist.txt";
            
            // Create blacklist file if it dosent exists
            if (!File.Exists(_blacklistPath)) CreateBlacklistFile();

            SeedBlacklist(File.ReadAllLines(_blacklistPath).ToList());
        }

        private void CreateBlacklistFile()
        {
            File.Create(_blacklistPath).Close();
        }

        public void WriteBlacklist()
        {
            File.WriteAllLines(_blacklistPath, _blacklist);
        }
    }
}
