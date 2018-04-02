using MvvmCross.Core.ViewModels;
using SharedElement.Presenter.Core.Navigation;

namespace SharedElement.Presenter.Core.ViewModels
{
    public class DetailsViewModel : MvxViewModel<GoToDetailsParameters>
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
