using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using TestApplication.Models;
using static TestApplication.Helpers.Enumerations;

namespace TestApplication
{
    public partial class Startup
    {
        #region Members

        private RoleManager<IdentityRole> _roleManager;
        private ApplicationUserManager _userManager;

        #endregion

        #region Properties

        public RoleManager<IdentityRole> RoleManager
        {
            get
            { return _roleManager ?? new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())); }
            private set { _roleManager = value; }
        }

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    { return _userManager ?? new HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        #endregion


        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString("/Account/LoginPage"),
                LoginPath = new PathString("/Member/AddGroup"),
                ExpireTimeSpan = TimeSpan.FromMinutes(30)
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        public async void ConfigureRoles()
        {
            if (!await RoleManager.RoleExistsAsync(UserRoles.Admin))
                await RoleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await RoleManager.RoleExistsAsync(UserRoles.User))
                await RoleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }

        public async void ConfigureUsers()
        {

            //var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            //ApplicationUserManager _userManager = new ApplicationUserManager(store);
            //var manger = _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = new ApplicationUser() { Email = aspNetUsers.Email, UserName = aspNetUsers.UserName };
            //var usmanger = manger.Create(user, aspNetUsers.PasswordHash);
        }

    }
}