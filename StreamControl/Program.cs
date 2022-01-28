using Eto.Forms;

namespace StreamControl
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            new Application().Run(new MainForm());
        }
    }
}