namespace SpaceDefence.Objectives
{
    public abstract class Objective
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string IconSpriteName { get; set; }

        public Objective(string title, string subtitle, string iconSpriteName)
        {
            Title = title;
            Subtitle = subtitle;
            IconSpriteName = iconSpriteName;
        }

        public abstract void OnComplete();
    }
}
