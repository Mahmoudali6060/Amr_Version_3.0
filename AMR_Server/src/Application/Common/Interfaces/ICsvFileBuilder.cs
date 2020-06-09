using AMR_Server.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace AMR_Server.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
