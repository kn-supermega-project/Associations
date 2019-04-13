using Associations.DataAccess.Data;
using Associations.DataAccess.Entity;
using Associations.DataAccess.Interfaces.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Associations.DataAccess.Repositories
{
    public class WordsRepository : Repository<Words, int>, IWordsRepository
    {
        public WordsRepository(AssociationsDbContext context, IMapper mapper)
                    : base(context, mapper)
        {
        }
    }
}
