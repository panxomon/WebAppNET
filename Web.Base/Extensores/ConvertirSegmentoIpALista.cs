using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Base
{
    public static class ConvertirSegmentoIpALista
    {

        public static IList<string> ToIpList(this string ip, params int[] cantidadIp)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return new List<string>();
            }
            var ipParseada = ParsearIp(ip);

            var listaIp = new List<string>();

            for (uint i = 0; i < cantidadIp[0]; i++)
            {
                listaIp.Add(FormatearIp(ipParseada + i));
            }

            return listaIp;
        }

        private static uint ParsearIp(string ip)
        {
            var ipEnBytes = ip.Split('.').Select(s => Byte.Parse(s)).ToArray();
            if (BitConverter.IsLittleEndian) Array.Reverse(ipEnBytes);
            return BitConverter.ToUInt32(ipEnBytes, 0);
        }

        private static string FormatearIp(uint ip)
        {
            var b = BitConverter.GetBytes(ip);
            if (BitConverter.IsLittleEndian) Array.Reverse(b);
            return string.Join(".", b.Select(n => n.ToString()));
        }
    }
}
