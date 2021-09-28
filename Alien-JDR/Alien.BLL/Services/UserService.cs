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
            _modelState = new ModelValidator();
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

        public async Task<string> SignInAsync(UserSignInDto user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            string hashedPassword = HashHelper.HashUsingPbkdf2(user.Password, "SaltADeplacer");
            var userFromRepo = await _userRepository.SignInAsync(user.Username, hashedPassword);
            if (userFromRepo is null) return string.Empty;

            string token = string.Empty;
            // Retourner soit un token, soit un claim, bref quelque chose qui permettra à l'utilisateur d'avoir une session active

            return token;
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("User ID is empty!");
            UserEntity userFromRepo = await _userRepository.GetByKeyAsync(id);
            if (userFromRepo is null) throw new NullReferenceException(nameof(userFromRepo));
            return _mapper.Map<UserDto>(userFromRepo);
        }
    }
}
