using Todo.Models;

namespace Todo.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NewTodoPage : ContentPage
{
	public string ItemId {
		set { LoadTodo(value); }
	}

	public NewTodoPage()
	{
		InitializeComponent();

		statePicker.ItemsSource = Enum.GetNames(typeof(TodoStates)).ToList();
		statePicker.SelectedIndex = 0;
    }

	private List<string> statePicker_FactoryMethod()
	{
		return Enum.GetNames(typeof(TodoStates)).ToList();
	}

    private void LoadTodo(string filename)
	{
		Models.Todo todoModel = new Models.Todo();
        todoModel.Path = filename;
        todoModel.Title = filename;

        if (File.Exists(filename))
		{
			var state = File.ReadAllText(filename.Split(".todo-text.txt").First() + ".todo-state.txt");

            todoModel.Description = File.ReadAllText(filename);

			int.TryParse(state, out var index);
			statePicker.SelectedIndex = index;
		}

        BindingContext = todoModel;
	}

	private async void SaveButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Todo todo)
		{
			File.WriteAllText(
				Path.Combine(FileSystem.AppDataDirectory, todo.Title + ".todo-text.txt"),
				DescEditor.Text);
			File.WriteAllText(
				Path.Combine(FileSystem.AppDataDirectory, todo.Title + ".todo-state.txt"),
				statePicker.SelectedIndex.ToString());
		}
		await Shell.Current.GoToAsync("..");
	}

	private async void DeleteButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Models.Todo todo)
			if (File.Exists(todo.Path))
				File.Delete(todo.Path);

        await Shell.Current.GoToAsync("..");
    }
}