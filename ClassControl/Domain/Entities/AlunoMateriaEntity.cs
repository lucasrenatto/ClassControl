namespace ClassControl.Domain.Entities
{
    public class AlunoMateriaEntity
    {
        public int ID { get; set; }
        public int AlunoID { get; set; }
        public int MateriaID { get; set; }
        public AlunosEntity Aluno { get; set; }
        public MateriasEntity Materia { get; set; }
    }
}
