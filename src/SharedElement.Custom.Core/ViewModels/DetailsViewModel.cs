using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using SharedElement.Custom.Core.Navigation;

namespace SharedElement.Custom.Core.ViewModels
{
    public class DetailsViewModel : MvxViewModel<GoToDetailsParameters>
    {
        public override Task Initialize(GoToDetailsParameters parameter)
        {
            Title = parameter.Title;

            return base.Initialize();
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
