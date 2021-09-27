Defination:

This tabcontrol is built from a UserControl.

**Usage:**

**from xaml:**

xmlns:tab="clr-namespace:ThingLing.Controls;assembly=ThingLing.Controls.TabControl"

**from code:**

using ThingLing.Controls;

TabControl tabControl = new TabControl();
tabControl.
grid.Children.Add(tabControl);

**to add a TabItem:**

var tabItem = new TabItem
{
    Header = $"Hello RichTextBox {++i}",
    Content = new TextBox { Text = $"Helloo {i}", TextWrapping = TextWrapping.Wrap },
    ToolTip = $"RichTextBox {i}"
};
_tabControl.Add(tabItem);

properties:
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

methods:
Add(TabItem tabItem)
Remove(TabItem tabItem)
RemoveAt(int tabIndex)
RemoveAll()

events:
TabItemAdded
TabItemRemoved
NewTabItemButtonClicked