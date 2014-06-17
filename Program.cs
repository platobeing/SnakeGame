/******************************************************************************
 * Filename:     Program.cs
 * Author:       platobeing<platobeing(c)gmail.com>
 * Created Date: 2014-06-15
 * Description:  The entry class.
 * ****************************************************************************
 * Winter is coming.
 * ***************************************************************************/

namespace Snake
{
    using System;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SnakeForm());
        }
    }
}
