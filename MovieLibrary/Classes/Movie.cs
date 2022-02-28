namespace MovieLibrary.Classes;

public class Movie
{
    private string _title;
    private List<String> _genres;

    public Movie(int movieId, string title, List<string> genres)
    {
        this.MovieId = movieId;
        this._title = title;
        this._genres = genres;
    }

    public int MovieId { get; set; }

    public string Title
    {
        get => _title;
        set => _title = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<string> Genres
    {
        get => _genres;
        set => _genres = value ?? throw new ArgumentNullException(nameof(value));
    }
}