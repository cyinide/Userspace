using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Models;

namespace Userspace.Web.Interfaces
{
    public interface ILinkService
    {
         Task<IEnumerable<Link>> GetLinks();
         Task<Link> GetLinkById(int id);
         Task<Link> CreateLink(Link link);
    }
}
