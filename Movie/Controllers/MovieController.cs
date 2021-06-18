using Movie_store.DTO;
using Movie_store.Filters;
using Movie_store.ProviderInterface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Movie_store.Common;

namespace Movie_store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieProvider _MovieProvider;
        public MovieController(IMovieProvider MovieProvider)
        {
            _MovieProvider = MovieProvider;
        }

        [AuthorizationFilter]
        [HttpPost("FetchMovieList")]
        public async Task<MovieResponseDTO> FetchMovieList(MovieRequestDTO request)
        {
            if (request.IsNull() || string.IsNullOrEmpty(request.AccessToken))
                return new MovieResponseDTO { ErrorMessage = ErrorConstant.InValidRequest };

            return await _MovieProvider.FetchMovieList(request);
        }

        [AuthorizationFilter]
        [HttpPost("FetchMovieDetail")]
        public async Task<MovieDetailResponseDTO> FetchMovieDetails(MovieDetailRequestDTO request)
        {
            if (request.IsNull() || string.IsNullOrEmpty(request.AccessToken))
                return new MovieDetailResponseDTO { ErrorMessage = ErrorConstant.InValidRequest };

            return await _MovieProvider.FetchMovieDetails(request);
        }
    }
}
