using System.Windows;

namespace WPF
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public static readonly DependencyProperty SizeProperty =
      DependencyProperty.Register("Size", typeof (int), typeof (MainWindow), new PropertyMetadata(0));


    public MainWindow()
    {
      InitializeComponent();
    }

    public int Size
    {
      get { return (int) GetValue(SizeProperty); }
      set { SetValue(SizeProperty, value); }
    }
  }
}