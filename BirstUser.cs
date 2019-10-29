using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infor.FSCM.Analytics
{
    /// <summary>
    /// Represents a Birst user.
    /// </summary>
    public class BirstUser
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// The user's unique ID.
        /// </summary>
        public string UserID { get; private set; }

        /// <summary>
        /// The user's name.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Whether the user is a space admin.
        /// </summary>
        public bool SpaceAdmin { get; private set; }

        /// <summary>
        /// The tenant a user belongs to.
        /// </summary>
        public string Tenant { get; private set; }

        /// <summary>
        /// Creates a new instance of class BirstUser.
        /// </summary>
        /// <param name="userID">The unique ID of the user.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="userName">The user name.</param>
        public BirstUser(string userID, string email, string userName)
        {
            Email = email;
            UserName = userName;
            UserID = userID;
            Tenant = string.Empty;
        }

        /// <summary>
        /// Creates a new instance of class BirstUser.
        /// </summary>
        /// <param name="userID">The unique ID of the user.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="tenant">The tenant the user belongs to.</param>
        public BirstUser(string userID, string email, string userName, string tenant)
        {
            Email = email;
            UserName = userName;
            UserID = userID;
            Tenant = tenant;
        }

        /// <summary>
        /// Gets the user ID representing the user in Birst. In case a tenant is provided a MT user ID is created using the schema tenant_userid.
        /// </summary>
        public string BirstUserID
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Tenant))
                {
                    return Tenant.ToLowerInvariant() + "_" + UserID.ToLowerInvariant();
                }
                else
                {
                    return UserName;
                }
            }
        }
    }
}