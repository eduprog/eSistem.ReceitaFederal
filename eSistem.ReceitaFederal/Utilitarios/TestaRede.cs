using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace eSistem.ReceitaFederal.Utilitarios
{
    /// <summary>
    /// Classe para testes de Placa de rede e acesso a rede LAN/MAN/WAN
    /// </summary>
    public static class TestaRede
    {
        #region Métodos

        /// <summary>
        /// Testa com ping se houve sucesso. Pode ser usado para testar conexão na rede interna como servidor e outros, bem como servidor externo na internet
        /// </summary>
        /// <param name="ip">Endereço Ip a Ser Testado. Default Null (Ele pinga o Gateway padrao</param>
        /// <returns>true ou false</returns>
        public static bool PingRede(string ip = null)
        {
            if (ip == null) ip = RetornaGatewayPadrao().ToString();
            if (new Ping().Send(ip)?.Status == IPStatus.Success)
            {
                return true;
            }
            throw new Exception("Seu equipamento não está conseguindo chegar ao GateWay Padrão da rede, portanto, não será possível acessar Internet");
        }

        /// <summary>
        /// Testa se existe placa de rede Ativa no equipamento
        /// </summary>
        /// <returns>True ou False</returns>
        public static bool PlacaRedeOk()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                return (from face in interfaces
                        where face.OperationalStatus == OperationalStatus.Up
                        where (face.NetworkInterfaceType != NetworkInterfaceType.Tunnel) && (face.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                        select face.GetIPv4Statistics()).Any(statistics => (statistics.BytesReceived > 0) && (statistics.BytesSent > 0));
            }
            throw new Exception("Não foi detectada uma placa de rede neste equipamento!");
        }

        /// <summary>
        /// Lista placas de redes (Ethernet) do equipamento
        /// </summary>
        /// <returns>Lista de Placa(s) Rede (Ethernet)</returns>
        public static List<PlacaRede> ListaEthernet()
        {
            List<PlacaRede> ethernet = new List<PlacaRede>();
            var replace = "$1:$2:$3:$4:$5:$6";
            var regex = "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})";
            if (PlacaRedeOk())
            {
                // however, this will include all adapters -- filter by opstatus and activity
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface networkInterface in interfaces)
                {
                    var placaRede = new PlacaRede
                    {
                        NomePlaca = networkInterface.Name,
                        TipoPlaca = networkInterface.NetworkInterfaceType.ToString(),
                        MacAddress = Regex.Replace(networkInterface.GetPhysicalAddress().ToString(), regex, replace)
                    };
                    foreach (UnicastIPAddressInformation address in networkInterface.GetIPProperties().UnicastAddresses)
                    {
                        placaRede.Ips.Add(address.Address.ToString());
                    }
                    ethernet.Add(placaRede);
                }
                return ethernet;
            }
            throw new Exception("Não foi possível identificar placa(s) de rede em seu equipamento!");
        }

        /// <summary>
        /// Retorna GateWay Padrão da placa Ativa
        /// </summary>
        /// <returns>GateWay Padrao</returns>
        public static IPAddress RetornaGatewayPadrao()
        {
            var rede = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address)
                .FirstOrDefault(a => a != null);

            if (rede == null)
            {
                throw new Exception("Não foi possível identificar o Gateway padrão de sua configuração de redes");
            }
            return rede;
        }


        /// <summary>
        /// Testa se acesso a internet está ok.
        /// </summary>
        /// <param name="url">Url a ser acessada. Default www.google.com</param>
        /// <returns>True ou False</returns>
        public static bool InternetOk(string url = "www.google.com")
        {
            try
            {
                IPHostEntry dummy = Dns.GetHostEntry(url);
                return true;
            }
            catch (SocketException)
            {
                throw new Exception("Não foi possível acessar a internet! Verifique seu DNS");
            }

        }

        #endregion

        #region Classe

        /// <summary>
        /// Classe que identifica dados das placas de rede do sistema, pode ser útil para relatórios de erros e informações para conexões em geral
        /// </summary>
        public class PlacaRede
        {
            public string NomePlaca { get; set; }
            public string TipoPlaca { get; set; }
            public List<string> Ips { get; set; } = new List<string>();
            public string MacAddress { get; set; }
        }

        #endregion
    }
}