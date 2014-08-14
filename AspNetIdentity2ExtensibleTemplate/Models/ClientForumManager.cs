using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace IdentitySample.Models
{
    public class ClientForumManager
    {
        private ApplicationDbContext _db;
        public ClientForumManager(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }


        public void Create(ClientForum forum)
        {
            var nameExists = _db.ClientForums.Any(f => f.Name.ToUpper() == forum.Name.ToUpper());
            if(nameExists)
            {
                throw new System.Exception("That name already exists");
            }

            _db.ClientForums.Add(forum);
            _db.SaveChanges();
        }


        public async Task CreateAsync(ClientForum forum)
        {
            var nameExists = await _db.ClientForums.AnyAsync(f => f.Name.ToUpper() == forum.Name.ToUpper());
            if (nameExists)
            {
                throw new System.Exception("That name already exists");
            }

            _db.ClientForums.Add(forum);
            await _db.SaveChangesAsync();
        }


        public void AddUserToForum(string userId, string forumId)
        {
            var forum = _db.ClientForums.FirstOrDefault(f => f.Id == forumId);
            if (forum == null)
            {
                throw new ArgumentNullException("ClientForum");
            }

            // This is clunky, but it works for now:
            var userForum = forum.Users.Any(u => u.ApplicationUserId == userId);
            {
                if (!userForum)
                {
                    forum.Users.Add(new UserClientForum
                    {
                        ApplicationUserId = userId,
                        ClientForumId = forumId
                    });
                    _db.SaveChanges();
                }
            }
        }


        public async Task AddUserToForumAsync(string userId, string forumId)
        {
            var forum = await _db.ClientForums.FirstOrDefaultAsync(f => f.Id == forumId);
            if(forum == null)
            {
                throw new ArgumentNullException("ClientForum");
            }

            // This is clunky, but it works for now:
            var userForum = forum.Users.Any(u => u.ApplicationUserId == userId);
            {
                if(!userForum)
                {
                    forum.Users.Add(new UserClientForum
                    {
                        ApplicationUserId = userId,
                        ClientForumId = forumId
                    });
                    await _db.SaveChangesAsync();
                }
            }
        }


        public void SetUserForums(string userId, params string[] ForumIds)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            user.ClientForums.Clear();
            _db.SaveChanges();
            foreach(var forumId in ForumIds)
            {
                this.AddUserToForum(userId, forumId);
            }
        }


        public async Task SetUserForumsAsync(string userId, params string[] ForumIds)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.ClientForums.Clear();
            await _db.SaveChangesAsync();
            foreach (var forumId in ForumIds)
            {
                await this.AddUserToForumAsync(userId, forumId);
            }
        }
    }
}