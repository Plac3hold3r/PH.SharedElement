using MvvmCross.Core.ViewModels;
using SharedElement.Core.Navigation;
using SharedElement.Core.Utils;

namespace SharedElement.Core.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        public override void Start()
        {
            Items = new MvxObservableCollection<ListItemViewModel>
            {
                new ListItemViewModel { Title = "title one" },
                new ListItemViewModel { Title = "title two" },
                new ListItemViewModel { Title = "title three" },
                new ListItemViewModel { Title = "title four" },
                new ListItemViewModel { Title = "title five" }
            };
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

            ShowViewModel<DetailsViewModel>(
                parameterValuesObject: GoToDetailsParameters.Create(SelectedItem.Title),
                presentationBundle: MvxBundleUtils.CreateTransitionPresentationBundle(transitionImageTag, transitionTextTag));
        }
    }
}
