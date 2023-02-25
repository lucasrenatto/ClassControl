using ClassControl.Domain.Entities;
using ClassControl.Domain.Models;
using System.Data.SqlClient;

namespace ClassControl.Repository
{
    public class NotasRepository
    {
        public string dbConnection = @$"Data Source = yourBase; Initial Catalog = db; User ID = root; Password = yourPass;";
        public async Task AdicionarNota(NotasEntity nota)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"INSERT INTO CLASS.Notas(
                                                          Nota,Data,VinculoID) VALUES (@Nota , @Data, @VinculoID)";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@Nota", decimal.Parse(nota.NotaString));
                    command.Parameters.AddWithValue("@Data", nota.Data.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@VinculoID", nota.VinculoID);
                    await command.ExecuteNonQueryAsync();

                    conn.Close();
                    //Faturamentos.Add(new FaturamentoModel());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task EditarNota(NotasEntity nota)
        {

            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"UPDATE CLASS.Notas SET
                                          Nota = @Nota,
                                          UpdDate = GETDATE() WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@Nota", decimal.Parse(nota.NotaString));
                    command.Parameters.AddWithValue("@ID", nota.ID);
                    await command.ExecuteNonQueryAsync();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task ExcluirNota(int notaID)
        {

            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"UPDATE CLASS.Notas SET
                                          DelDate = GETDATE()
                                          WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@ID", notaID);
                    await command.ExecuteNonQueryAsync();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<NotasEntity>> ListarNotas()
        {

            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"SELECT  NOTA.[ID]
                                          ,NOTA.[Nota]
                                          ,NOTA.[Data]
                                          ,NOTA.[DelDate]
                                          ,NOTA.[UpdDate]
                                          ,NOTA.[VinculoID]
										  ,Aluno.Nome 'NomeAluno'
										  ,Materia.Nome 'NomeMateria'
                                      FROM [CLASSADMIN].[CLASS].[Notas] NOTA
									  LEFT JOIN CLASS.AlunoMateria AM ON AM.ID = NOTA.VinculoID 
									  LEFT JOIN CLASS.Alunos Aluno ON Aluno.ID = AM.AlunoID
									  LEFT JOIN CLASS.Materias Materia ON Materia.ID = AM.MateriaID
                                   WHERE NOTA.DelDate IS NULL
";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    List<NotasEntity> notas = new List<NotasEntity>();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        NotasEntity NotasEntity = new NotasEntity()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            NotaString = reader["Nota"].ToString(),
                            Data = DateTime.Parse(reader["Data"].ToString()),
                            VinculoID = Convert.ToInt32(reader["VinculoID"]),
                            Vinculo = new AlunoMateriaEntity()
                            {
                                Aluno = new AlunosEntity()
                                {
                                    Nome = reader["NomeAluno"].ToString()
                                },
                                Materia = new MateriasEntity()
                                {
                                    Nome = reader["NomeMateria"].ToString()
                                }
                            }
                        };
                        notas.Add(NotasEntity);
                    }
                    conn.Close();
                    return notas;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<NotasEntity> BuscarPorID(int ID)
        {

            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"SELECT  NOTA.[ID]
                                          ,NOTA.[Nota]
                                          ,NOTA.[Data]
                                          ,NOTA.[DelDate]
                                          ,NOTA.[UpdDate]
                                          ,NOTA.[VinculoID]
										  ,Aluno.Nome 'NomeAluno'
										  ,Materia.Nome 'NomeMateria'
                                      FROM [CLASSADMIN].[CLASS].[Notas] NOTA
									  LEFT JOIN CLASS.AlunoMateria AM ON AM.ID = NOTA.VinculoID 
									  LEFT JOIN CLASS.Alunos Aluno ON Aluno.ID = AM.AlunoID
									  LEFT JOIN CLASS.Materias Materia ON Materia.ID = AM.MateriaID
                                   WHERE NOTA.ID = @ID";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@ID", ID);
                    NotasEntity nota = new NotasEntity();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        nota = new NotasEntity()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Nota = Convert.ToDouble(reader["Nota"]),
                            Data = DateTime.Parse(reader["Data"].ToString()),
                            VinculoID = Convert.ToInt32(reader["VinculoID"]),
                            Vinculo = new AlunoMateriaEntity()
                            {
                                Aluno = new AlunosEntity()
                                {
                                    Nome = reader["NomeAluno"].ToString()
                                },
                                Materia = new MateriasEntity()
                                {
                                    Nome = reader["NomeMateria"].ToString()
                                }
                            }
                        };

                    }
                    conn.Close();
                    return nota;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<NotasEntity>> BuscarPorAluno(int ID)
        {

            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"SELECT  NOTA.[ID]
                                          ,NOTA.[Nota]
                                          ,NOTA.[Data]
                                          ,NOTA.[DelDate]
                                          ,NOTA.[UpdDate]
                                          ,NOTA.[VinculoID]
										  ,Aluno.Nome 'NomeAluno'
										  ,Materia.Nome 'NomeMateria'
                                      FROM [CLASSADMIN].[CLASS].[Notas] NOTA
									  LEFT JOIN CLASS.AlunoMateria AM ON AM.ID = NOTA.VinculoID 
									  LEFT JOIN CLASS.Alunos Aluno ON Aluno.ID = @ID
									  LEFT JOIN CLASS.Materias Materia ON Materia.ID = AM.MateriaID
                                   WHERE NOTA.DelDate IS NULL";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@ID", ID);
                    NotasEntity nota = new NotasEntity();
                    List<NotasEntity> notas = new List<NotasEntity>();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        nota = new NotasEntity()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Nota = Convert.ToDouble(reader["Nota"]),
                            Data = DateTime.Parse(reader["Data"].ToString()),
                            VinculoID = Convert.ToInt32(reader["VinculoID"]),
                            Vinculo = new AlunoMateriaEntity()
                            {
                                Aluno = new AlunosEntity()
                                {
                                    Nome = reader["NomeAluno"].ToString()
                                },
                                Materia = new MateriasEntity()
                                {
                                    Nome = reader["NomeMateria"].ToString()
                                }
                            }
                        };
                        notas.Add(nota);

                    }
                    conn.Close();
                    return notas;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}

