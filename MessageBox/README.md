# MessageBox
Displays a message box. It is a normal Avalonia window customised to be used as a simple dialog to accept Yes, No, Ok, Cancel and None responses from the user.

**Using:**

```c#
using ThingLing.Controls;
```

**Without result:**

```c#
MessageBox.ShowAsync(message);
// Or with a custom window
MessageBox.ShowAsync(owner window, message);
```

**OR**

```c#
await MessageBox.ShowAsync(message);
// Or with a custom window
await MessageBox.ShowAsync(owner window, message);
```

**To get result:**

**from async method:**

```c#
var mb = await MessageBox.ShowAsync("Hello world, this message box is working fine", "Hello title", MessageBoxButton.OKCancel, MessageBoxImage.Information);

// Or with a class that inherits from Window
var mb = await MessageBox.ShowAsync(this, "Hello world, this message box is working fine", "Hello title", MessageBoxButton.OKCancel, MessageBoxImage.Information);

// Or with a custom window
var window = new Window();
var mb = await MessageBox.ShowAsync(window, "Hello world, this message box is working fine", "Hello title", MessageBoxButton.OKCancel, MessageBoxImage.Information);

this.FindControl<TextBlock>("result").Text = mb.ToString();
```

**from non async method:**

```c#
TextBlock textBlock = new TextBlock;
MessageBoxResult result;
var task = new Task(async () =>
{
    result = await MessageBox.ShowAsync("Hello world message", "Title", MessageBoxButton.YesNoCancel,MessageBoxImage.Warning);

// Or with a class that inherits from window
    result = await MessageBox.ShowAsync(this, "Hello world message", "Title", MessageBoxButton.YesNoCancel,MessageBoxImage.Warning);
    textBlock.Text=result.ToString();

// Or with a custom window
   var window = new Window();
    result = await MessageBox.ShowAsync(window, "Hello world message", "Title", MessageBoxButton.YesNoCancel,MessageBoxImage.Warning);
    textBlock.Text=result.ToString();
});
task.RunSynchronously();
```