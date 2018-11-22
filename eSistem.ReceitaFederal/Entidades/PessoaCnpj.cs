namespace eSistem.ReceitaFederal.Entidades
{
    public class PessoaCnpj
    {
        public int CodEmresa { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string NaturezaJuridica { get; set; }
        public string AtividadeEconomicaPrimaria { get; set; }
        public string AtividadeEconomicaSecundaria { get; set; }
        public string NumeroDaInscricao { get; set; }
        public string MatrizFilial { get; set; }
        public string SituacaoCadastral { get; set; }
        public string DataSituacaoCadastral { get; set; }
        public string MotivoSituacaoCadastral { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Cnae { get; set; }

        // PROPRIEDADES ADICIONADAS
        public string Email { get; set; }
        public string Telefones { get; set; }
        public string DataAbertura { get; set; }
        public string SituacaoEspecial { get; set; }
        public string DataSituacaoEspecial { get; set; }
        public string EnteFederativoResponsavel { get; set; }
    }
}