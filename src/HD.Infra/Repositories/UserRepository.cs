using HD.Domain.Users.Entities;
using HD.Domain.Users.Interfaces;
using HD.Infra.Context;

namespace HD.Infra.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context) { }

    public async Task<IEnumerable<User>> GetUsersByDepartment(Guid departmentId) => await Search(p => p.DepartmentId == departmentId);
}
