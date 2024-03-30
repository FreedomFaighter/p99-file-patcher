using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HelloWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        readonly string strHello = "Hello";
        readonly string strWorld = "World";
        private HelloWorldViewModel HelloWorldVM { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            HelloWorldVM = new HelloWorldViewModel();
        }

        private void Show_Hello_World_Click(object sender, RoutedEventArgs e)
        {
            HelloWorldVM.HelloWorldMessage = $"{strHello} {strWorld}";
            HelloWorldVM.MessageDisplayed = true;
        }

        private void Clear_Text_Click(object sender, RoutedEventArgs e)
        {
            HelloWorldVM.HelloWorldMessage = String.Empty;
            HelloWorldVM.MessageDisplayed = false;
        }

        private bool NegateBool(bool TFValue)
        {
            return !TFValue;
        }
    }
}
