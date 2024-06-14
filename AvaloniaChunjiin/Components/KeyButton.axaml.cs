using System;
using Avalonia.Controls;
using AvaloniaChunjiin.Models;

namespace AvaloniaChunjiin.Components;

public class KeyButton : Button
{
    public KeyButton()
    {
        this.Click+= (s, e) =>
        {
            var model = (KeyButtonModel)this.DataContext;
            if (model.isCustomData && String.IsNullOrWhiteSpace(model.changType3Data) == false)
            {
                this.Command?.Execute(model.changType3Data);
                return;
            }
            this.Command?.Execute(model.Tag);
        };
    }
}