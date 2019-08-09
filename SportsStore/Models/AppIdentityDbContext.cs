using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class AppIdentityDbContext:IdentityDbContext<IdentityUser>
    {
        // IdentityDbContext 为EF Core提供了身份认定
        // IdentityUser 是内置的用户类，实例化为登陆管理员
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options): base(options)
        {

        }
    }
}
