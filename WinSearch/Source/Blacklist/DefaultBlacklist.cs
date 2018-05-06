using System.Collections.Generic;
using System.Linq;

namespace SmartSearch.Modules.Blacklist
{
    class DefaultBlacklist : Blacklist
    {
        private List<string> applications = new List<string>
        {
           ".url"
        };

        private UserBlacklist _userBlacklist;

        public DefaultBlacklist()
        {
            Init();
            AddDefaultToBlacklist();
        }

        public UserBlacklist GetUserBlacklist()
        {
            return _userBlacklist;
        }

        public void AddUserBlacklist()
        {
            _userBlacklist = new UserBlacklist();
        }

        public void AddDefaultToBlacklist()
        {
            _blacklist.AddRange(applications);
        }

        public void AddToDefault(string name)
        {
            applications.Add(name);
        }

        public override List<string> FullBlacklist
        {
            get
            {
                if (_userBlacklist._blacklist.Count > 0)
                    return _blacklist.Concat(_userBlacklist._blacklist).ToList();
                else
                    return _blacklist;
            }
       }
    }
}
