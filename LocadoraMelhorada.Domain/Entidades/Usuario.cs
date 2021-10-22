namespace LocadoraMelhorada.Domain.Entidades
{
    public class Usuario : EntidadeBase<long>
    {
        public string Nome { get; private set; }

        public string Login { get; private set; }

        public string Senha { get; private set; }

        public Usuario(string nome, string login, string senha) : base()
        {
            Nome = nome;
            Login = login;
            Senha = senha;
        }

        public Usuario(long id, string nome, string login, string senha) : base(id)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
        }
    }
}
