using MvvmCross.Core.ViewModels;
using SharedElement.Official.Core.Navigation;

namespace SharedElement.Official.Core.ViewModels
{
    public class DetailFragmentViewModel : MvxViewModel<GoToDetailsParameters>
    {
        public override void Prepare(GoToDetailsParameters parameter)
        {
            Title = parameter.Title;
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
