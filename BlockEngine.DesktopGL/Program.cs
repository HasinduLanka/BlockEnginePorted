using System;

namespace BlockEngine.DesktopGL
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            BlockEngine.Main.platform = BlockEngine.Main.Platform.DesktopGL;
            var Frm = new FrmMenu();
            Frm.ShowDialog();
        }
    }
}
