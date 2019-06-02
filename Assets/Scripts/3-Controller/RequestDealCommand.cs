using strange.extensions.command.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RequestDealCommand: EventCommand
{
    [Inject]public CardModel CardModel { get; set; }
    public DeskController DeskController { get { return GameObject.FindObjectOfType<DeskController>(); } }
    public override void Execute()
    {
        //Debug.Log("发牌");
        //洗牌
        CardModel.Shuffle();
        DeskController.StartCoroutine(DealCard());
    }


  IEnumerator  DealCard()
    {
        //给每个人17张
        CharacterType curr = CharacterType.Player;
        for (int i = 0; i < 51; i++)
        {
            if (curr == CharacterType.Library || curr == CharacterType.Desk)
            {
                curr = CharacterType.Player;
            }
            Deal(curr);
            curr++;
            yield return new WaitForSeconds(0.01f);
        }
        //地主牌 桌面
        for (int i = 0; i < 3; i++)
        {
            Deal(CharacterType.Desk);
        }
        CardUI[] carduis = DeskController.CreatePoint.GetComponentsInChildren<CardUI>();
        foreach (var ui in carduis)
        {
            ui.SetImageAgain();
        }
        //发牌结束
        dispatcher.Dispatch(ViewEvent.CompleteDeal);
    }

    private void Deal(CharacterType ctype)
    {
        Card card = CardModel.DealCard(ctype);
        DealCardArgs e = new DealCardArgs()
        {
            card = card,
            cType = ctype,
            isSelected = false

        };
        dispatcher.Dispatch(ViewEvent.DealCard, e);
    }
}