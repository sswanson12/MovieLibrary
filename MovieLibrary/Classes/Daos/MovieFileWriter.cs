using MovieLibrary.Interfaces;

namespace MovieLibrary.Classes.Daos;

public class MovieFileWriter
{
    private StreamWriter _streamWriter;

    private MovieLibrary _movieLibrary;

    public MovieFileWriter(StreamWriter streamWriter, MovieLibrary movieLibrary)
    {
        _streamWriter = streamWriter;
        _movieLibrary = movieLibrary;
    }

    public void Write()
    {
        String currentLine = $"movieId,title,genres";
        _streamWriter.WriteLine(currentLine);
        foreach (var movie in _movieLibrary.MovieList)
        {
            currentLine = $"{movie.MovieId},{movie.Title},";
            foreach (var genre in movie.Genres)
            {
                currentLine += $"{genre}|";
            }

            currentLine = currentLine.Substring(0, currentLine.Length - 1);
            _streamWriter.WriteLine(currentLine);
        }
    }
}