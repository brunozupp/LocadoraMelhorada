CREATE DATABASE LocadoraMelhorada
GO

USE LocadoraMelhorada
GO

CREATE TABLE Usuarios(
	Id INT PRIMARY KEY IDENTITY,
	Nome NVARCHAR(100) NOT NULL,
	Login NVARCHAR(100) NOT NULL,
	Senha NVARCHAR(255) NOT NULL
)
GO

INSERT INTO Usuarios(Nome,Login,Senha) VALUES(@Nome,@Login,@Senha);
Select SCOPE_IDENTITY();

UPDATE Usuarios SET Nome = @Nome, Login = @Login, Senha = @Senha WHERE UsuarioId = @UsuarioId;

DELETE FROM Usuarios WHERE UsuarioId = @UsuarioId;

SELECT UsuarioId as UsuarioId,Nome as Nome,Login as Login, Senha as Senha FROM Usuarios;

SELECT UsuarioId as UsuarioId,Nome as Nome,Login as Login, Senha as Senha FROM Usuarios WHERE UsuarioId = @UsuarioId;


CREATE TABLE Filmes(
	Id INT PRIMARY KEY IDENTITY,
	Titulo NVARCHAR(100) NOT NULL,
	Diretor NVARCHAR(100) NOT NULL
)
GO

INSERT INTO Filmes(Titulo,Diretor) VALUES(@Titulo,@Diretor);
Select SCOPE_IDENTITY();

UPDATE Filmes SET Titulo = @Titulo, Diretor = @Diretor WHERE FilmeId = @FilmeId;

DELETE FROM Filmes WHERE FilmeId = @FilmeId;

SELECT FilmeId as FilmeId,Titulo as Titulo,Diretor as Diretor FROM Filmes;

SELECT FilmeId as FilmeId,Titulo as Titulo,Diretor as Diretor FROM Filmes WHERE FilmeId = @FilmeId;

CREATE TABLE Votos(
	Id INT PRIMARY KEY IDENTITY,
	UsuarioId INT NOT NULL,
	FilmeId INT NOT NULL,
	FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),
	FOREIGN KEY (FilmeId) REFERENCES Filmes(Id),
)
GO

INSERT INTO Votos(UsuarioId,FilmeId) VALUES(@UsuarioId,@FilmeId);
Select SCOPE_IDENTITY();

UPDATE Votos SET UsuarioId = @UsuarioId, FilmeId = @FilmeId WHERE VotoId = @VotoId;

DELETE FROM Votos WHERE VotoId = @VotoId;

SELECT V.VotoId as VotoId,
F.FilmeId as FilmeId, F.Titulo as Titulo, F.Diretor as Diretor,
U.UsuarioId as UsuarioId, U.Nome as Nome, U.Login as Login
FROM Votos V
INNER JOIN Filmes F ON F.FilmeId = V.FilmeId
INNER JOIN Usuarios U ON U.UsuarioId = V.UsuarioId;

SELECT V.VotoId as VotoId,
F.FilmeId as FilmeId, F.Titulo as Titulo, F.Diretor as Diretor,
U.UsuarioId as UsuarioId, U.Nome as Nome, U.Login as Login
FROM Votos V
INNER JOIN Filmes F ON F.FilmeId = V.FilmeId
INNER JOIN Usuarios U ON U.UsuarioId = V.UsuarioId
WHERE V.VotoId = @VotoId;

SELECT * FROM Usuarios
SELECT * FROM Filmes
SELECT * FROM Votos