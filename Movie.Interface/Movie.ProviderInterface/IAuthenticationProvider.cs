using Movie_store.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie_store.ProviderInterface
{
   public interface IAuthenticationProvider
    {
        Task<Bearer> GenToken(AuthenticationDTORequest request);

        Task<UserDetailResponseDTO> GetUserDetail(string accessToken);

    }
}
