using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infor.FSCM.Analytics
{
    /// <summary>
    /// Implements a container for assignments of users to groups.
    /// </summary>
    public class BirstUserGroupAssignment
    {
        /// <summary>
        /// The user id of the user to assign.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// The group to assign the user to.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Initializes a new instance of class BirstUserGroupAssignment.
        /// </summary>
        /// <param name="user">The user to assign.</param>
        /// <param name="group">The group to assign the user to.</param>
        public BirstUserGroupAssignment(string user, string group)
        {
            User = user;
            Group = group;
        }

        /// <summary>
        /// Initializes a new instance of class BirstUserGroupAssignment.
        /// </summary>
        /// <param name="parseText">A CSV text to parse, which needs to contain user,group in each line.</param>
        public BirstUserGroupAssignment(string parseText)
        {
            string[] parts = parseText.Split(',');
            if (parts.Length != 2)
            {
                throw new Exception("The text " + parseText + " does not have the right format as a user to be added to the spaces.");
            }
            Group = parts[0].Trim();
            User = parts[1].Trim();
        }
    }

    /// <summary>
    /// This class implements a list of user to group assignments.
    /// </summary>
    public class BirstUserGroupAssignments : List<BirstUserGroupAssignment>
    {
    }
}