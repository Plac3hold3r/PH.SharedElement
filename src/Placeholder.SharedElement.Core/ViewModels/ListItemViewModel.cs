namespace Placeholder.SharedElement.Core.ViewModels
{
    public class ListItemViewModel
    {
        public string Title { get; set; }

        public override string ToString()
            => Title;
    }
}
