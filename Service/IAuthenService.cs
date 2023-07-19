using SampleAPI.Dtos;
using SampleAPI.Models;

namespace SampleAPI.Service
{
    public interface IAuthenService
    {
        UserLoginDtos? Login(LoginDtos dto);

        RegisterDtos? Register(RegisterDtos dto);

        User GetUser(Int32 id);
        List<User> GetAllUsers();
    }
}
