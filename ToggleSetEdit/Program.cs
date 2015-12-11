using System;
using System.Windows.Forms;
using Informatix.MGDS;

namespace ToggleSetEdit
{
    static class Program
    {
        const int DefaultTimeoutMs = 5 * 1000;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                using (var c = new Conversation())
                {
                    var sessionID = args.Length > 0 ? Convert.ToInt32(args[0]) : Conversation.AnySession;
                    c.Start(sessionID, DefaultTimeoutMs);
                    try
                    {
                        Cad.InfoBarButton(InfoBar.SetEdit, !Cad.GetInfoBarButton(InfoBar.SetEdit));
                    }
                    catch (ApiException ex)
                    {
                        if (ex.ErrorOccurred(AppErrorType.MGDS, AppError.NoSetEdit))
                        {
                            return; // ignore this exception.
                        }
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.GetType().FullName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
