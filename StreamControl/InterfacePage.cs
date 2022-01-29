using System.ComponentModel;
using System.Runtime.CompilerServices;
using Eto.Forms;
using Gtk;
using ComboBox = Eto.Forms.ComboBox;
using Label = Eto.Forms.Label;

namespace StreamControl;

public class InterfacePage : TabPage
{
    public AdvancedNetworkInterface Interface;

    private ComboBox? speedBox;
    private NumericStepper? speedUpDown;
    
    public InterfacePage(AdvancedNetworkInterface nic, Form f)
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

        speedUpDown = new NumericStepper
        {
            MinValue = 0,
            MaxValue = 1000,
            Value = 1
        };
        speedUpDown.ValueChanged += SpeedOnValueChanged;
        var speedLabel = new Label {Text = "Speed: "};
        speedBox = new ComboBox();
        speedBox.DataStore = new string[]
        {
            "bps", "kbps", "mbps", "gbps"
        };
        speedBox.SelectedIndex = 3;
        speedBox.SelectedValueChanged += SpeedOnValueChanged;
        var speedRow = new StackLayout(new StackLayoutItem(speedLabel), new StackLayoutItem(speedUpDown), new StackLayoutItem(speedBox));
        

        Content = new StackLayout(latencyRow, packetLossRow, speedRow);
        f.Closing += FOnClosing;
    }

    private void FOnClosing(object? sender, CancelEventArgs e)
    {
        Interface.Reset();
    }

    private void SpeedOnValueChanged(object? sender, EventArgs e)
    {
        Interface.Rate = speedUpDown.Value + speedBox.SelectedValue.ToString();
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