using ClassControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassControl.Models
{
    public class Contexto : DbContext
    {
        public DbSet<MateriasEntity> Alunos { get; set; }
        public DbSet<MateriasEntity> Materiais { get; set; }
        public DbSet<NotasEntity> Notas { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
    }
}
