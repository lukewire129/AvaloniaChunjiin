using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaChunjiin.Models;

public partial class KeyButtonModel : ObservableObject
{
    public string Tag;
    
    [ObservableProperty] string charData;
    private string changType1Data;
    private string changType2Data;
    public string changType3Data;
    private bool isBaseData = true;
    public bool isCustomData = false;
    public KeyButtonModel(string Tag, string changType1, string changType2 = null, string changType3 = null)
    {
        this.Tag = Tag;
        this.changType1Data = changType1;
        this.changType2Data = changType2;
        this.changType3Data = changType3;

        CharData = this.changType1Data;
    }

    private void CharChange()
    {
        CharData = isBaseData? changType1Data : changType2Data;
    }
    
    public void Change()
    {
        isCustomData = false;
        isBaseData = !isBaseData;
        CharChange();
    }
    
    public void CustomChange()
    {
        isCustomData = !isCustomData;
        if (isCustomData)
        {
            CharData = this.changType3Data;
            return;
        }
        CharChange();
    }
}