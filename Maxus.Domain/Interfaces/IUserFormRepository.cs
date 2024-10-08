using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface IUserFormRepository 
    {
        Task<tbl_UserFormRights> CreateAsync(tbl_UserFormRights obj);

        Task<tbl_UserFormRights?> GetByIdAsync(int id , int FormId);

        Task<bool> UpdateAsync(tbl_UserFormRights obj);
    }
}
