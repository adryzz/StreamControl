using System.Collections.ObjectModel;
using Eto.Forms;
using Eto.Drawing;

namespace StreamControl
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Title = "StreamControl";
            ClientSize = new Size(800, 600);
            var pages = new Collection<TabPage>();
            var tab = new TabControl();
            foreach (AdvancedNetworkInterface nic in Utils.ListInterfaces())
            {
                tab.Pages.Add(new InterfacePage(nic));
            }

            Content = tab;
            Shown += OnShown;
        }

        private void OnShown(object? sender, EventArgs e)
        {
            if (Utils.IsRoot)
            {
                MessageBox.Show("This app can't work without root access.");
                Environment.Exit(-1);
            }
        }
    }
}