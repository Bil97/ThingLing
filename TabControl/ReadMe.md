**Defination:**

This tabcontrol is built from a UserControl.

**Usage:**

**from xaml:**
```xaml
xmlns:tab="clr-namespace:ThingLing.Controls;assembly=ThingLing.Controls.TabControl"
```
Default TabMode=Document
```xaml
<tab:TabControl x:Name="_tabControl" />
```
```xaml
<tab:TabControl x:Name="_tabControl" TabMode="Document" />
```
or
```xaml
<tab:TabControl x:Name="_tabControl" TabMode="Window" />
```
**from code:**
```C#
using ThingLing.Controls;

TabControl tabControl = new TabControl();

grid.Children.Add(tabControl);
```

**to add a TabItem:**

```C#
var tabItem = new TabItem
{
    Header = $"Hello RichTextBox {++i}",
    Content = new TextBox { Text = $"Helloo {i}", TextWrapping = TextWrapping.Wrap },
    ToolTip = $"RichTextBox {i}"
};
_tabControl.Add(tabItem);
```

**properties:**

```C#
TabMode
TabStripPlacementSide
TabItemRotationAngle
TabItemsCount
SelectedTabItem
Theme
CollapseVisibilityWhenEmpty
AlwaysVisible
HideNewTabButton
HideOpenTabsButton
```
**methods:**

```C#
Add(TabItem tabItem)
Remove(TabItem tabItem)
RemoveAt(int tabIndex)
RemoveAll()
```

**events:**

```C#
TabItemAdded
TabItemRemoved
NewTabItemButtonClicked
```