using ServerSite.Dependency;
using Ss.Core.RedisCache;
using Ss.Data.Enums;
using Ss.Data.Models;
using Ss.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ServerSite.ActionFilters
{
    public class UserRole
    {
        public int Role_Id { get; set; }
        public string RoleName { get; set; }
        public List<RolePermission> Permissions = new List<RolePermission>();
    }

    public class RolePermission
    {
        public int Permission_Id { get; set; }
        public string PermissionDescription { get; set; }
    }


    public class RbacUser
    {
        public int User_Id { get; set; }
        public bool IsSysAdmin { get; set; }
        public string Username { get; set; }
        private List<UserRole> Roles = new List<UserRole>();

        public RbacUser(string _username)
        {
            this.Username = _username;
            this.IsSysAdmin = false;
            GetDatabaseUserRolesPermissions();
        }

        private void GetDatabaseUserRolesPermissions()
        {
            IRepositoryContext context = UnityConfig.Container.Resolve<IRepositoryContext>();
            ICacheProvider cacheRedisProvider = UnityConfig.Container.Resolve<ICacheProvider>();

            bool checkMember = cacheRedisProvider.IsInCache(Username);
            if (true)
            {
                User _user = context.UserRepository.Get().Where(u => u.UserName == this.Username).FirstOrDefault();
                if (_user != null && _user.Actflg == Actflg.Active)
                {
                    Role isRoleRoot = _user.Roles.FirstOrDefault(r => r.RoleName == "Root");
                    if (isRoleRoot != null)
                    {
                        // Add full permistion for Root Admin
                        UserRole _userRole = new UserRole { Role_Id = isRoleRoot.Id, RoleName = isRoleRoot.RoleName };
                        foreach (AccessPermission role_permission in context.AccessPermissionRepository.Get())
                        {
                            _userRole.Permissions.Add(new RolePermission { Permission_Id = role_permission.Id, PermissionDescription = role_permission.AccessPermissionDescription });
                        }
                        this.Roles.Add(_userRole);
                    }
                    else
                    {
                        User_Id = _user.Id;
                        foreach (Role _role in _user.Roles)
                        {
                            UserRole _userRole = new UserRole { Role_Id = _role.Id, RoleName = _role.RoleName };

                            var roleAccessPermissions = context.RoleAccessPermissionRepository.Get().Where(role => role.Role.Id == _role.Id);

                            foreach (RoleAccessPermission rolePermission in roleAccessPermissions)
                            {
                                _userRole.Permissions.Add(new RolePermission { Permission_Id = rolePermission.AccessPermission.Id, PermissionDescription = rolePermission.AccessPermission.AccessPermissionDescription });
                            }

                            this.Roles.Add(_userRole);

                            if (!this.IsSysAdmin)
                            {
                                this.IsSysAdmin = _role.IsSysAdmin;
                            }

                        }
                    }
                }
                cacheRedisProvider.Set(Username, this);
                return;
            }

            var cache = cacheRedisProvider.Get<RbacUser>(Username);
            this.User_Id = cache.User_Id;
            this.IsSysAdmin = cache.IsSysAdmin;
            this.Roles = cache.Roles;
        }

        public bool HasPermission(string requiredPermission)
        {
            bool bFound = false;
            foreach (UserRole role in this.Roles)
            {
                bFound = (role.Permissions.Where(p => p.PermissionDescription.ToLower() == requiredPermission.ToLower()).ToList().Count > 0);
                if (bFound)
                    break;
            }
            return bFound;
        }

    }
}
