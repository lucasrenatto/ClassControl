using ClassControl.Domain.Entities;
using ClassControl.Domain.Models;
using System.Data.SqlClient;

namespace ClassControl.Repository
{
    public class MateriasRepository
    {
        public string dbConnection = @$"Data Source = yourBase; Initial Catalog = db; User ID = root; Password = yourPass;";
        public async Task AdicionarMateria(MateriasEntity materia)
        {
            
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"INSERT INTO CLASS.Materias(
                                                          Nome,Descricao) VALUES (@Nome , @Descricao)";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@Nome", materia.Nome);
                    command.Parameters.AddWithValue("@Descricao", materia.Descricao);
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
        public async Task EditarMateria(MateriasEntity materia)
        {
            
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"UPDATE CLASS.Materias SET
                                          Nome = @Nome,
                                          UpdDate = GETDATE(),
                                          Descricao = @Descricao WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@Nome", materia.Nome);
                    command.Parameters.AddWithValue("@Descricao", materia.Descricao);
                    command.Parameters.AddWithValue("@ID", materia.ID);
                    await command.ExecuteNonQueryAsync();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task ExcluirMateria(int materiaID)
        {
            
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"UPDATE CLASS.Materias SET
                                          DelDate = GETDATE()
                                          WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@ID", materiaID);
                    await command.ExecuteNonQueryAsync();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<MateriasEntity>> ListarMaterias()
        {
            
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"SELECT [ID]
                                        ,[Nome]
                                        ,[Descricao]
                                        ,[UpdDate]
                                        ,[DelDate]
                                    FROM [CLASSADMIN].[CLASS].[Materias]
                                   WHERE DelDate IS NULL";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    List<MateriasEntity> materias = new List<MateriasEntity>();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        MateriasEntity materia = new MateriasEntity()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Descricao = reader["Descricao"].ToString(),
                            Nome = reader["Nome"].ToString()
                        };
                        materias.Add(materia);
                    }
                    conn.Close();
                    return materias;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<int>> BuscarMateriasJaVinculadas(int alunoID)
        {
            
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    string cmd = "";
                    conn.Open();
                    cmd = $@"SELECT
                                       [ID]
                                      ,[AlunoID]
                                      ,[MateriaID]
                                  FROM [CLASSADMIN].[CLASS].[AlunoMateria]
                                  WHERE AlunoID = {alunoID}";

                    SqlCommand command = new SqlCommand(cmd, conn);
                    List<int> ids = new List<int>();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        ids.Add(Convert.ToInt32(reader["MateriaID"]));
                    }
                    conn.Close();
                    return ids;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<AlunoMateriaEntity>> BuscarMateriasVinculadas(int alunoID)
        {
            
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    string cmd = "";
                    conn.Open();
                    cmd = $@"SELECT  AM.[ID]
                                 , AM.[AlunoID]
                                 , AM.[MateriaID]
                            	  , Materia.Nome 'NomeMateria'
                            	  ,Aluno.Nome 'NomeAluno'
                             FROM [CLASSADMIN].[CLASS].[AlunoMateria] AM
                             LEFT JOIN CLASS.Materias Materia ON Materia.ID = AM.MateriaID AND Materia.DelDate IS NULL
                             LEFT JOIN CLASS.Alunos Aluno ON Aluno.ID = AM.AlunoID AND Aluno.DelDate IS NULL
                             WHERE AM.AlunoID = {alunoID} ";

                    SqlCommand command = new SqlCommand(cmd, conn);
                    List<AlunoMateriaEntity> vinculos = new List<AlunoMateriaEntity>();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var vinculo = new AlunoMateriaEntity()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            AlunoID = Convert.ToInt32(reader["AlunoID"]),
                            MateriaID = Convert.ToInt32(reader["MateriaID"]),
                            Aluno = new AlunosEntity()
                            {
                                Nome = reader["NomeAluno"].ToString()
                            },
                            Materia = new MateriasEntity()
                            {
                                Nome = reader["NomeMateria"].ToString()
                            }
                        };
                        vinculos.Add(vinculo);
                    }
                    conn.Close();
                    return vinculos;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task VincularMaterias(List<int> idsMaterias, int alunoID)
        {
            
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    string cmd = "";
                    conn.Open();
                    foreach (var id in idsMaterias)
                    {
                        cmd = cmd + $@"INSERT INTO CLASS.AlunoMateria(
                                                          AlunoID,MateriaID) VALUES ({alunoID} ,{id});   ";
                    }
                    SqlCommand command = new SqlCommand(cmd, conn);
                    List<MateriasEntity> materias = new List<MateriasEntity>();
                    await command.ExecuteNonQueryAsync();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<MateriasEntity> BuscarPorID(int materiaID)
        {
            
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"SELECT [ID]
                                        ,[Nome]
                                        ,[Descricao]
                                        ,[UpdDate]
                                        ,[DelDate]
                                    FROM [CLASSADMIN].[CLASS].[Materias]
                                   WHERE DelDate IS NULL AND ID = @ID";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@ID", materiaID);
                    MateriasEntity materia = new MateriasEntity();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        materia = new MateriasEntity()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Descricao = reader["Descricao"].ToString(),
                            Nome = reader["Nome"].ToString()
                        };

                    }
                    conn.Close();
                    return materia;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}

