using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Placeholder.SharedElement.Core.Navigation;
using Placeholder.SharedElement.Core.Utils;

namespace Placeholder.SharedElement.Core.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public ListViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task Initialize()
        {
            Items = new MvxObservableCollection<ListItemViewModel>
            {
                new ListItemViewModel { Id = 1, Title = "title one" },
                new ListItemViewModel { Id = 2, Title = "title two" },
                new ListItemViewModel { Id = 3, Title = "title three" },
                new ListItemViewModel { Id = 4, Title = "title four" },
                new ListItemViewModel { Id = 5, Title = "title five" }
            };

            return base.Initialize();
        }

        private MvxObservableCollection<ListItemViewModel> _items;
        public MvxObservableCollection<ListItemViewModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private ListItemViewModel _selectedItem;
        public ListItemViewModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public void SelectItemExecution(ListItemViewModel item, string transitionImageTag, string transitionTextTag)
        {
            SelectedItem = item;

            _navigationService.Navigate<DetailsViewModel, GoToDetailsParameters>(
                param: GoToDetailsParameters.Create(SelectedItem.Title),
                presentationBundle: MvxBundleUtils.CreateTransitionPresentationBundle(transitionImageTag, transitionTextTag));
        }
    }
}
