using Alien.BLL.Dtos;
using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Services
{
    public class UserRepository
    {
        private readonly IUserRepository _userService;
        private readonly IMapper _mapper;

        public UserRepository(IUserRepository userService, IMapper mapper)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public bool SignUp(UserSignUpDto user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            _userService.Create(_mapper.Map<UserEntity>(user));
            return _userService.SaveChanges();
        }

        public string SignIn(UserSignInDto user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));

            return string.Empty;
        }
    }
}
