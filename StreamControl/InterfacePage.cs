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
        var actualLatency =  new Label {Text = $"{latencyUpDown.Value}ms"};
        var latencyRow = new TableRow(new TableCell(latencyLabel), new TableCell(latencyUpDown), new TableCell(actualLatency));

        var packetLossSlider = new Slider
        {
            MinValue = 0,
            MaxValue = 100,
            Width = 400,
            Height = 40
        };
        packetLossSlider.ValueChanged += PacketLossSliderOnValueChanged;
        var packetLossLabel = new Label {Text = "Packet loss (%): "};
        var packetLossRow = new TableRow(new TableCell(packetLossSlider), new TableCell(packetLossLabel));
        Content = new TableLayout(latencyRow, packetLossRow);
        
        var speedSlider = new Slider
        {
            MinValue = 0,
            MaxValue = 100,
            Width = 400,
            Height = 40
        };
        speedSlider.ValueChanged += SpeedSliderOnValueChanged;
        var speedLabel = new Label {Text = "Speed (%): "};
        var speedRow = new TableRow(new TableCell(speedSlider), new TableCell(speedLabel));
        
        Content = new TableLayout(latencyRow, packetLossRow, speedRow);
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