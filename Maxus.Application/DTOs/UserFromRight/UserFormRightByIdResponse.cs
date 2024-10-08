namespace Maxus.Application.DTOs.UserFromRight
{
    public class UserFormRightByIdResponse
    {
        public int UserFormRightsId { get; set; }

        public long UserId { get; set; }

        public int FormId { get; set; }

        public Boolean CanView { get; set; }

        public Boolean CanAdd { get; set; }

        public Boolean CanEdit { get; set; }

        public Boolean CanDelete { get; set; }

        public Boolean CanExport { get; set; }
    }
}
