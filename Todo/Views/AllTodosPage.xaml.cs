namespace Todo.Views;

public partial class AllTodosPage : ContentPage
{
	public AllTodosPage()
	{
		InitializeComponent();

		BindingContext = new Models.AllTodos();
	}

	protected override void OnAppearing()
	{
		((Models.AllTodos)BindingContext).LoadTodos();
	}

	private async void New_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(NewTodoPage));
	}

	private async void todosCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Count != 0)
		{
			var todo = (Models.Todo)e.CurrentSelection[0];
            await Shell.Current.GoToAsync($"{nameof(NewTodoPage)}?{nameof(NewTodoPage.ItemId)}={todo.Path}");
            todosCollection.SelectedItem = null;
		}
	}
}