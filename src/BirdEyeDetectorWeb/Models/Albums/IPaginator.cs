namespace BirdEyeDetector.Models.Album
{
    public interface IPaginator
    {
        string Name { get; }

        string Link { get; }

        IPaginator Next { get; }

        IPaginator Previous { get; }
    }
}