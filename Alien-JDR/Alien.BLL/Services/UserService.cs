using Alien.BLL.Dtos;
using Alien.BLL.Interfaces;
using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using Alien.Tools.Helpers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ModelValidator _modelState;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool SignUp(UserSignUpDto user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            if (!_modelState.IsValid(user)) return false;

            var userToCreate = _mapper.Map<UserEntity>(user);
            userToCreate.Password = HashHelper.HashUsingPbkdf2(user.Password, "SaltADeplacer");
            _userRepository.Create(userToCreate);
            return _userRepository.SaveChanges();
        }

        public string SignIn(UserSignInDto user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            return string.Empty;
        }
    }
}
