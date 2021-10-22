namespace LocadoraMelhorada.Infra.Data.Repositories.SqlServer.Queries
{
    public static class VotoQueries
    {
        public static string Inserir = @"INSERT INTO Votos(UsuarioId,FilmeId) VALUES(@UsuarioId,@FilmeId);
                                        Select SCOPE_IDENTITY();";

        public static string Excluir = @"DELETE FROM Votos WHERE Id = @Id;";

        public static string Listar = @"SELECT V.Id as VotoId,
                                        F.FilmeId as FilmeId, F.Titulo as Titulo, F.Diretor as Diretor,
                                        U.UsuarioId as UsuarioId, U.Nome as Nome, U.Login as Login
                                        FROM Votos V
                                        INNER JOIN Filmes F ON F.Id = V.FilmeId
                                        INNER JOIN Usuarios U ON U.Id = V.UsuarioId;";

        public static string ListarPorUsuario = @"SELECT V.Id as VotoId,
                                        F.Id as FilmeId, F.Id as Titulo, F.Diretor as Diretor
                                        FROM Votos V
                                        INNER JOIN Filmes F ON F.Id = V.FilmeId
                                        WHERE V.UsuarioId = @UsuarioId;";

        public static string CheckId = @"Select Id From Votos Where Id=@Id";

        public static string JaFoiVotado = @"Select Id From Votos Where UsuarioId=@UsuarioId AND FilmeId=@FilmeId";
    }
}
