using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdentitySample.Models
{
    public class ClientForum
    {
        public ClientForum()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new List<UserClientForum>();
        }

        public ClientForum(string name) : this()
        {
            this.Name = name;
        }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string SomeOtherForumProperty { get; set; }
        public virtual ICollection<UserClientForum> Users { get; set; }
    }
}