using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ChangeMultipleCommand:EventCommand
{
    [Inject]public IntegrationModel Integerationmodel { get; set; }

    public override void Execute()
    {
        //改model
        Integerationmodel.Multiple *= (int)evt.data;//
        //Debug.Log(evt.data);

        Tools.CreatPanel(PanelType.CharacterPanel);
        Tools.CreatPanel(PanelType.InteractionPanel);
    }
}
