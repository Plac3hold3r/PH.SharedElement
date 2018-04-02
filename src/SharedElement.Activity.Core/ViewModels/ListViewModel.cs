using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using SharedElement.Activity.Core.Navigation;
using SharedElement.Activity.Core.Utils;

namespace SharedElement.Activity.Core.ViewModels
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
                new ListItemViewModel { Id = 1, Title = "title one Fragment" },
                new ListItemViewModel { Id = 2, Title = "title two Activity" },
                new ListItemViewModel { Id = 3, Title = "title three Fragment" },
                new ListItemViewModel { Id = 4, Title = "title four Activity" },
                new ListItemViewModel { Id = 5, Title = "title five Fragment" }
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

        public void SelectItemExecution(ListItemViewModel item, params string[] transitionsTags)
        {
            SelectedItem = item;

            if (item.Id % 2 == 0)
            {
                _navigationService.Navigate<DetailsViewModel, GoToDetailsParameters>(
                    param: GoToDetailsParameters.Create(SelectedItem.Title),
                    presentationBundle: MvxBundleUtils.CreateTransitionPresentationBundle(transitionsTags));
            }
            else
            {
                _navigationService.Navigate<DetailFragmentViewModel, GoToDetailsParameters>(
                    param: GoToDetailsParameters.Create(SelectedItem.Title),
                    presentationBundle: MvxBundleUtils.CreateTransitionPresentationBundle(transitionsTags));
            }
        }
    }
}
