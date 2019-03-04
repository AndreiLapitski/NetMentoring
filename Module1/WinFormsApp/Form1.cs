using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelAnswer.Text = $@"Hello, {textBoxName.Text}!";
            textBoxName.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labelAnswer.Text = SpeakerClassLibrary.Speaker.SayHelloNow(textBoxName2.Text);
            textBoxName2.Clear();
        }
    }
}
