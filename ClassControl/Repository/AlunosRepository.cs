using ClassControl.Domain.Entities;
using System.Data.SqlClient;

namespace ClassControl.Repository
{
    public class AlunosRepository
    {
        public string dbConnection = @$"Data Source = yourBase; Initial Catalog = db; User ID = root; Password = yourPass;";
        public async Task AdicionarAluno(AlunosEntity aluno)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"INSERT INTO CLASS.Alunos(
                                                          Nome,Email) VALUES (@Nome , @Email)";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@Nome", aluno.Nome);
                    command.Parameters.AddWithValue("@Email", aluno.Email);
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
        public async Task EditarAluno(AlunosEntity aluno)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"UPDATE CLASS.Alunos SET
                                          Nome = @Nome,
                                          UpdDate = GETDATE(),
                                          Email = @Email WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@Nome", aluno.Nome);
                    command.Parameters.AddWithValue("@Email", aluno.Email);
                    command.Parameters.AddWithValue("@ID", aluno.ID);
                    await command.ExecuteNonQueryAsync();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task ExcluirAluno(int alunoID)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"UPDATE CLASS.Alunos SET
                                          DelDate = GETDATE()
                                          WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@ID", alunoID);
                    await command.ExecuteNonQueryAsync();

                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<AlunosEntity>> ListarAlunos()
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"SELECT  [ID]
                                          ,[Nome]
                                          ,[Email]
                                          ,[DelDate]
                                          ,[UpdDate]
                                      FROM [CLASSADMIN].[CLASS].[Alunos]
                                   WHERE DelDate IS NULL";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    List<AlunosEntity> alunos = new List<AlunosEntity>();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        AlunosEntity alunosEntity = new AlunosEntity()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Email = reader["Email"].ToString(),
                            Nome = reader["Nome"].ToString()
                        };
                        alunos.Add(alunosEntity);
                    }
                    conn.Close();
                    return alunos;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<AlunosEntity> BuscarPorID(int ID)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection))
            {
                try
                {
                    conn.Open();
                    string cmd = @"SELECT  [ID]
                                          ,[Nome]
                                          ,[Email]
                                          ,[DelDate]
                                          ,[UpdDate]
                                      FROM [CLASSADMIN].[CLASS].[Alunos]
                                   WHERE DelDate IS NULL AND ID = @ID";
                    SqlCommand command = new SqlCommand(cmd, conn);
                    command.Parameters.AddWithValue("@ID", ID);
                    AlunosEntity aluno = new AlunosEntity();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        aluno = new AlunosEntity()
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Email = reader["Email"].ToString(),
                            Nome = reader["Nome"].ToString()
                        };

                    }
                    conn.Close();
                    return aluno;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}