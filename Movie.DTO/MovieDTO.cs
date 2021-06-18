using System;
using System.Collections.Generic;
using System.Text;

namespace Movie_store.DTO
{
    public class MovieDetailDTO
    {
        public List<MovieMainDTO> Movies { get; set; }
    }

    public class MovieDetailResponseDTO : BaseResponseDTO
    {
        public MovieMainDTO MovieDetail { get; set; }
    }
    public class MovieMainDTO : MovieDTO
    {
        public string Plot { get; set; }
        public string Poster { get; set; }
        public List<string> SoundEffects { get; set; }
        public List<string> Stills { get; set; }
        public string ImdbID { get; set; }
        public string ListingType { get; set; }
    }
    public class MovieDTO
    {
        public string Language { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public string ImdbRating { get; set; }
    }

    public class MovieResponseDTO : BaseResponseDTO
    {
        public List<MovieDTO> Movies { get; set; }
    }

    public class MovieRequestDTO : BaseRequestDTO
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public string Location { get; set; }
        public OrderBy SortBy { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class MovieDetailRequestDTO : BaseRequestDTO
    {
        public string Title { get; set; }
    }
    public enum OrderBy
    {
        ASCENDING,
        DESCENDING
    }
}
