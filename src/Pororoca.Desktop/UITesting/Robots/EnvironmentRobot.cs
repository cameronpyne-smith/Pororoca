using Avalonia.Controls;
using Pororoca.Desktop.Views;

namespace Pororoca.Desktop.UITesting.Robots;

public sealed class EnvironmentRobot : BaseNamedRobot
{
    public EnvironmentRobot(EnvironmentView rootView) : base(rootView){}

    internal Button SetAsCurrentEnvironment => GetChildView<Button>("btSetAsCurrentEnvironment")!;
    internal Button AddVariable => GetChildView<Button>("btAddVariable")!;
    internal DataGrid Variables => GetChildView<DataGrid>("dgVariables")!;
}