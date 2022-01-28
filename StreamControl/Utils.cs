using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace StreamControl
{
    public class Utils
    {
        public static bool IsRoot => geteuid() == 0;

        [DllImport ("libc")]
        private static extern int geteuid();

        public static string Run(string process, string args)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                FileName = process,
                Arguments = args
            };
            var p = Process.Start(psi);
            return p?.StandardOutput.ReadToEnd() ?? string.Empty;
        }

        public static IEnumerable<AdvancedNetworkInterface> ListInterfaces()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces().Where(x => x.NetworkInterfaceType != NetworkInterfaceType.Loopback))
            {
                yield return new AdvancedNetworkInterface(nic);
            }
        }
    }
}