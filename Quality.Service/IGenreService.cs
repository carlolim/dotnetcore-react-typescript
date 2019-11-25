using System.Collections.Generic;
using System.Threading.Tasks;
using Quality.Common;
using Quality.Common.Dto.Genre;
using Quality.DataAccess.Entities;

namespace Quality.Service
{
    public interface IGenreService
    {
        Task<Result> Create(NewGenreDto data);
        Task<List<Genre>> All();
    }
}