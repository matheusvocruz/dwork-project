using Dworks.Application.Interfaces.Queries;
using DWorks.Domain.Entities;
using DWorks.Domain.Exceptions;
using DWorks.Domain.Interfaces.Repositories;

namespace Dworks.Application.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IUserRepository _userRepository;

        public UserQueries(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }

        public async Task<bool> hasUserWithId(long id)
        {
            try
            {
                return await _userRepository.hasUserWithId(id);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }

        public async Task<User> getById(long id)
        {
            try
            {
                return await _userRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}
