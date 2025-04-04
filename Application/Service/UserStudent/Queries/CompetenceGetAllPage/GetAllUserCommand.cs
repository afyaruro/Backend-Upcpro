
using Domain.Entity.Program;

namespace Application.Service.User.Commands.UserGetAllPage
{
    public class GetAllPageUserOutputCommand
    {
        public string Id { get; set; }
        public string Mail { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string TypeIdentification { get; set; }
        public string Gender { get; set; }
        public string TypeUser { get; set; }
        public ProgramEntity Program { get; set; }

        public GetAllPageUserOutputCommand()
        {

        }
    }

    public class GetAllPageUserInputCommand
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public GetAllPageUserInputCommand(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}