using AssociationsAPI.DTOs;
using AssociationsAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationsAPI.Helpers
{
    public class AutoMappersProfiles: Profile
    {
        public AutoMappersProfiles()
        {
            CreateMap<Words, WordsDTO>();
            CreateMap<WordsRel, WordsRelDTO>();
        }
    }
}
