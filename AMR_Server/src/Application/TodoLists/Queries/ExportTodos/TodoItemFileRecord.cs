using AMR_Server.Application.Common.Mappings;
using AMR_Server.Domain.Entities;

namespace AMR_Server.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
