namespace LocadoraMelhorada.Domain.Entidades
{
    public class Usuario : EntidadeBase<string>
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

        public Usuario(string id, string nome, string login, string senha) : base(id)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
        }
    }
}
