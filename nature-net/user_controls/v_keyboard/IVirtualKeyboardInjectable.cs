using System;
using System.Windows.Controls;

namespace nature_net.user_controls
{
    public interface IVirtualKeyboardInjectable
    {
        System.Windows.Controls.Control ControlToInjectInto { get; }
    }
}
