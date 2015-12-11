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
                    var sessionID = Convert.ToInt32(args[0]);
                    c.Start(sessionID, DefaultTimeoutMs);
                    Cad.InfoBarButton(InfoBar.SetEdit, !Cad.GetInfoBarButton(InfoBar.SetEdit));
                }
            }
            catch (ApiException ex)
            {
                if (!ex.ErrorOccurred(AppErrorType.MGDS, AppError.NoSetEdit)) MessageBox.Show(ex.ApiFunction, ex.Message);
            }
            catch
            {
                MessageBox.Show("なんらかの例外が発生しました");
            }
        }
    }
}
