using System;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.command.impl;

public class PlayCardCommand:EventCommand
{
    [Inject]
    public RoundModel RoundModel { get; set; }
    public override void Execute()
    {
        PlayCardArgs e=evt.data as PlayCardArgs;
        if (e.CharacterType == CharacterType.Player)
        {
            if (e.CardType == RoundModel.CurrentType && e.Length == RoundModel.CurrentLength &&
                e.Weight > RoundModel.CurrentWeight)
                dispatcher.Dispatch(ViewEvent.SuccessedPlay);
            else if(e.CardType==CardType.Boom&&RoundModel.CurrentType!=CardType.Boom)
                dispatcher.Dispatch(ViewEvent.SuccessedPlay);
            else if(e.CardType==CardType.JokerBoom)
                dispatcher.Dispatch(ViewEvent.SuccessedPlay);
            else if(e.CharacterType==RoundModel.BiggestCharacter)
                dispatcher.Dispatch(ViewEvent.SuccessedPlay);
            else
            {
                UnityEngine.Debug.Log("重新选择");
                return;
            }                
            
        }


        //更新数据
        RoundModel.BiggestCharacter = e.CharacterType;
        
        RoundModel.CurrentLength = e.Length;
        RoundModel.CurrentWeight = e.Weight;
        RoundModel.CurrentType = e.CardType;


        //换人
        RoundModel.Turn();
    }
}