
using Ss.Data.Enums;
using Ss.Data.Models;
using Ss.Data.Repository;
using Ss.Data.Repository.Interfaces;
using Ss.Service;
using Ss.Service.Interfaces;
using System;
using System.Collections.Generic;
using Unity;

namespace ServerSite.Dependency
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer Container
        {
            get { return container.Value; }
        }

        #endregion Unity Container

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types
        /// such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type
        /// even if it was not previously registered.</remarks>
        private static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            #region  Register Conntect database

            var database = new DatabaseContext();

            var accessPermission = new AccessPermission()
            {
                Actflg = Actflg.Active,
                AccessPermissionDescription = "user-getall"
            };

            var role = new Role()
            {
                RoleName = "Root",
                Actflg = Actflg.Active,
                RoleDescription = "Root"
            };

            var roleAccessPermission = new RoleAccessPermission()
            {
                AccessPermission = accessPermission,
                Role = role,
                ScopeView = ScopeView.All
            };

            var userDefault = new User()
            {
                UserName = "admin",
                Password = "123456789",
                FullName = "admin",
                Permission = Permission.Admin,
                Actflg = Actflg.Active,
                Roles = new List<Role>() { role }
            };

            bool checkUserDefault = false;
            try
            {
                checkUserDefault = database.Users.Find(1) != null;
            }
            catch (Exception ex) { }

            if (!checkUserDefault)
            {
                database.Users.Add(userDefault);
                database.SaveChanges();
            }

            container.RegisterInstance(database);


            #endregion

            #region Register database context

            container.RegisterType<IRepositoryContext, RepositoryContext>();

            #endregion


            #region Register Services

            container.RegisterType<IUserService, UserService>();

            #endregion

        }
    }
}
