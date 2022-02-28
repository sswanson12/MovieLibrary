namespace MovieLibrary.Classes.Daos;

public class MovieFileReader
{
    private StreamReader _streamReader;
    
    private List<Movie> movieList;

    public MovieFileReader(StreamReader streamReader, MovieLibrary movieLibrary)
    {
        _streamReader = streamReader;
        this.movieList = movieLibrary.MovieList;
    }

    public void ReadToLibrary()
    {
        _streamReader.ReadLine(); //Skips first line of file (movieId,title,genres)

        //This is the one thing so far that I've had trouble removing dependencies from...
        while (!_streamReader.EndOfStream)
        {
            var currentLine = _streamReader.ReadLine() ?? throw new InvalidOperationException();

            var lineSplitUp = currentLine.Split(',');

            var genresSplitUp = lineSplitUp[2].Split('|');

            List<string> genresList = new List<string>();

            foreach (var genre in genresSplitUp)
            {
                genresList.Add(genre);
            }

            movieList.Add(new Movie(Convert.ToInt32(lineSplitUp[0]), lineSplitUp[1], genresList));
        }
    }
}