using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace p99FileUpdater
{
    [Bindable(BindableSupport.Yes)]
    public class HelloWorldViewModel : INotifyPropertyChanged
    {
        private p99FileUpdaterView p99fuv = new p99FileUpdaterView();

        public ICommand SetHelloWorldCommand { get; }
        public ICommand SetTextBoxClear { get; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public HelloWorldViewModel()
        {
            SetHelloWorldCommand = new RelayCommand.RelayCommand(() =>
                {
                    HelloWorldMessage = $"{ p99fuv.strHello} {p99fuv.strWorld}";
                    MessageDisplayed = true;
                });
            SetTextBoxClear = new RelayCommand.RelayCommand(() =>
                {
                    HelloWorldMessage = String.Empty;
                    MessageDisplayed = false;
                });
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
