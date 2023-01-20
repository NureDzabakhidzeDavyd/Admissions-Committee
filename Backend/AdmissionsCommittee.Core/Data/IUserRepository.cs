using AdmissionsCommittee.Core.Domain;

namespace AdmissionsCommittee.Core.Data
{
    public interface IUserRepository : IRepository<UserProfile>
    {
        public Task<UserProfile?> GetUserByEmail(string email);

        public Task<UserProfile> GetUserByCredentials(string userName, string password);

        public Task<UserProfile> GetByFirstName(string username);
    }
}
