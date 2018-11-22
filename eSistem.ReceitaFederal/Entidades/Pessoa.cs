using eSistem.ReceitaFederal.Entidades.Enum;

namespace eSistem.ReceitaFederal.Entidades
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Uf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public QualificacaoSocial QualificacaoSocial { get; set; }
    }
}