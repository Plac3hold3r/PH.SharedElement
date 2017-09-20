namespace SharedElement.Custom.Core.Navigation
{
    public class GoToDetailsParameters
    {
        public string Title { get; set; }

        public static GoToDetailsParameters Create(string title)
        {
            return new GoToDetailsParameters { Title = title };
        }
    }
}
