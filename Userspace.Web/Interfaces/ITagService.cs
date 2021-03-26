using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Userspace.Web.Models;

namespace Userspace.Web.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetTags();
        Task<Tag> GetTagById(int id);
        Task<Tag> CreateTag(Tag link);
    }
}
