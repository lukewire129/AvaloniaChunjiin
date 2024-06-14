using Avalonia.Controls;

namespace AvaloniaChunjiin.Components;

public class KeyPad : ListBox
{
    public KeyPad()
    {
            
    }
    protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
    {
        return new KeyButton();
    }
}