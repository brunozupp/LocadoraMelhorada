namespace LocadoraMelhorada.Infra.Data.Repositories.SqlServer.Queries
{
    public static class FilmeQueries
    {
        public static string Inserir = @"INSERT INTO Filmes(Titulo,Diretor) VALUES(@Titulo,@Diretor);
                                        Select SCOPE_IDENTITY();";

        public static string Atualizar = @"UPDATE Filmes SET Titulo = @Titulo, Diretor = @Diretor WHERE Id = @Id;";

        public static string Excluir = @"DELETE FROM Filmes WHERE Id = @Id;";

        public static string Listar = @"SELECT Id as FilmeId,Titulo as Titulo,Diretor as Diretor FROM Filmes;";

        public static string Obter = @"SELECT Id as FilmeId,Titulo as Titulo,Diretor as Diretor FROM Filmes WHERE Id = @Id;";

        public static string CheckId = @"Select Id From Filmes Where Id=@Id";
    }
}
