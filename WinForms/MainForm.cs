using System.Windows.Forms;

namespace WinForms
{
  public partial class MainForm : Form
  {
    // creates the main form
    public MainForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, System.EventArgs e)
    {
      ((Control) sender).Visible = false;
    }
  }          
}
