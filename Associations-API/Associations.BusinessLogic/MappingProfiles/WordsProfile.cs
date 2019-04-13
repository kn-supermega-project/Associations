using Associations.Common.DTOs;
using Associations.DataAccess.Entity;
using AssociationsAPI.DTOs;
using AutoMapper;

namespace Associations.BusinessLogic.MappingProfiles
{
    public class WordsProfile : Profile
    {
        public WordsProfile()
        {
            CreateMap<Words, Words>()
            .ForMember(d => d.Id, o => o.Ignore()); // Don't Map Id because It is useless for Ids when updating

            CreateMap<Words, WordsDTO>();
            CreateMap<Words, WordsToListDTO>();

            CreateMap<WordsDTO, Words>();
        }
    }
}
