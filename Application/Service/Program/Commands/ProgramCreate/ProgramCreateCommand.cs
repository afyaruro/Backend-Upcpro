using Application.Service.Faculty.Commands.FacultyCreate;

namespace Application.Service.Program.Commands.ProgramCreate

{
    public class ProgramCreateInputCommand
    {

        public string Name { get; set; }
        public string IdFaculty { get; set; }

        public ProgramCreateInputCommand(string name, string idFaculty)
        {
            this.Name = name;
            this.IdFaculty = idFaculty;
        }
    }

    public class ProgramCreateOutputCommand
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public FacultyCreateOutputCommand Faculty { get; set; }



        public ProgramCreateOutputCommand(string name, string id, FacultyCreateOutputCommand faculty)
        {
            this.Name = name;
            this.Id = id;
            this.Faculty = faculty;
        }
    }
}