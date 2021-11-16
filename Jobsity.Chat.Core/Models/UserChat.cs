using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Jobsity.Chat.Core.Models
{
    public class UserChat : IdentityUser
    {
        public virtual ICollection<Message> MessagesList { get; set; } = new HashSet<Message>();
    }
}
