namespace ClassControl.Domain.Entities
{
    public class NotasEntity
    {
        public int ID { get; set; }
        public double Nota { get; set; }
        public string NotaString { get; set; }
        public DateTime Data { get; set; }
        public int VinculoID{ get; set; }
        public AlunoMateriaEntity? Vinculo{ get; set; }
    }
}
