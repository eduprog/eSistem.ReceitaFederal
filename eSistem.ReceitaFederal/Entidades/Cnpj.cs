using System.Collections.Generic;

namespace eSistem.ReceitaFederal.Entidades
{
    /// <summary>
    /// Classe CNPJ os nomes das propriedades tem de ficar como estão, pois caso contrário o JSON não consegue serializar.
    /// </summary>
    public class Cnpj
    {
        public List<AtividadePrincipal> atividade_principal { get; set; }
        public string data_situacao { get; set; }
        public string nome { get; set; }
        public string uf { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public List<AtividadesSecundaria> atividades_secundarias { get; set; }
        public List<Qsa> qsa { get; set; }
        public string situacao { get; set; }
        public string bairro { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cep { get; set; }
        public string municipio { get; set; }
        public string abertura { get; set; }
        public string natureza_juridica { get; set; }
        public string cnpj { get; set; }
        public string ultima_atualizacao { get; set; }
        public string status { get; set; }
        public string tipo { get; set; }
        public string fantasia { get; set; }
        public string complemento { get; set; }
        public string efr { get; set; }
        public string motivo_situacao { get; set; }
        public string situacao_especial { get; set; }
        public string data_situacao_especial { get; set; }
        public string capital_social { get; set; }
        public Extra extra { get; set; } // Criada apenas para uso da deserealização e não gerar exception
    }


   
}
