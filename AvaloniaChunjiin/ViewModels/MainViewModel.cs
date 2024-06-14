using System.Collections.ObjectModel;
using AvaloniaChunjiin.Models;
using AvaloniaChunjiin.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaChunjiin.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private KeypadService _keypadService;
    [ObservableProperty] KeyPadModel keyPadModels;
    [ObservableProperty] ObservableCollection<string> alltext;
    [ObservableProperty] string text;
    public MainViewModel()
    {
        Alltext = new();
        _keypadService = new();
        KeyPadModels = new()
        {
            new KeyButtonModel("0", "ㅣ", "ABC", "1"),
            new KeyButtonModel("1", "·", "DEF", "2"),
            new KeyButtonModel("2", "ㅡ", "GHI", "3"),
            new KeyButtonModel("back", "\u2190"),
            new KeyButtonModel("3", "ㄱㅋ", "JKL", "4"),
            new KeyButtonModel("4", "ㄴㄹ", "MNO", "5"),
            new KeyButtonModel("5", "ㄷㅌ", "PQR", "6"),
            new KeyButtonModel("Enter", "\u23ce"),
            new KeyButtonModel("6", "ㅂㅍ", "STU", "7"),
            new KeyButtonModel("7", "ㅅㅎ", "VWX", "8"),
            new KeyButtonModel("8", "ㅈㅊ", "YZ", "9"),
            new KeyButtonModel("AllClear", "\u25c0"),
            new KeyButtonModel("CharChange", "한/영","Kor/Eng"),
            new KeyButtonModel("9", "ㅇㅁ", "", "0"),          
            new KeyButtonModel("SpaceBar", "SpaceBar"),
            new KeyButtonModel("NumberChange", "123","ㄱㄴㄷ/ABC"),
        };
    }

    [RelayCommand]
    private void Keypad(string num)
    {
        int i = 0;
        string s = num;  
        bool result = int.TryParse(s, out i);
        if (result == false)
        {
            switch (s)
            {
                case "back":
                    _keypadService.Cor();
                    break;
                case "Enter":
                    Alltext.Add(Text);
                    _keypadService.Clear();
                    break;
                case "AllClear":
                    _keypadService.Clear();
                    break;
                case "CharChange":
                    _keypadService.LanTypeChange();
                    KeyPadModels.CharChange();
                    break;
                case "NumberChange":
                    KeyPadModels.NumberChange();
                    break;
                case "SpaceBar":
                    _keypadService.Spacebar();
                    break;
            }
            this.Text = this._keypadService.InputTextString;
            return;
        }

        if (KeyPadModels.isNumberMode == true)
        {
            _keypadService.InputText(s);
        }
        else
        {
            _keypadService.InputText(i);
        }
        this.Text = this._keypadService.InputTextString;
    }
}