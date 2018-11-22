using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using eSistem.ReceitaFederal.Entidades;
using eSistem.ReceitaFederal.Utilitarios;

namespace eSistem.ReceitaFederal.Servicos
{
    public static class Receita
    {
        /// <summary>
        /// Pesquisa Cnpj Site da Receita
        /// </summary>
        /// <param name="aCnpj">Cnpj a ser pesquisado. Somente Números</param>
        /// <returns>Objeto Cnpj Preenchido com os dados</returns>
        public static Cnpj ConsultaCnpj(string aCnpj)
        {
            // VALIDACAO MÍNIMA - EVITAR CONSULTA DESNECESSÁRIA AO SITE
            if (ValidaCampos(aCnpj.Trim()) == false) return null;
            HttpWebResponse response = null;
            Cnpj cnpj = null;
            string auxUri = "https://www.receitaws.com.br/v1/cnpj/" + aCnpj;
            try
            {
                if(TestaRede.InternetOk())
                {
                    HttpWebRequest request = (HttpWebRequest) WebRequest.Create(auxUri);
                    request.Method = WebRequestMethods.Http.Get;
                    request.Accept = "application/json";
                    response = (HttpWebResponse) request.GetResponse();
                    Stream responsestream = response.GetResponseStream();
                    if (responsestream != null)
                    {
                        DataContractJsonSerializer cnpjSer = new DataContractJsonSerializer(typeof(Cnpj));
                        cnpj = (Cnpj) cnpjSer.ReadObject(responsestream);
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)e.Response;
                    throw new Exception($"Código Erro: {(int)response.StatusCode}");
                }
                else
                {
                    throw new Exception($"Erro: {e.Status}\r\n{e.Message} - Verifique a conexão com a Internet");
                }
            }
            finally
            {
                response?.Close();
            }
            return cnpj;
        }

        public static bool ValidaCampos(string aCnpj)
        {
            // VALIDAÇÃO MÍNIMA BASICA.
            // EVITA CONSULTA DESNECESSÁRIA NO SITE DA RECEITA (MUITAS CONSULTAS PODEM LEVAR O BLOQUEIO DO IP)
            string erro = "";

            if (IsCnpj(aCnpj) == false)
                erro += "CNPJ não informado corretamente\n";

            if (erro.Length > 0)
            {
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Valida Cnpj
        /// </summary>
        /// <param name="cnpj">Cnpj</param>
        /// <returns>Retorna true Cnpj Válido</returns>
        private static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");

            if (cnpj.Length != 14)
            {
                return false;
            }
            else
            {
                var tempCnpj = cnpj.Substring(0, 12);

                var soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                var resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                var digito = resto.ToString();
                tempCnpj = tempCnpj + digito;

                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto;
                return cnpj.EndsWith(digito);
            }
        }
    }
}
