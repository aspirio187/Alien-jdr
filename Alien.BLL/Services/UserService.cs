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

            UserEntity userToCreate = _mapper.Map<UserEntity>(user);
            userToCreate.Password = HashHelper.HashUsingPbkdf2(user.Password, "SaltADeplacer");
            _userRepository.Create(userToCreate);
            return _userRepository.SaveChanges();
        }

        public async Task<UserDto> SignInAsync(UserSignInDto user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            string hashedPassword = HashHelper.HashUsingPbkdf2(user.Password, "SaltADeplacer");
            UserEntity userFromRepo = await _userRepository.SignInAsync(user.Username, hashedPassword);
            if (userFromRepo is null) return null;

            UserDto userToReturn = _mapper.Map<UserDto>(userFromRepo);
            return userToReturn;
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("User ID is empty!");
            UserEntity userFromRepo = await _userRepository.GetByKeyAsync(id);
            if (userFromRepo is null) throw new NullReferenceException(nameof(userFromRepo));
            return _mapper.Map<UserDto>(userFromRepo);
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _userRepository.GetAllAsync());
        }
    }
}
