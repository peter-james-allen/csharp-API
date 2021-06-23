using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Movie
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Search for a Movie:");
            string searchTerm = Console.ReadLine();
            var searchResults = SearchMovies(searchTerm);
            MovieSearchResults movieJson = JsonConvert.DeserializeObject<MovieSearchResults>(searchResults);
            if (movieJson.Response)
            {
                foreach (Movie movie in movieJson.Search)
                {
                    Console.WriteLine($"Title: {movie.Title}\nYear: {movie.Year}\n");
                    //Console.WriteLine($"Year: {movie.Year}");
                }
            }

            //Movie jaws = new Movie(title: "Jaws", year: 1975, imdbID: "tt0073195", type: "movie", poster: "https://m.media-amazon.com/images/M/MV5BMmVmODY1MzEtYTMwZC00MzNhLWFkNDMtZjAwM2EwODUxZTA5XkEyXkFqcGdeQXVyNTAyODkwOQ@@._V1_SX300.jpg");

            //string json = JsonConvert.SerializeObject(jaws);

            //Movie readMovie = JsonConvert.DeserializeObject<Movie>(json);
        }

        static string SearchMovies(string title)
        {
            const string URL = "https://www.omdbapi.com/";
            const string apiKey = "&apikey=23bb5b7a";

            using var client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            string searchParams = "?s=" + title + apiKey;

            var content = client.GetStringAsync(URL + searchParams);

            return content.Result;
        }
    }

    public class Movie
    {
        public string Title { get; }
        public int Year { get; }
        public string ImdbID { get; }
        public string Type { get; }
        public string Poster { get; }

        public Movie(string title, int year, string imdbID, string type, string poster)
        {
            Title = title;
            Year = year;
            ImdbID = imdbID;
            Type = type;
            Poster = poster;
        }
    }

    public class MovieSearchResults
    {
        public IList<Movie> Search { get; }
        public string TotalResults { get; }
        public bool Response { get; }

        public MovieSearchResults(IList<Movie> search, string totalResults, bool response)
        {
            Search = search;
            TotalResults = totalResults;
            Response = response;
        }
    }
}
