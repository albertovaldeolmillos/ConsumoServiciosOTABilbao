using ConsumoServiciosWeb.PxEndpointWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumoServiciosWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            //Para ejecutar este servicio es necesario:
            //  1- tener acceso al equipo de Desarrollo de OtaBilbao local (192.168.71.5 ---administrador/Gertek.2019)
            //  2- tener arrancado el servicio TariffComputer.asmx en el IIS Express local (localhost://65020) para lo que es necesrio:
            //      a- ejecutarlo directamente (GatewayListener/PIC/TariffComputer/TariffComputer.WS)
            //      b- lanzar cualquiera de las aplicaciones de GatewayListener (OTSlistenerService, Tariffstest ...) 
            TariffComputerLocal.TariffComputerWSSoapClient clienttariff = new TariffComputerLocal.TariffComputerWSSoapClient("TariffComputerWSSoap");
            var usuT = clienttariff.ClientCredentials.UserName;//usu=null, psw=null
            //clienttariff.ClientCredentials.UserName.UserName = @"bilbokoudala\extappota";
            //clienttariff.ClientCredentials.UserName.Password = "eER8TncC";
            string XML_ReqTA = @"<ipark_in><ins_id>100001</ins_id><lic_pla>rrr</lic_pla><date>16205305062020</date><grp_id>100026</grp_id><tar_id>100001</tar_id><amou_off>5</amou_off><prov>BILBAOTAO</prov><ext_acc>tamaraclaveria7@gmail.com</ext_acc><free_time>0</free_time><vers>1.0</vers><ah>1111111111111111</ah></ipark_in>";
            string XML_ResponseTAw = clienttariff.QueryParkingOperationWithAmountSteps(XML_ReqTA);


            //Para ejecutar este servicio NO es necesario tener acceso a la VPN de desarrollo (urruti.bilbao.eus --- 320625/Gertek.2020):
            string XML_Request = "<ipark_in><f>12000873</f><city_id>40100</city_id><d>2020-02-05T17:17:27</d><vers>1.0</vers><ah>B53AB639FF6EEEB1</ah><em>BILBAOTAO</em></ipark_in>";
            integraExternalServicesRef.integraExternalServicesSoapClient client = new integraExternalServicesRef.integraExternalServicesSoapClient("integraExternalServicesSoap");
            var usu = client.ClientCredentials.UserName;//usu=null, psw=null
            client.ClientCredentials.UserName.UserName = @"bilbokoudala\extappota";
            client.ClientCredentials.UserName.Password = "eER8TncC";
            string XML_Response5 = client.QueryFinePaymentQuantity(XML_Request);

            //Para ejecutar este servicio solo es necesario tener acceso a la VPN de desarrollo (urruti.bilbao.eus --- 320625/Gertek.2020):
            PxEndpointWCF.EndpointsClient client3 = new PxEndpointWCF.EndpointsClient();
            var usu3 = client3.ClientCredentials.UserName;//usu=null, psw=null
            ConsolaSoapHeader consolaSoap = new ConsolaSoapHeader();
            consolaSoap.NomUsuario = @"bilbokoudala\extappota";
            consolaSoap.Password = "eER8TncC";
            string XML_Response = client3.rdPQueryFinePaymentQuantity(consolaSoap, XML_Request);
        }
    }
}
