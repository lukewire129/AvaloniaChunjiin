using System.Collections.ObjectModel;
using System.Linq;

namespace AvaloniaChunjiin.Models;

public class KeyPadModel : ObservableCollection<KeyButtonModel>
{
    public bool isNumberMode = false;
    public void CharChange()
    {
        if (isNumberMode == true)
        { 
            this.Items.FirstOrDefault((x=>x.Tag == "NumberChange")).Change();
            isNumberMode = false;
        }
        foreach (var keyButtonModel in this.Items.Where(x=>isCharChange(x)).ToList())
        {
            keyButtonModel.Change();
        }
    }

    public bool isCharChange(KeyButtonModel model)
    {
        if (isDigit(model.Tag))
            return true;

        if (model.Tag == "CharChange")
            return true;
        
        return false;
    }
    
    public void NumberChange()
    {
        isNumberMode = !isNumberMode;
        foreach (var keyButtonModel in this.Items.Where(x=> isCustomTag(x)).ToList())
        {
            keyButtonModel.CustomChange();
        }
        
        this.Items.FirstOrDefault((x=>x.Tag == "NumberChange")).Change();
    }

    private bool isCustomTag(KeyButtonModel model)
    {
        if (model.changType3Data == null)
        {
            return false;
        }
        return isDigit(model.Tag);
    }

    private bool isDigit(string s)
    {
        int i = 0;
        string _s = s;  
        return int.TryParse(_s, out i);
    }
}