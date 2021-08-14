using System.Threading.Tasks;

namespace UserAPI.Interfaces
{
    public interface IPhoneAlreadyExist
    {
        Task<bool>  PhoneAlreadyExists(string phoneNumber);
    }
}