using AutoMapper;
using Quality.Common;
using Quality.Common.Dto.Genre;
using Quality.DataAccess;
using Quality.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quality.Service
{
    public class GenreService : IGenreService
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreService(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Genre>> All()
        {
            return (await _dbContext.Genre.All()).ToList();
        }

        public async Task<Result> Create(NewGenreDto data)
        {
            var result = new Result();

            if (string.IsNullOrWhiteSpace(data.Name))
            {
                result.IsSuccess = false;
                result.Message = "Genre name is required";
            }
            else
            {
                var genre = _mapper.Map<Genre>(data);
                result = await _dbContext.Genre.Insert(genre);
                if (result.IsSuccess) result.Message = "Genre added successfully!";
            }

            return result;
        }
    }
}
