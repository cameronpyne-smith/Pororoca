using System.Reactive;
using Pororoca.Desktop.Converters;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Pororoca.Desktop.ViewModels;

public sealed class EditableTextBlockViewModel : ViewModelBase
{
    private readonly Action<string> onNameUpdated;

    [Reactive]
    public bool HasIcon { get; private set; }

    private EditableTextBlockIcon? iconField;
    public EditableTextBlockIcon? Icon
    {
        get => this.iconField;
        set
        {
            this.RaiseAndSetIfChanged(ref this.iconField, value);
            HasIcon = value is not null;
        }
    }

    [Reactive]
    public string Txt { get; set; }

    private bool isEditingField;
    public bool IsEditing
    {
        get => this.isEditingField;
        set
        {
            this.RaiseAndSetIfChanged(ref this.isEditingField, value);
            OnIsEditingChanged?.Invoke(value);
        }
    }

    public Action<bool>? OnIsEditingChanged { get; set; }

    public ReactiveCommand<Unit, Unit> EditOrApplyTxtChangeCmd { get; }

    public EditableTextBlockViewModel(string name, Action<string> onNameUpdated)
    {
        this.onNameUpdated = onNameUpdated;
        Txt = name;
        IsEditing = false;
        EditOrApplyTxtChangeCmd = ReactiveCommand.Create(EditOrApplyTxtChange);
    }

    public void EditOrApplyTxtChange()
    {
        if (IsEditing)
        {
            this.onNameUpdated(Txt);
            IsEditing = false;
        }
        else
        {
            IsEditing = true;
        }
    }
}