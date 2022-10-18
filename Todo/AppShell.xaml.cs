namespace Todo;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(Views.NewTodoPage), typeof(Views.NewTodoPage));
	}
}
