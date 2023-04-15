using CommunityToolkit.Mvvm.ComponentModel;

namespace tema4_2.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    int value;

    public bool IsNotBusy => !IsBusy;

}
