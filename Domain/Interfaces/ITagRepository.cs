using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Objects;

namespace Domain.Interfaces
{
    public interface ITagRepository
    {
        /// <summary>
        /// Persist Tag into the Database
        /// </summary>
        /// <param name="tag">Tag to persist</param>
        /// <returns></returns>
        Task PersistAsync(Tag tag);

        /// <summary>
        /// Return all Tags from Database
        /// </summary>
        /// <returns></returns>
        List<Tag> GetAll();
    }
}
