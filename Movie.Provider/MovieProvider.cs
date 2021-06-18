using Microsoft.AspNetCore.Hosting;
using Movie_store.Common;
using Movie_store.DTO;
using Movie_store.ProviderInterface;
using Movie_store.RepositoryInterface;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Movie_store.Provider
{
    public class MovieProvider : IMovieProvider
    {
        private readonly IHostingEnvironment _environment;

        public MovieProvider(IServiceProvider servicesProvider)
        {
            _environment = servicesProvider.GetService<IHostingEnvironment>();
        }

        public async Task<MovieDetailResponseDTO> FetchMovieDetails(MovieDetailRequestDTO request)
        {
            if (request.IsNull() || string.IsNullOrEmpty(request.Title))
                return new MovieDetailResponseDTO() { ErrorMessage = ErrorConstant.InValidRequest };

            MovieDetailDTO MovieItems = null;
            MovieDetailResponseDTO resp = new MovieDetailResponseDTO();
            using (StreamReader r = new StreamReader(Path.Combine(_environment.ContentRootPath, "Files//movies.json")))
            {
                string json = r.ReadToEnd();
                MovieItems = ExtensionMethod.DeserializeObject<MovieDetailDTO>(json);
            }
            if (MovieItems.IsNotNull() && MovieItems.Movies.HasRecords())
            {
                MovieMainDTO movie = MovieItems.Movies.Where(x => x.Title.ToLower() == request.Title.ToLower()).FirstOrDefault();
                if (movie.IsNotNull())
                {
                    resp.MovieDetail = movie;
                    resp.IsSuccess = true;
                    resp.HttpStatusCode = HttpStatusCode.OK;
                }
            }
            else
                resp.ErrorMessage = ErrorConstant.NoDataFound;
            return resp;
        }

        public async Task<MovieResponseDTO> FetchMovieList(MovieRequestDTO request)
        {
            MovieResponseDTO MovieItems = null;
            if (request.IsNotNull())
            {
                request.PageNo = request.PageNo == default ? 1 : request.PageNo;
                request.PageSize = request.PageSize == default ? 10 : request.PageSize;
            }

            using (StreamReader r = new StreamReader(Path.Combine(_environment.ContentRootPath, "Files//movies.json")))
            {
                string json = r.ReadToEnd();
                MovieItems = ExtensionMethod.DeserializeObject<MovieResponseDTO>(json);
            }

            if (MovieItems.IsNotNull() && MovieItems.Movies.HasRecords())
            {
                MovieItems.IsSuccess = true;
                MovieItems.HttpStatusCode = HttpStatusCode.OK;
            }
            if (!string.IsNullOrEmpty(request.Title))
                MovieItems.Movies = MovieItems.Movies.Where(x => x.Title.ToLower().Contains(request.Title.ToLower())).ToList();

            if (!string.IsNullOrEmpty(request.Language))
                MovieItems.Movies = MovieItems.Movies.Where(x => x.Language.ToLower().Contains(request.Language.ToLower())).ToList();

            if (!string.IsNullOrEmpty(request.Location))
                MovieItems.Movies = MovieItems.Movies.Where(x => x.Location.ToLower().Contains(request.Location.ToLower())).ToList();

            if (request.SortBy == OrderBy.DESCENDING)
                MovieItems.Movies = MovieItems.Movies.OrderByDescending(x => x.Title).ToList();

            MovieItems.Movies = MovieItems.Movies.Skip((request.PageNo - 1) * request.PageSize).Take(request.PageSize).ToList();
            return MovieItems;
        } 
    }
}
