using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCommand : Command{

    [Inject] public IntegrationModel integrationModel { get; set; }

    [Inject]public CardModel CardModel { get; set; }
    [Inject]public RoundModel RoundModel { get; set; }
    //执行事件
    public override void Execute()
    {
        Tools.CreatPanel(PanelType.StartPanel);

        //初始化model
        integrationModel.InitIntegration();
        RoundModel.InitRound();
        CardModel.InitCardLibrary();

        //TODO ：读取数据

    }
}
