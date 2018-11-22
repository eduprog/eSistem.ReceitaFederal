using System;
using System.Collections.Generic;
using eSistem.ReceitaFederal.Entidades.Enum;

namespace eSistem.ReceitaFederal.Entidades
{
    public class CnpjDesativado
    {
        public List<Atividade> AtividadePrincipal { get; set; } = new List<Atividade>();
        public List<Atividade> AtividadeSecundaria { get; set; } = new List<Atividade>();
        public DateTime DataSituacao { get; set; }
        public DateTime Abertura { get; set; }
        public Situacao Situacao { get; set; }
        public Pessoa Contribuinte { get; set; }
        public List<Pessoa> QuadroAssociativo { get; set; } = new List<Pessoa>();
        public string NaturezaJuridica { get; set; }
        public string CnpjRetorno { get; set; }

        public DateTime UltimaAtualizacao { get; set; }

        public Status Status { get; set; }
        public TipoLoja TipoLoja { get; set; }
        public string Fantasia { get; set; }
        public string Complemento { get; set; }
        public string Efr { get; set; }
        public string MotivoSituacao { get; set; }
        public string SituacaoEspecial { get; set; }
        public DateTime DataSituacaoEspecial { get; set; }
        public Decimal? CapitalSocial { get; set; }


    }
}
