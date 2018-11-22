using System;
using System.Linq;
using System.Windows.Forms;
using eSistem.ReceitaFederal.Entidades;
using eSistem.ReceitaFederal.Servicos;

namespace AppTeste
{
    public partial class FrmConsultaCpnj : Form
    {

        public FrmConsultaCpnj() // ALTEREI O NOME DO FORMULARIO PARA NOME COMUM, PARA NAO CONFUNDIR COM A CLASSE ConsultaCNPJReceita
        {
            InitializeComponent();
        }

        private void frmConsultaCNPJ_Load(object sender, EventArgs e)
        {
            LimpaCampoCnpj();
        }

        private void btConsultar_Click(object sender, EventArgs e)
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>().ToArray().Where(x => x.Name != "txtCNPJ"))
            {
                tb.Clear();
            }
            ConsultaCnpj();

        }



        private void LimpaCampoCnpj()
        {
            Invoke((MethodInvoker)(() =>
            {
                txtCNPJ.Text = "";
                txtCNPJ.Focus();
            }));
        }





        private void ConsultaCnpj()
        {
            if (Receita.ValidaCampos(txtCNPJ.Text.Trim()) == false)
            {
                MessageBox.Show(@"CNPJ não está correto");
                return;
            }

            try
            {
                Cnpj cnpj = Receita.ConsultaCnpj(txtCNPJ.Text);
                if (cnpj == null)
                {
                    MessageBox.Show(@"CNPJ não encontrado na base da receita federal!");
                    return;
                }
                txtRazao.Text = cnpj.nome;
                txtMatrizFilial.Text = cnpj.tipo;
                txtDataAbertura.Text = cnpj.abertura;
                txtFantasia.Text = cnpj.fantasia;
                txtCNAE.Text = cnpj.atividade_principal.FirstOrDefault()?.code;
                txtAtividadeEconomeica.Text = cnpj.atividade_principal.FirstOrDefault()?.text;
                foreach (AtividadesSecundaria atividadesSecundaria in cnpj.atividades_secundarias)
                {
                    txtAtividadeEconomicaSecundaria.Text += $@"{atividadesSecundaria.code} - {atividadesSecundaria.text}{Environment.NewLine}";
                }
                txtNaturezaJuridica.Text = cnpj.natureza_juridica;
                txtLogradouro.Text = cnpj.logradouro;
                txtNumero.Text = cnpj.numero;
                txtComplemento.Text = cnpj.complemento;
                txtCEP.Text = $@"{cnpj.cep:00.000-000}";
                txtBairro.Text = cnpj.bairro;
                txtCidade.Text = cnpj.municipio;
                txtUF.Text = cnpj.uf;
                txtEmail.Text = cnpj.email;
                txtTelefone.Text = cnpj.telefone;
                txtResponsavel.Text = cnpj.efr;
                txtSituacaoCadastral.Text = cnpj.situacao;
                txtDataSituacaoCadastral.Text = $"{cnpj.data_situacao:dd/MM/yyyy}";
                txtMotivoSituacaoCadastral.Text = cnpj.motivo_situacao;
                txtSituacaoEspecial.Text = cnpj.situacao_especial;
                txtDataSituacaoEspecial.Text = $"{cnpj.data_situacao_especial:dd/MM/yyyy}";
                lblDataConsulta.Text = $"Efetuada: {cnpj.ultima_atualizacao.Substring(0,10):dd/MM/yyyy}" ;
                this.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Erro no processamento da consulta do CNPJ{Environment.NewLine}{e.Message}");
            }
        }
    }
}
