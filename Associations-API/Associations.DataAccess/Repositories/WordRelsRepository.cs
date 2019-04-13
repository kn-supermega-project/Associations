using Associations.DataAccess.Data;
using Associations.DataAccess.Entity;
using Associations.DataAccess.Interfaces.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Associations.DataAccess.Repositories
{
    public class WordRelsRepository : Repository<WordRels, int>, IWordRelsRepository
    {
        public WordRelsRepository(AssociationsDbContext context, IMapper mapper)
                    : base(context, mapper)
        {
        }
    }
}
