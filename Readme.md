<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128652771/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4617)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# WPF Data Grid – Select Child Rows When a User Clicks or Expands a Group Row

This example selects all child rows in a group when a user expands a group row or clicks a group row.

![Select Child Rows When a User Clicks or Expands a Group Row](./Images/expanded-rows.jpg)

Use this technique when you need to:

* Select all children after a group expands.
* Select children on a group-row click.
* Apply recursive selection in nested groups.

## Implementation Details

### Attached Behavior

Set the `GroupChildSelector.Mode` attached property to `Hierarchical` to select all descendants in expanded subgroups.

The child-row selection logic works with two events: 

* [`PreviewMouseLeftButtonUp`](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.previewmouseleftbuttonup) - fires when a user clicks a group row. 
* [`GroupRowExpanding`](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl.GroupRowExpanding) - fires when a user expands a group. 

When either event occurs, the child-row selection logic selects all child rows in that group and calls [`BeginSelection`](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.BeginSelection) and [`EndSelection`](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.EndSelection) methods to apply the changes in one step.

```xaml
<dxg:GridControl>
    <dxg:GridControl.View>
        <dxg:TableView
        local:GroupChildSelector.Mode="Hierarchical" />
    </dxg:GridControl.View>
</dxg:GridControl>
```

### Selection Logic

The `GroupChildSelector` calls the `SelectChild(grid, groupRowHandle)` method to select all child rows in a group. In `Hierarchical` mode, if a child row is an expanded group, the method calls itself to select that group’s child rows. The code calls the `BeginSelection` method before changes and the `EndSelection` method after changes to update the selection in a single step.

## Files to Review

* [MainWindow.xaml](./CS/GridGroupSelect/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/GridGroupSelect/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/GridGroupSelect/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/GridGroupSelect/MainWindow.xaml.vb))
* [GroupChildSelector.cs](./CS/GridGroupSelect/GroupChildSelector.cs) (VB: [GroupChildSelector.vb](./VB/GridGroupSelect/GroupChildSelector.vb))
* [SampleDataRow.cs](./CS/GridGroupSelect/SampleDataRow.cs) (VB: [SampleDataRow.vb](./VB/GridGroupSelect/SampleDataRow.vb))

## Documentation

* [TableView](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView)
* [GroupRowExpanding](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl.GroupRowExpanding)
* [BeginSelection](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.BeginSelection)
* [EndSelection](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.DataControlBase.EndSelection)

## More Examples

* [WPF Data Grid - Specify Custom Content for Headers Displayed in the Column Chooser](https://github.com/DevExpress-Examples/wpf-data-grid-custom-content-for-column-chooser-headers)
* [WPF Data Grid - Bind to Dynamic Data](https://github.com/DevExpress-Examples/wpf-bind-gridcontrol-to-dynamic-data)
* [Implement CRUD Operations in the WPF Data Grid](https://github.com/DevExpress-Examples/wpf-data-grid-implement-crud-operations)
* [WPF Grid - Resize Rows Using a Splitter](https://github.com/sergepilipchuk/wpf-grid-resize-rows-using-splitter)

<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=how-to-select-all-group-subitems-when-a-group-row-is-clicked-wpf-e4617&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=how-to-select-all-group-subitems-when-a-group-row-is-clicked-wpf-e4617&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
