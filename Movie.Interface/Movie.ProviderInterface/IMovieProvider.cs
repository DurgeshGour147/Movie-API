using System;
using System.Threading.Tasks;
using Movie_store.DTO;

namespace Movie_store.ProviderInterface
{
    public interface IMovieProvider
    {
        Task<MovieResponseDTO> FetchMovieList(MovieRequestDTO request);

        Task<MovieDetailResponseDTO> FetchMovieDetails(MovieDetailRequestDTO request);
    }
}
