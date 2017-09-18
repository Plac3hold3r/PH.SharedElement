using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Placeholder.SharedElement.Core.Navigation;

namespace Placeholder.SharedElement.Core.ViewModels
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
