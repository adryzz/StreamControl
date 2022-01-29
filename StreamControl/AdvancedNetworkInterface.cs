using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace StreamControl
{
    public class AdvancedNetworkInterface
    {
        public AdvancedNetworkInterface(NetworkInterface i)
        {
            Interface = i;
        }
    
        /// <summary>
        /// The underlying network interface
        /// </summary>
        public NetworkInterface Interface { get; }

        /// <summary>
        /// Packet loss %
        /// </summary>
        public int PacketLoss
        {
            get => GetFilter("loss");
            set => SetFilter("loss", $"{value}%");
        }
    
        /// <summary>
        /// Added latency
        /// </summary>
        public int Latency
        {
            get => GetFilter("delay");
            set => SetFilter("delay", $"{value}ms");
        }

        /// <summary>
        /// NIC speed (set to -1 to disable)
        /// </summary>
        public string Rate
        {
            set => SetFilter("rate", value);
        }

        private int GetFilter(string name)
        {
            try
            {
                string output = Utils.Run("tc", $"qdisc show dev {Interface.Name}");
                Regex r = new Regex($"{name}\\s(\\w+)");
                return int.Parse(r.Matches(output)[0].Groups[1].Value);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        private void SetFilter(string name, string value)
        {
            if (value.StartsWith('-'))
            {
                Utils.Run("tc", $"qdisc del dev {Interface.Name} root netem {name} 0");
            }
            else    
            {
                Utils.Run("tc", $"qdisc replace dev {Interface.Name} root netem {name} {value}");
            }
        }

        public void Reset()
        {
            SetFilter("loss", "-");
            SetFilter("delay", "-");
            SetFilter("rate", "-");
        }
    }
}