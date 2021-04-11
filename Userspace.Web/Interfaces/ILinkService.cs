using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Models;
using Userspace.Web.Resources;

namespace Userspace.Web.Interfaces
{
    public interface ILinkService
    {
         Task<IEnumerable<LinkViewModel>> GetLinks(string userId);
         Task<LinkViewModel> GetLinkById(int id);
         Task<bool> CreateLink(LinkResource link);
         Task<LinkResource> CheckLinkForOccurance(string name);
    }
}
