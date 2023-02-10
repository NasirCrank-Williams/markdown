using markdown.Shared.Models;

namespace markdown.Server.Data.Repositories
{
	public interface IUserRepository
	{
		public Task<User> GetUser(Guid userId);
		public Task AddUser(User user);
		public Task UpdateUser(User user);
		public Task DeleteUser(Guid userId);
	}
}
