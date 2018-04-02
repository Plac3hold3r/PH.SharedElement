using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using SharedElement.Activity.Core.Navigation;

namespace SharedElement.Activity.Core.ViewModels
{
    public class DetailFragmentViewModel : MvxViewModel<GoToDetailsParameters>
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
