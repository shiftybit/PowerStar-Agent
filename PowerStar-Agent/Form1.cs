using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerStar_Agent
{
	public partial class Form1 : Form
	{
		delegate void WriteLineDelegate(string text);
		public Form1()
		{
			InitializeComponent();
		}

		public void WriteLine(string text)
		{
			if (!textBox1.InvokeRequired)
				textBox1.Text += text + "\r\n";
			else
			{
				WriteLineDelegate d = new WriteLineDelegate(WriteLine);
				this.Invoke(d, new object[] { text });
			}
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			Agent.Initialize(WriteLine);
			var output = Agent.GetFirstPowerShell();
			WriteLine(output);
			textBox2.Focus();
		}

		private void TextBox1_TextChanged(object sender, EventArgs e)
		{

			textBox1.SelectionStart = textBox1.Text.Length;
			textBox1.ScrollToCaret();
		}
		private void RunCurrentText()
		{
			var script = textBox2.Text.ToString();
			textBox2.Clear();
			WriteLine("PS> " + script);
			WriteLine(Agent.RunString(script));
		}
		private void Button1_Click(object sender, EventArgs e)
		{
			RunCurrentText();
		}

		private void TextBox2_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				RunCurrentText();
		}
	}
}
