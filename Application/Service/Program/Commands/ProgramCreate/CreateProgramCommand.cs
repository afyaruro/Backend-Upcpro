using Application.Service.Faculty.Commands.FacultyCreate;

namespace Application.Service.Program.Commands.ProgramCreate

{
    public class CreateInputProgramCommand
    {

        public string Name { get; set; }
        public string IdFaculty { get; set; }

        public CreateInputProgramCommand(string name, string idFaculty)
        {
            this.Name = name;
            this.IdFaculty = idFaculty;
        }
    }

    public class CreateOutputProgramCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public CreateOutputFacultyCommand Faculty { get; set; }



        public CreateOutputProgramCommand(string name, string id, CreateOutputFacultyCommand faculty)
        {
            this.Name = name;
            this.Id = id;
            this.Faculty = faculty;
        }
    }
}