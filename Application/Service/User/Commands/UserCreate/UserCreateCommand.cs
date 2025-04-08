
using Domain.Entity.Program;

namespace Application.Service.User.Commands.UserCreate
{
    public class UserCreateInputCommand
    {
        public string Mail { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string TypeIdentification { get; set; }
        public string Gender { get; set; }
        public string IdProgram { get; set; }

        public UserCreateInputCommand()
        {
        }
    }

    public class UserAuthInputCommand
    {
        public string UserId { get; set; }

        public UserAuthInputCommand(string UserId)
        {
            this.UserId = UserId;
        }
    }
 
    public class UserCreateOutputCommand
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

        public UserCreateOutputCommand()
        {
        }
    }
}