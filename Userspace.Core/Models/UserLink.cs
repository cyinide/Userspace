using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Userspace.Core.Models.Auth;

namespace Userspace.Core.Models
{
    public class UserLink
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        public int LinkId { get; set; }
        public int TagId { get; set; }
        public virtual User User { get; set; }
        public virtual Link Link { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
