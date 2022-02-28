
using Microsoft.Extensions.Logging;
using MovieLibrary.Classes.Daos;

namespace MovieLibrary.Classes;

class Program
{
    static void Main(string[] args)
    {
        ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.CreateLogger<Program>();
            List<Movie> movieList = new List<Movie>();
            MovieLibrary movieLibrary = new MovieLibrary(movieList);
            //Automatically imports movie library from movies.csv
            var streamReader = new StreamReader(new FileStream("ml-latest-small/movies.csv", FileMode.Open));
            MovieFileReader movieFileReader = new MovieFileReader(streamReader, movieLibrary);
            movieFileReader.ReadToLibrary();
            streamReader.Close();
            //Console.WriteLine(movieLibrary.listMedia(50)); //For testing the listing of movies
            var loopControl = true;
            while (loopControl)
            {
                Console.Write("Hello, and welcome to Sam's movie library!\n" +
                              "Would you like to list the movies already existing in the library, or add movies?" +
                              "\nEnter (1) to list current movies, and (2) to add a movie to the library, or (x) to exit the program: ");
                var choice = Convert.ToString(Console.ReadLine());

                while (choice is null || !(choice.Equals("1") || choice.Equals("2") || choice.ToUpper().Equals("X")))
                {
                    Console.Write("Looks like you entered an invalid input.\n" +
                                  "Please try again, making sure to only enter (1) to list current movies, (2) to add a movie to the library, or (x) to exit the program: ");
                    choice = Convert.ToString(Console.ReadLine);
                }
            
                switch (choice)
                {
                    case "1":
                        ListMovies(movieLibrary);
                        break;
                    case "2":
                        AddMovie(movieLibrary);
                        break;
                    case "x":
                        loopControl = false;
                        break;
                }
            }

            Console.WriteLine("Writing to file");
            StreamWriter streamWriter = new StreamWriter(new FileStream("ml-latest-small/movies.csv", FileMode.Create));
            MovieFileWriter movieFileWriter = new MovieFileWriter(streamWriter, movieLibrary);
            movieFileWriter.Write();
            streamWriter.Close();
    }

    static void AddMovie(MovieLibrary library)
    {
        int newId = library.MovieList[^1].MovieId + 1;

        Console.Write("Please enter the year the movie was released: ");
        var releaseYear = Console.ReadLine();

        Console.Write("Please enter the movie title: ");
        var newTitle = Convert.ToString(Console.ReadLine());
        
        while (newTitle.Length <= 0)
        {
            Console.Write("Looks like you left the title blank! Please try again: ");
            newTitle = Convert.ToString(Console.ReadLine());
        }

        newTitle = $"{newTitle} ({releaseYear})";

        foreach (var movie in library.MovieList)
        {
            if (newTitle.Equals(movie.Title))
            {
                Console.Write("It looks like that movie already exists in this library!\n" +
                              "The program will now exit to the main menu, in order to add a different movie simply select to add another movie in the menu.");
                return;
            }
        }
        
        Console.Write("Please enter all the movie's genres (when done entering, enter (x) to exit): ");
        var genreList = new List<string>();

        while(true)
        {
            var currentGenre = Convert.ToString(Console.ReadLine());
            while (currentGenre is null)
            {
                Console.Write("Looks like you left a blank genre! Please try again: ");
                currentGenre = Convert.ToString(Console.ReadLine());
            }

            if (currentGenre.ToLower().Equals("x"))
            {
                break;
            }

            genreList.Add(currentGenre);
        }
        library.addMedia(new Movie(newId, newTitle, genreList));
    }

    static void ListMovies(MovieLibrary library)
    {
        Console.WriteLine(library.listMedia(100)); //
    }
}