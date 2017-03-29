using System;
using JSpank.DevSuite.Domain.Entitie;
using JSpank.DevSuite.Domain.Repository;

namespace JSpank.DevSuite.Domain.Service
{
    public class UserService : IUserService
    {
        readonly IUserRepository _IUserRepository;

        public UserService(IUserRepository _IUserRepository)
        {
            this._IUserRepository = _IUserRepository;
        }

        public User Me
        {
            get
            {
                var model = this._IUserRepository.Get(Environment.UserName);
                if (model == null)
                {
                    this._IUserRepository.Add(new User { lg_user = Environment.UserName });
                    model = this._IUserRepository.Get(Environment.UserName);
                }

                return model;
            }
        }
    }
}
