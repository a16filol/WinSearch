using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartSearch.Modules.Blacklist
{
    class Blacklist
    {
        public List<string> _blacklist { get; set; }

        public void Add(string name)
        {
            _blacklist.Add(name);
        }

        public void DistinctBlacklist()
        {
            _blacklist = _blacklist.Distinct().ToList<string>();
        }

        public virtual void Init()
        {
            _blacklist = new List<string>();
        }

        public virtual void SeedBlacklist(List<string> blacklistSeed)
        {
            _blacklist.AddRange(blacklistSeed);
        }

        public virtual void ResetBlacklist(string path)
        {
            File.Create(path).Close();
        }

        public virtual void Remove(string name)
        {
            _blacklist.Remove(name);
        }

        public virtual List<string> FullBlacklist => _blacklist;
    }
}
