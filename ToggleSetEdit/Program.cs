using System;
using System.Windows.Forms;
using Informatix.MGDS;

namespace ToggleSetEdit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using (var c = new Conversation())
                {
                    c.Start();
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
