using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp12
{
    

    public class MainViewModel : INotifyPropertyChanged
    {
        private string _agent = string.Empty;
        private string _agent2 = string.Empty;
        private Random _random = new Random();
        private List<Smoker> _smokers;
        private CancellationTokenSource _cancellationTokenSource;

        public string Agent
        {
            get { return _agent; }
            set
            {
                _agent = value;
                OnPropertyChanged("Agent");
            }
        }

        public string Agent2
        {
            get { return _agent2; }
            set
            {
                _agent2 = value;
                OnPropertyChanged("Agent2");
            }
        }

        public ICommand StartCommand { get; set; }

        public MainViewModel()
        {
            _smokers = new List<Smoker>
            {
                new Smoker(Component.Tobacco, "Курильщик с табаком"),
                new Smoker(Component.Paper, "Курильщик с бумагой"),
                new Smoker(Component.Matches, "Курильщик со спичками")
            };

            StartCommand = new RelayCommand(Start);
        }

        private async void Start(object? parameter)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Run(() => RunProcess(_cancellationTokenSource.Token));
        }

        private void RunProcess(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var components = Enum.GetValues(typeof(Component)).Cast<Component>().OrderBy(x => _random.Next()).Take(2).ToList();
                Agent = $"Посредник кладет на стол: {components[0]} и {components[1]}";

                var smoker = _smokers.FirstOrDefault(s => !components.Contains(s.Component));
                if (smoker != null)
                {
                    Agent2 = $"{smoker.Name} скручивает сигарету и курит";
                }

                Thread.Sleep(4000);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
