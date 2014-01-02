using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sordid.Core.Exceptions;
using Sordid.Core.Interfaces;
using Sordid.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sordid.Core.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;

        private IPrincipal User { get { return Thread.CurrentPrincipal;  } }

        public UserService(IUnitOfWork unitOfWork)
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(unitOfWork.Context));
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
                throw new SordidSecurityException("Could not load current user");
            return user;
        }
    }
}
