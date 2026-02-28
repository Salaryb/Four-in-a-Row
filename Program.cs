using System.Windows.Forms;

namespace Ex_05
{
    public class Program
    {
        public static void Main()
        {
            FormGameSettings settingsForm = new FormGameSettings();

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                FormGame gameForm = new FormGame(settingsForm);
                gameForm.ShowDialog();
            }
        }
    }
}