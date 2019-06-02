using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

public class LearnButton:Button
{
    public event Action Highlight_btn;
    public event Action Pressed_btn;
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        switch (state)
        {
            case SelectionState.Normal:
                break;
            case SelectionState.Highlighted:
                if (Highlight_btn != null)
                    Highlight_btn();
                break;
            case SelectionState.Pressed:
                if (Pressed_btn != null)
                    Pressed_btn();
                break;
            case SelectionState.Disabled:
                break;
            default:
                break;
        }
    }
}