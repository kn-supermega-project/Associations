﻿using Associations.Common.DTOs;
using Associations.DataAccess.Entity;
using AssociationsAPI.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Associations.BusinessLogic.MappingProfiles
{
    public class WordRelsProfile : Profile
    {
        public WordRelsProfile()
        {
            CreateMap<WordRels, WordRels>()
            .ForMember(d => d.Id, o => o.Ignore()); // Don't Map Id because It is useless for Ids when updating

            CreateMap<WordRels, WordRelsDTO>();

            CreateMap<WordRelsDTO, WordRels>();
        }
    }
}
