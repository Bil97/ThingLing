# MessageBox
Displays a message box. It is a normal Avalonia window customised to be used as a simple dialog to accept Yes, No, Ok, Cancel and None responses from the user.

**Using:**

```c#
using ThingLing.Controls;
```

**Without result:**

```c#
MessageBox.ShowAsync(owner window, message);
```

**//OR**

```c#
await MessageBox.ShowAsync(owner window, message);
```

**To get result:**

**from async method:**

```c#
var mb = await MessageBox.ShowAsync(this, "Hello world, this message box is working fine", "Hello title", MessageBoxButton.OKCancel, MessageBoxImage.Information);
this.FindControl<TextBlock>("result").Text = mb.ToString();
```

**from non async method:**

```c#
TextBlock textBlock = new TextBlock;
MessageBoxResult result;
var task = new Task(async () =>
{
    result = await MessageBox.ShowAsync(this, "Hello world message", "Title", MessageBoxButton.YesNoCancel,MessageBoxImage.Warning);
    textBlock.Text=result.ToString();
});
task.RunSynchronously();
```

**from a class that does not inherit from window:**

```c#
public class Users : UserControl
{
	public Users()
	{
		InitializeComponent();

		TextBlock textBlock = new TextBlock;
		MessageBoxResult result;
		var task = new Task(async () =>
		{
			var desktop = (IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current.ApplicationLifetime;
			result = await MessageBox.ShowAsync(desktop.MainWindow, "Hello world message", "Title", MessageBoxButton.YesNoCancel,MessageBoxImage.Warning);

			textBlock.Text=result.ToString();
		});
	}

	//// OR from async method

	async OnClick(object sender, RoutedEventArgs e)
	{
		var desktop = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
		var mb = await MessageBox.ShowAsync(desktop.MainWindow, "Hello world, this message box is working fine", "Hello title", MessageBoxButton.OKCancel, MessageBoxImage.Information);
		this.FindControl<TextBlock>("result").Text = mb.ToString();
	}

}
```

