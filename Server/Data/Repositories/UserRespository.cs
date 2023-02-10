using markdown.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace markdown.Server.Data.Repositories
{
	public class UserRespository : IUserRepository
	{
		private readonly UserMarkdownDbContext _userMarkdownDbContext;

		public UserRespository(UserMarkdownDbContext userMarkdownDbContext)
		{
			this._userMarkdownDbContext = userMarkdownDbContext ?? throw new ArgumentNullException(nameof(userMarkdownDbContext));
		}

		public Task AddUser(User user)
		{
			try
			{
				user.UserId = Guid.NewGuid();
				_userMarkdownDbContext.Users.Add(user);
				_userMarkdownDbContext.SaveChanges();
			}
			catch (Exception) {
				throw new Exception("Failed to create user");
			}

			return Task.CompletedTask;
		}

		public async Task DeleteUser(Guid userId)
		{
			try
			{
				User? user = await _userMarkdownDbContext.Users.FirstOrDefaultAsync(user => user.UserId == userId);
				if (user != null)
				{
					_userMarkdownDbContext.Users.Remove(user);
					_userMarkdownDbContext.SaveChanges();
				}
				else
				{
					throw new ArgumentNullException();
				}
			}
			catch (ArgumentNullException) 
			{
				throw new ArgumentNullException("User does not exist");
			}
			catch (Exception) 
			{
				throw new Exception("Failed to delete user");
			}
		}

		public async Task<User> GetUser(Guid userId)
		{
			try
			{
				Console.WriteLine("What");
				User? user = await _userMarkdownDbContext.Users.FirstOrDefaultAsync(user => user.UserId == userId);
				if (user != null)
				{
					return user;
				}
				else
				{
					throw new ArgumentNullException();
				}
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("Failed to find user");
			}
			catch (Exception)
			{
				throw new Exception("Failed to get user");
			}
		}

		public async Task UpdateUser(User user)
		{
			try
			{
				User? userToUpdate = await _userMarkdownDbContext.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
				if (userToUpdate != null)
				{
					userToUpdate.UserName = user.UserName;
					userToUpdate.Email= user.Email;
					userToUpdate.Password = user.Password;

					_userMarkdownDbContext.Users.Update(userToUpdate);
					_userMarkdownDbContext.SaveChanges();
				}
				else
				{
					throw new ArgumentNullException();
				}
			}
			catch (ArgumentNullException)
			{
				throw new ArgumentNullException("Failed to find user");
			}
			catch (Exception) 
			{
				throw new Exception("Failed to update user");
			}
		}
	}
}
