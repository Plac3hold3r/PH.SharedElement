using MvvmCross.Core.ViewModels;
using SharedElement.Core.Navigation;
using System.Threading.Tasks;

namespace SharedElement.Core.ViewModels
{
    public class DetailsViewModel : MvxViewModel<GoToDetailsParameters>
    {
        public override Task Initialize(GoToDetailsParameters parameter)
        {
            Title = parameter.Title;

            return Task.FromResult(true);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
