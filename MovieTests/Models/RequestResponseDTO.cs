using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Movie_storeTests.Models
{
    public class BaseResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
    public class Bearer : BaseResponseDTO
    {
        public string AccessToken { get; set; }
    }

    public class AuthenticationDTORequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class UserDetailResponseDTO : BaseResponseDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public List<RoleForBearerDTO> Roles { get; set; }
    }

    public class RoleForBearerDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserDetailRequestDTO
    {
        public string AccessToken { get; set; }
    }

    public class BaseRequestDTO
    {
        public string AccessToken { get; set; }
    }
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
