using System.Runtime.CompilerServices;
using Eto.Forms;

namespace StreamControl;

public class InterfacePage : TabPage
{
    public AdvancedNetworkInterface Interface;
    
    public InterfacePage(AdvancedNetworkInterface nic)
    {
        Text = nic.Interface.Name;
        Interface = nic;
        var latencyUpDown = new NumericStepper
        {
            MinValue = 0,
            MaxValue = 20000,
            Value = Interface.Latency
        };
        latencyUpDown.ValueChanged += LatencyUpDownOnValueChanged;
        var latencyLabel = new Label {Text = "Latency (ms): "};
        var latencyRow = new StackLayout(new StackLayoutItem(latencyLabel), new StackLayoutItem(latencyUpDown));

        var packetLossSlider = new Slider
        {
            MinValue = 0,
            MaxValue = 100,
            Width = 400,
            Height = 40
        };
        packetLossSlider.ValueChanged += PacketLossSliderOnValueChanged;
        var packetLossLabel = new Label {Text = "Packet loss (%): "};
        var packetLossRow = new StackLayout(new StackLayoutItem(packetLossLabel), new StackLayoutItem(packetLossSlider));

        var speedSlider = new Slider
        {
            MinValue = 0,
            MaxValue = 100,
            Value = 100,
            Width = 400,
            Height = 40
        };
        speedSlider.ValueChanged += SpeedSliderOnValueChanged;
        var speedLabel = new Label {Text = "Speed (%): "};
        var speedRow = new StackLayout(new StackLayoutItem(speedLabel), new StackLayoutItem(speedSlider));
        
        Content = new StackLayout(latencyRow, packetLossRow, speedRow);
    }

    private void SpeedSliderOnValueChanged(object? sender, EventArgs e)
    {
        Interface.Rate = (int)((Slider) sender)?.Value;
    }

    private void PacketLossSliderOnValueChanged(object? sender, EventArgs e)
    {
        Interface.PacketLoss = (int)((Slider) sender)?.Value;
    }

    private void LatencyUpDownOnValueChanged(object? sender, EventArgs e)
    {
        Interface.Latency = (int)((NumericStepper) sender)?.Value;
    }
}