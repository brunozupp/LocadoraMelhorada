namespace LocadoraMelhorada.Infra.Data.Repositories.SqlServer.Queries
{
    public static class UsuarioQueries
    {
        public static string Inserir = @"INSERT INTO Usuarios(Nome,Login,Senha) VALUES(@Nome,@Login,@Senha);
                                        Select SCOPE_IDENTITY();";

        public static string Atualizar = @"UPDATE Usuarios SET Nome = @Nome, Login = @Login, Senha = @Senha WHERE Id = @Id;";

        public static string Excluir = @"DELETE FROM Usuarios WHERE Id = @Id;";

        public static string Listar = @"SELECT Id as UsuarioId,Nome as Nome,Login as Login, Senha as Senha FROM Usuarios;";

        public static string Obter = @"SELECT Id as UsuarioId,Nome as Nome,Login as Login, Senha as Senha FROM Usuarios WHERE Id = @Id;";

        public static string CheckId = @"Select Id From Usuarios WHERE Id=@Id";

        public static string Autenticar = @"Select Id From Usuarios WHERE Login=@Login AND Senha=@Senha";
    }
}
