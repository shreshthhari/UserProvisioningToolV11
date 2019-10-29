using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Infor.FSCM.Analytics.BirstWebService;

namespace Infor.FSCM.Analytics
{
    /// <summary>
    /// This class implements a general SOAP client for Infor BIRST and provides functions to administer spaces.
    /// </summary>
    public class BirstService : IDisposable
    {
        /// <summary>
        /// The service URL of the BIRST farm.
        /// </summary>
        public string ServiceURL { get; private set; }

        /// <summary>
        /// Holds the login token returned by BIRST which is required for subsequent service calls.
        /// </summary>
        public string LoginToken { get; private set; }

        /// <summary>
        /// The web service instance used to access BIRST.
        /// </summary>
        private CommandWebService _webService;

        /// <summary>
        /// For IDisposable implementation.
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// The static constructor makes sure the client protocol is set to TLS1.2 used by BIRST.
        /// </summary>
        static BirstService()
        {
            // check or ignore 100 http header and continue by default
            ServicePointManager.Expect100Continue = true;
            // make sure the right protocol is used, which is static for the service point manager.
            ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// This constructor creates the web service instance using the given URL:
        /// </summary>
        /// <param name="serviceURL">The service URL of the BIRST farm. </param>
        /// <remarks>Currently this is http://login.bws.birst.com/CommandWebService.asmx for the Infor Cloud.</remarks>
        public BirstService(string serviceURL)
        {
            ServiceURL = serviceURL;
            _webService = new CommandWebService();
            _webService.Url = serviceURL;
            _webService.CookieContainer = new CookieContainer();
        }

        /// <summary>
        /// Logs in to BIRST and creates a login token for subsequent calls.
        /// </summary>
        /// <param name="username">The user to connect.</param>
        /// <param name="password">The password to login.</param>
        /// <returns>True, if the login was successful; false, otherwise.</returns>
        public bool Login(string username, string password)
        {
            LoginToken = _webService.Login(username, password);
            return true;
        }

        /// <summary>
        /// Logs our from BIRST.
        /// </summary>
        public void Logout()
        {
            _webService.Logout(LoginToken);
            LoginToken = string.Empty;
        }

        /// <summary>
        /// Gets if there is currently a logged in user.
        /// </summary>
        public bool LoggedIn
        {
            get
            {
                return LoginToken.Length > 0;
            }
        }

        /// <summary>
        /// Gets the current user's profile from BIRST.
        /// </summary>
        /// <returns>The currently logged int user's profile.</returns>
        public Profile GetUserProfile()
        {
            return _webService.getUserProfile(LoginToken);
        }

        /// <summary>
        /// Gets the list of created users from BIRST.
        /// </summary>
        /// <returns>The list of users.</returns>
        public string[] GetCreatedUsers()
        {
            return _webService.listCreatedUsers(LoginToken);
        }

        /// <summary>
        /// Gets the list of managed users from BIRST.
        /// </summary>
        /// <returns>The list of users.</returns>
        public string[] GetManagedUsers()
        {
            return _webService.listManagedUsers(LoginToken);
        }

        /// <summary>
        /// Adds a user to a group in a space. The user and group both must exist in the space.
        /// </summary>
        /// <param name="spaceID">The space id of the space to add the user to a group.</param>
        /// <param name="user">The user to add, which must be a member of the space already.</param>
        /// <param name="group">The group to add the user to. The group must be member of the space.</param>
        public void AddUserToGroupInSpace(string spaceID, string user, string group)
        {
            _webService.addUserToGroupInSpace(LoginToken, user, group, spaceID);
        }

        /// <summary>
        /// Creates a new user in Birst.
        /// </summary>
        /// <param name="username">The name of the user to create.</param>
        /// <param name="password">The passowrd for the new user. If not provided it will automatically be x1xXx1Xx!.</param>
        /// <param name="email">The user's email adress.</param>
        public void CreateBirstUser(string username, string password, string email)
        {
            string options = string.Empty;

            if (!string.IsNullOrWhiteSpace(password))
            {
                options += "password=" + password;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                options += " email=" + email;
            }

            _webService.addUser(LoginToken, username, options);
        }

        /// <summary>
        /// Sets the dashboard type for a user.
        /// </summary>
        /// <param name="username">The user name to use.</param>
        /// <param name="dashboad">An option out of the following ones: "html" | "flash" | "dashboard2.0"</param>
        public void SetUserDashboardType(string username, string dashboad)
        {
            _webService.setDashboardView(LoginToken, username, dashboad);
        }

        /// <summary>
        /// Gets the list of users in space from BIRST.
        /// </summary>
        /// <param name="spaceID">The ID of the space to query.</param>
        /// <returns>The list of users.</returns>
        public string[] GetUsersInSpace(string spaceID)
        {
            return _webService.listUsersInSpace(LoginToken, spaceID);
        }

        /// <summary>
        /// Returns if a given user exists in Birst.
        /// </summary>
        /// <param name="userID">The user id to check.</param>
        /// <returns>True, if the user exists, false otherwise.</returns>
        public bool BirstUserExists(string userID)
        {
            var users = GetCreatedUsers();
            foreach (string user in users)
            {
                if (user.ToLowerInvariant().Equals(userID.ToLowerInvariant()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets if a job is complete.
        /// </summary>
        /// <param name="jobToken">The token returned for a job.</param>
        public bool IsJobComplete(string jobToken)
        {
            return _webService.isJobComplete(LoginToken, jobToken);
        }

        /// <summary>
        /// Gets the list of groups in space from BIRST.
        /// </summary>
        /// <param name="spaceID">The ID of the space to query.</param>
        /// <returns>The list of groups.</returns>
        public string[] GetGroupsInSpace(string spaceID)
        {
            return _webService.listGroupsInSpace(LoginToken, spaceID);
        }

        /// <summary>
        /// Creates a new user group in a space. If the group exists it does nothing.
        /// </summary>
        /// <param name="spaceID">The ID of the space to query.</param>
        /// <param name="groupName">The name of the group to create.</param>
        public void AddGroupToSpace(string spaceID, string groupName)
        {
            _webService.addGroupToSpace(LoginToken, groupName, spaceID);
        }

        /// <summary>
        /// Adds permissions to a group.
        /// </summary>
        /// <param name="spaceID">The ID of the space tthat contains the group to change.</param>
        /// <param name="groupName">The name of the group to change.</param>
        /// <param name="permission">The permission to add.</param>
        public void AddGroupPermission(string spaceID, string groupName, string permission)
        {
            _webService.addAclToGroupInSpace(LoginToken, groupName, permission, spaceID);
        }

        /// <summary>
        /// Creates a new user group in a space. If the group exists it does nothing.
        /// </summary>
        /// <param name="spaceID">The ID of the space to query.</param>
        /// <param name="groupName">The name of the group to create.</param>
        public bool GroupExistsInSpace(string spaceID, string groupName)
        {
            return Array.Exists(GetGroupsInSpace(spaceID), group => group == groupName);
        }

        /// <summary>
        /// Deletes all data from a space.
        /// </summary>
        /// <param name="spaceID">The ID of the space to query.</param>
        /// <returns>The id of the job executing the data deletion.</returns>
        public string DeleteAllData(string spaceID)
        {
            return _webService.deleteAllDataFromSpace(LoginToken, spaceID);
        }

        /// <summary>
        /// Adds an existing user to a space.
        /// </summary>
        /// <param name="spaceID">The ID of the space to query.</param>
        /// <param name="username">The user to add to the space.</param>
        /// <param name="isAdmin">Indicates whether the user is admin in the space.</param>
        public void AddUserToSpace(string spaceID, string username, bool isAdmin)
        {
            _webService.addUserToSpace(LoginToken, username, spaceID, isAdmin);
        }

        /// <summary>
        /// Remove an existing user from a space.
        /// </summary>
        /// <param name="spaceID">The ID of the space to query.</param>
        /// <param name="username">The user to remove from the space.</param>
        public void RemoveUserFromSpace(string spaceID, string username)
        {
            _webService.removeUserFromSpace(LoginToken, username, spaceID);
        }

        /// <summary>
        /// Sets the properties for a space.
        /// </summary>
        /// <param name="spaceID">The ID of the space to set the properties to.</param>
        /// <param name="properties">The properties to set.</param>
        public void SetSpaceProperties(string spaceID, SpaceProperties properties)
        {
            _webService.SetSpaceProperties(LoginToken, spaceID, properties);
        }

        /// <summary>
        /// Gets the properties for a space.
        /// </summary>
        /// <param name="spaceID">The space ID to qurey the properties from.</param>
        /// <returns>The properties of the specified space.</returns>
        public SpaceProperties GetSpaceProperties(string spaceID)
        {
            return _webService.GetSpaceProperties(LoginToken, spaceID);
        }

        /// <summary>
        /// Create a new space.
        /// </summary>
        /// <param name="spacename">The name of the new space.</param>
        /// <param name="comments">The comment text that appears on the homepage below the space name.</param>
        /// <param name="automatic">If true, an autonatic space and if false, an extended space is created.</param>
        public void CreateSpace(string spacename, string comments, bool automatic)
        {
            _webService.createNewSpace(LoginToken, spacename, comments, automatic);
        }

        /// <summary>
        /// Gets the users in a spcae.
        /// </summary>
        /// <param name="spaceID">The ID of the space to use.</param>
        /// <returns>The list of users assigned to the space.</returns>
        public string[] GetSpaceUsers(string spaceID)
        {
            return _webService.listUsersInSpace(LoginToken, spaceID);
        }

        /// <summary>
        /// Gets the groups in a space.
        /// </summary>
        /// <param name="spaceID">The space ID to use.</param>
        /// <returns>A list of the groups in the space.</returns>
        public string[] GetSpaceGroups(string spaceID)
        {
            return _webService.listGroupsInSpace(LoginToken, spaceID);
        }

        /// <summary>
        /// Gets the groups a user is assigned to within a space.
        /// </summary>
        /// <param name="spaceID">The id of the space to use.</param>
        /// <param name="userName">The name of the user to use.</param>
        /// <param name="internalGroups">Whether also internal or just external groups are returned.</param>
        /// <returns></returns>
        public string[] GetUserGroupAssignments(string spaceID, string userName, bool internalGroups)
        {
            return _webService.listUserGroupMembership(LoginToken, spaceID, userName, internalGroups);
        }

        /// <summary>
        /// Copies the content of a space into another.
        /// TODO make enum for options and mode.
        /// </summary>
        /// <param name="sourceSpace">The source space to copy from.</param>
        /// <param name="targetSpace">The target space to copy to.</param>
        /// <param name="mode">Mode is either:
        /// copy: Items in from_SpaceID are copied to to_SpaceID.
        /// replicate: Makes the items in targetSpace the same as in sourceSpace, copying and deleting as necessary. Replicate is more commonly used than copy.
        ///
        /// Mode is case-sensitive, lowercase "copy" and "replicate".
        /// </param>
        /// <param name="options">
        /// See https://login.bws.birst.com/Help/Full/t_admin/systemops/administrative_commands.htm#copyspace
        /// </param>
        public string CopySpace(string sourceSpace, string targetSpace, string mode, string options)
        {
            return _webService.copySpace(LoginToken, sourceSpace, targetSpace, mode, options);
        }

        /// <summary>
        /// Create a new space.
        /// </summary>
        /// <param name="spaceID">The ID of the space to delete.</param>
        public void DeleteSpace(string spaceID)
        {
            _webService.deleteSpace(LoginToken, spaceID);
        }

        /// <summary>
        /// Copies the content of a space to antoher space.
        /// </summary>
        /// <param name="sourceSpaceID">The ID of the space to copy the content from.</param>
        /// <param name="targetSpaceID">The id of the space to copy the content to.</param>
        /// <returns>Returns a Birst job ID which can be used to query if the job is complete.</returns>
        public string CopySpaceContent(string sourceSpaceID, string targetSpaceID)
        {
            return _webService.copySpaceContents(LoginToken, sourceSpaceID, targetSpaceID);
        }

        /// <summary>
        /// Swaps the content of two spaces.
        /// </summary>
        /// <param name="sourceSpaceID">The ID of the space to copy the content from.</param>
        /// <param name="targetSpaceID">The id of the space to copy the content to.</param>
        /// <returns>Returns a Birst job ID which can be used to query if the job is complete.</returns>
        public string SwapSpaceContent(string sourceSpaceID, string targetSpaceID)
        {
            return _webService.swapSpaceContents(LoginToken, sourceSpaceID, targetSpaceID);
        }

        /// <summary>
        /// Swaps the packages of two spaces, i.e. the packages from source space go into the target space and vice versa.
        /// </summary>
        /// <param name="sourceSpace">The first space to swap the package from.</param>
        /// <param name="syncImportedPackages">After import sanchronize the package data from the new data source.</param>
        /// <param name="targetSpace">The second space to swap the package with.</param>
        /// <returns>Returns a Birst job ID which can be used to query if the job is complete.</returns>
        public string SwapSpaceForPackages(string sourceSpace, string targetSpace, bool syncImportedPackages)
        {
            return _webService.swapSpaceForPackages(LoginToken, sourceSpace, targetSpace, syncImportedPackages);
        }

        /// <summary>
        /// Imports a package into a space.
        /// </summary>
        /// <param name="sourceSpaceID">The space that contains the package.</param>
        /// <param name="targetSpaceID">The space to import the package into.</param>
        /// <param name="packageID">The ID of the package to import.</param>
        /// <returns>TODO what does this return???</returns>
        public string[] ImportPackage(string sourceSpaceID, string targetSpaceID, string packageID)
        {
            return _webService.importPackage(LoginToken, sourceSpaceID, packageID, targetSpaceID);
        }

        /// <summary>
        /// Repoints a package to a new parent space.
        /// </summary>
        /// <param name="currentSourceSpaceID">TODO ???</param>
        /// <param name="newSourceSpaceID">TODO ???</param>
        /// <param name="targetSpaceID">TODO ???</param>
        public void RepointPackage(string currentSourceSpaceID, string newSourceSpaceID, string targetSpaceID)
        {
            //_webService.repointPackages(LoginToken, targetSpaceID, currentSourceSpaceID, newSourceSpaceID);
        }

        /// <summary>
        /// Gets a list of the user spaces.
        /// </summary>
        /// <returns>A list of all spaces including ID, name and owner for each of them.</returns>
        public UserSpace[] GetUserSpaces()
        {
            return _webService.listSpaces(LoginToken);
        }

        /// Indicates if a space exists.
        /// </summary>
        /// <param name="spacename">The name of the space.</param>
        /// <returns>True, if a space with the provided name exists.</returns>
        public bool SpaceExists(string spacename)
        {
            return Array.Exists(GetUserSpaces(), x => x.name == spacename);
        }

        /// <summary>
        /// Gets the space ID for a given space name.
        /// </summary>
        /// <param name="spacename">The name of the space.</param>
        /// <returns>The space ID if the space was found or an empty string, otherwise.</returns>
        public string GetSpaceID(string spacename)
        {
            UserSpace space = Array.Find(GetUserSpaces(), x => x.name == spacename);
            return space == null ? string.Empty : space.id;
        }

        #region IDisposable Support

        /// <summary>
        /// Implementation of dispose.
        /// </summary>
        /// <param name="disposing">Whether managed or unmanaged resources are disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_webService != null)
                    {
                        Logout();
                        _webService.Dispose();
                        _webService = null;
                    }
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Disposes instances of this class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}