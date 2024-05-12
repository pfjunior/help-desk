using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HD.Identity.Api.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext(options);
