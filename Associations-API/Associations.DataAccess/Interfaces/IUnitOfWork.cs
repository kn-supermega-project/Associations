using Associations.DataAccess.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Associations.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IWordsRepository WordsRepository { get; }
        IWordRelsRepository WordRelsRepository { get; }

        Task<bool> SaveAllAsync();
    }
}
