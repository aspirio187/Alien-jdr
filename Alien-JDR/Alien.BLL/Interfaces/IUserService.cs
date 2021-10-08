using Alien.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.BLL.Interfaces
{
    public interface IUserService 
    {
        bool SignUp(UserSignUpDto user);
        Task<string> SignInAsync(UserSignInDto user);
        Task<UserDto> GetUserAsync(Guid id);
    }
}
