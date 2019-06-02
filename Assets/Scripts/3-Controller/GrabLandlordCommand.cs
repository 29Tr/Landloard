using strange.extensions.command.impl;
using System;
using System.Collections.Generic;


public class GrabLandlordCommand:EventCommand
{
    [Inject] public RoundModel RoundModel { get; set; }
    public override void Execute()
    {
        GrabAndDisGrabArgs e = evt.data as GrabAndDisGrabArgs;
        //发地主牌
        dispatcher.Dispatch(ViewEvent.DealThreeCards, e);

        //地主开始游戏
        RoundModel.Start(e.cType);
    }
}