using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using Android.App;

namespace Todo.Models;
internal class AllTodos
{
    public ObservableCollection<Todo> Todos { get; set; } = new ObservableCollection<Todo>();

    public AllTodos() => LoadTodos();

    public void LoadTodos()
    {
        Todos.Clear();

        string appDataPath = FileSystem.AppDataDirectory;

        IEnumerable<Todo> todos = Directory
            .EnumerateFiles(appDataPath, "*.todo-text.txt")
            .Select(filename =>
            {
                TodoStates state = TodoStates.Todo;
                if (File.Exists(filename.Split(".todo-text.txt").First() + ".todo-state.txt"))
                {
                    var val = File.ReadAllText(filename.Split(".todo-text.txt").First() + ".todo-state.txt");
                    int.TryParse(val, out var index);
                    state = (TodoStates)index;
                }

                var dt = new Todo
                {
                    Path = filename,
                    Title = filename,
                    Description = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename),
                    State = state,
                };

                return dt;
            })
            .OrderBy(todo => todo.Title);

        foreach (Todo todo in todos)
            Todos.Add(todo);
    }
}
