using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;

namespace Pororoca.Desktop.HotKeys;

internal static class Dialogs
{
    internal static void ShowDialog(string title, string message, ButtonEnum buttons, Action onButtonOkClicked)
    {
        Task asyncAction() => new(onButtonOkClicked);
        ShowDialog(title, message, buttons, asyncAction);
    }

    internal static void ShowDialog(string title, string message, ButtonEnum buttons, Func<Task>? onButtonOkClicked = null)
    {
        Bitmap bitmap = new(AssetLoader.Open(new("avares://Pororoca.Desktop/Assets/Images/pororoca.png")));

        var msgbox = MessageBoxManager.GetMessageBoxStandard(
            new MessageBoxStandardParams()
            {
                ContentTitle = title,
                ContentMessage = message,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowIcon = new(bitmap),
                ButtonDefinitions = buttons
            });
        Dispatcher.UIThread.Post(async () =>
        {
            var buttonResult = await msgbox.ShowAsync();
            if (buttonResult == ButtonResult.Ok)
            {
                var task = onButtonOkClicked?.Invoke();
                if (task is not null) await task;
            }
        });
    }
}