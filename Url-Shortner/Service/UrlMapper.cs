using AutoMapper;
using Url_Shortner.DataTransferObject;
using Url_Shortner.Models;

namespace Url_Shortner.Service
 
{
    public static class UrlMapper  
    {
        public static Url_Shortner.DataTransferObject.UrlPairDto ToUrlDto(this UrlPair urlPair)
        {
         

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UrlPair, UrlPairDto>();
            });
            IMapper iMapper = config.CreateMapper();

            var destination = iMapper.Map<UrlPair, UrlPairDto>(urlPair);
            return destination;

        }

    }


    }