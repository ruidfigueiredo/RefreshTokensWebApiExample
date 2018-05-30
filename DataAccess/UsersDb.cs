using Microsoft.EntityFrameworkCore;

namespace RefreshTokensWebApiExample.DataAccess
{
    public class UsersDb : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDb(DbContextOptions<UsersDb> options): base(options) { }
    }
}