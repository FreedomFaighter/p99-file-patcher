using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HelloWorld
{
    [Bindable(BindableSupport.Yes)]
    public class HelloWorldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public HelloWorldViewModel()
        {
            helloWorldMessage = String.Empty;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private string helloWorldMessage;

        private bool messageDisplayed;

        public string HelloWorldMessage { get => helloWorldMessage; set => SetProperty(ref helloWorldMessage, value); }

        public bool MessageDisplayed { get => messageDisplayed; set => SetProperty(ref messageDisplayed, value); }
    }
}
