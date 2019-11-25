using Moq;
using Quality.Common;
using Quality.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quality.Test.DataAccess
{

    public class MockGenericDataAccess<T> where T : class, new()
    {
        public readonly IGenericBase<T> DataAccess;
        private List<T> _data;
        private readonly Mock<IGenericBase<T>> _moqDataAccess = new Mock<IGenericBase<T>>();

        public MockGenericDataAccess(IEnumerable<T> data)
        {
            _data = data.ToList();
            _moqDataAccess.Setup(m => m.GetSingleFilteredByAsync(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns((Expression<Func<T, bool>> condition) => Task.FromResult(_data.FirstOrDefault(condition.Compile())));

            _moqDataAccess.Setup(m => m.All()).Returns(Task.FromResult(_data.AsEnumerable()));

            _moqDataAccess.Setup(m => m.Insert(It.IsAny<T>())).Returns((T forInsert) =>
            {
                _data.Add(forInsert);
                return Task.FromResult(new Result() { IsSuccess = true, Message = "Inserted" });
            });

            _moqDataAccess.Setup(m => m.GetAllFilteredBy(It.IsAny<Expression<Func<T, bool>>>()))
                .Returns((Expression<Func<T, bool>> condition) => Task.FromResult(_data.Where(condition.Compile()).ToList()));

            DataAccess = _moqDataAccess.Object;
        }
    }
}
