using DevFreela.Core.Entities;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastruture.Persistence;


namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewUserInputModel inputModel)
        {
            var user = new User(inputModel.FullName,
                inputModel.Email,
                inputModel.BirthDate);


            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public UserDetailsViewModel GetById(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            var userDetailsViewModel = new UserDetailsViewModel(
                user.Id,
                user.FullName,
                user.Email,
                user.BirthDate,
                user.Active,
                user.Skills,
                user.OwnedProjects,
                user.FreelanceProjects
                );

            return userDetailsViewModel;
        }

        public void Update(UpdateUserInputModel inputModel)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == inputModel.Id);

            user?.Update(inputModel.FullName, inputModel.Email, inputModel.BirthDate, inputModel.Active);

            _dbContext.SaveChanges();
        }
    }
}
