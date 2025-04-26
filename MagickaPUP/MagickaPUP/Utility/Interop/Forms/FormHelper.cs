using System;
using System.Windows.Forms;

namespace MagickaPUP.Utility.Interop.Forms
{
    public static class FormHelper
    {
        public static Form Create()
        {
            var dummyForm = new Form
            {
                Width = 1,
                Height = 1,
                ShowInTaskbar = false,
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                Location = new System.Drawing.Point(-32000, -32000) // I HATE MICROSOFT!!! FUCK YOU!!!
            };
            dummyForm.Show();
            return dummyForm;
        }

        public static void Release(Form dummyForm)
        {
            dummyForm.Close();
        }
    }
}
