using System;
using jspank.devsuite.domain.entitie;
using jspank.devsuite.domain.repository;

namespace jspank.devsuite.domain.service
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
