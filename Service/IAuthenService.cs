using SampleAPI.Dtos;

namespace SampleAPI.Service
{
    public interface IAuthenService
    {
        UserLoginDtos? Login(LoginDtos dto);

        RegisterDtos? Register(RegisterDtos dto);
    }
}
