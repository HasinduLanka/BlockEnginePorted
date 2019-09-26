using System;

namespace BlockEngine.DX
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
            //BlockEngine.Main.Game = new BlockEngine.Game1();
            //BlockEngine.Main.Game.Run();
            BlockEngine.Main.platform = BlockEngine.Main.Platform.DirectX;
            var Frm = new FrmMenu();
            Frm.ShowDialog();
        }
    }
}
