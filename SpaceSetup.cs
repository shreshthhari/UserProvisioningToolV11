using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infor.FSCM.Analytics
{
    public class SpaceSetup
    {
        public string[] Users { get; set; }
        public string[] Groups { get; set; }

        private Dictionary<string, List<string>> Assignments { get; set; }

        public bool GroupExists(string group)
        {
            return Groups.Contains(group);
        }

        public string[] AssignedGroups
        {
            get
            {
                return Assignments.Keys.ToArray();
            }
        }

        public void SetAssignments(Dictionary<string, List<string>> assignments)
        {
            Assignments = assignments;
        }

        public List<string> AssignedUsersInGroups(string group)
        {
            foreach (string g in Assignments.Keys)
            {
                if (g.ToLowerInvariant().Equals(group.ToLowerInvariant()))
                {
                    return Assignments[g];
                }
            }
            return new List<string>();
        }

        public bool GroupIsAssigned(string group)
        {
            foreach (string g in Assignments.Keys)
            {
                if (g.ToLowerInvariant().Equals(group.ToLowerInvariant()))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AssignmentExists(string user, string group)
        {
            foreach (string g in Assignments.Keys)
            {
                if (g.ToLowerInvariant().Equals(group.ToLowerInvariant()))
                {
                    foreach (string u in Assignments[g])
                    {
                        if (u.ToLowerInvariant().Equals(user.ToLowerInvariant()))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool UserExists(string user)
        {
            foreach (string u in Users)
            {
                if (u.ToLowerInvariant().Equals(user.ToLowerInvariant()))
                {
                    return true;
                }
            }
            return false;
        }

        public bool UserInGroup(string user, string group)
        {
            if (!Assignments.ContainsKey(group))
            {
                return false;
            }

            List<string> users = Assignments[group];

            foreach (string u in Users)
            {
                if (u.ToLowerInvariant().Equals(user.ToLowerInvariant()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}