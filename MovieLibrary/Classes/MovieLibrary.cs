namespace MovieLibrary.Classes;

public class MovieLibrary : Library<Movie>
{
    private List<Movie> movieList;

    public List<Movie> MovieList => movieList;

    public MovieLibrary(List<Movie> movieList)
    {
        this.movieList = movieList;
    }

    public override void addMedia(Movie movie)
    {
        this.movieList.Add(movie);
    }

    public override string listMedia()
    {
        String listedMovies = "Movie ID: Movie Title";
        
        foreach (var movie in movieList)
        {
            listedMovies += $"\n{movie.MovieId}: {movie.Title}";
        }

        return listedMovies;
    }
    
    public string listMedia(int quantity)
    {
        String listedMovies = "Movie ID: Movie Title";
        
        for (var i = 0; i < quantity; i++)
        {
            listedMovies += $"\n{MovieList[i].MovieId}: {MovieList[i].Title}";
        }

        return listedMovies;
    }
}