using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NeosVrCache
{
    public class ControlWriter : TextWriter
    {
        private readonly TextBox textbox;

        public ControlWriter(TextBox textbox)
        {
            this.textbox = textbox;
        }

        public override Encoding Encoding => Encoding.ASCII;

        public override void Write(char value)
        {
            textbox.Text += value;
            textbox.SelectionStart = textbox.Text.Length;
            textbox.ScrollToCaret();
        }

        public override void Write(string value)
        {
            textbox.Text += value;
            textbox.SelectionStart = textbox.Text.Length;
            textbox.ScrollToCaret();
        }
    }
}