namespace NelsonHub.Shared.DAL.Models
{
    public enum ReferenceCategory
    {
        Programming,
        C_Sharp,
        Blazor,
        RaspberryPi,
        Other,
        DotNetCore

    }
    public class ReferenceModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public ReferenceCategory Category { get; set; }
    }
}
