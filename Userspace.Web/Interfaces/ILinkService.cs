using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Models;

namespace Userspace.Web.Interfaces
{
    public interface ILinkService
    {
         Task<IEnumerable<LinkViewModel>> GetLinks(string userId);
         Task<LinkViewModel> GetLinkById(int id);
         Task<LinkViewModel> CreateLink(LinkViewModel link);
    }
}
