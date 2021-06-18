using Microsoft.VisualStudio.TestTools.UnitTesting;
using Movie_storeTests;
using Movie_store.Common;
using Movie_storeTests.Models;

namespace Movie_store.Controllers.Tests
{
    [TestClass()]
    public class TestingControllerTests
    {

        [TestMethod()] 
        public void GetGenTokenTest()
        {
            Bearer bearer = TestUtils.GenToken();
            Assert.IsTrue(bearer.IsNotNull() && bearer.IsSuccess && !string.IsNullOrEmpty(bearer.AccessToken));
        }

        [TestMethod()]
        public void GetUserDetailTest()
        {
            UserDetailResponseDTO result = TestUtils.GetUserDetail();
            Assert.IsTrue(result.IsNotNull() && result.IsSuccess);
        }

        [TestMethod()]
        public void FetchMovieListTest()
        {
            MovieResponseDTO result = TestUtils.FetchMovieListTest();
            Assert.IsTrue(result.IsNotNull() && result.IsSuccess && result.Movies.HasRecords());
        }

        [TestMethod()]
        public void FetchMovieDetailTest()
        {
            MovieDetailResponseDTO result = TestUtils.FetchMovieDetailTest();
            Assert.IsTrue(result.IsNotNull() && result.IsSuccess && result.MovieDetail.IsNotNull());
        }
    }


    public class TestUtils
    {
        public static Bearer GenToken()
        {
            AuthenticationDTORequest model = new AuthenticationDTORequest()
            {
                UserName = "regular@gmail.com",
                Password = "Welcome@321"
            };
            return RestServiceUtils.MakeRestCall<AuthenticationDTORequest, Bearer>(model, "Authentication/genToken", Constant.URL);
        }

        public static UserDetailResponseDTO GetUserDetail()
        {
            Bearer bearer = GenToken();
            if (bearer.IsNotNull() && bearer.IsSuccess)
            {
                UserDetailRequestDTO model = new UserDetailRequestDTO()
                {
                    AccessToken = bearer.AccessToken
                };
                return RestServiceUtils.MakeRestCall<UserDetailRequestDTO, UserDetailResponseDTO>(model, "Authentication/userDetail", Constant.URL);
            }
            return null;
        }

        public static MovieResponseDTO FetchMovieListTest()
        {
            Bearer bearer = GenToken();
            if (bearer.IsNotNull() && bearer.IsSuccess)
            {
                MovieRequestDTO model = new MovieRequestDTO()
                {
                    AccessToken = bearer.AccessToken
                };
                return RestServiceUtils.MakeRestCall<MovieRequestDTO, MovieResponseDTO>(model, "Movie/FetchMovieList", Constant.URL);
            }
            return null;
        }

        public static MovieDetailResponseDTO FetchMovieDetailTest()
        {
            Bearer bearer = GenToken();
            MovieResponseDTO result = TestUtils.FetchMovieListTest();
            if (bearer.IsNotNull() && bearer.IsSuccess && result.IsNotNull() && result.Movies.HasRecords())
            {
                MovieDetailRequestDTO model = new MovieDetailRequestDTO()
                {
                    AccessToken = bearer.AccessToken,
                    Title = result.Movies[0].Title
                };
                return RestServiceUtils.MakeRestCall<MovieDetailRequestDTO, MovieDetailResponseDTO>(model, "Movie/FetchMovieDetail", Constant.URL);
            }
            return null;
        }
    }
}