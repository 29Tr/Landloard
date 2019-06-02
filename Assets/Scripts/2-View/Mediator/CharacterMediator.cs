using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CharacterMediator:EventMediator
{
    [Inject] public CharacterView CharacterView { get; set; }
    [Inject]
    public RoundModel RoundModel { get; set; }
    public override void OnRegister()
    {
        CharacterView.Init();
        dispatcher.AddListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.AddListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.AddListener(ViewEvent.DealThreeCards, OnDealThreeCards);
        dispatcher.AddListener(ViewEvent.RequestPlay,OnPlayerPlayCard);
        dispatcher.AddListener(ViewEvent.SuccessedPlay,OnSuccessedPlay);

        RoundModel.ComputerHandle += Roundmodel_ComputerHandle;
        RoundModel.PlayerHandle += RoundModel_PlayHandle;
    }

   


    public override void OnRemove()
    {
     
        dispatcher.RemoveListener(ViewEvent.DealCard, OnDealCard);
        dispatcher.RemoveListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.RemoveListener(ViewEvent.DealThreeCards, OnDealThreeCards);
        dispatcher.RemoveListener(ViewEvent.RequestPlay, OnPlayerPlayCard);
        dispatcher.RemoveListener(ViewEvent.SuccessedPlay, OnSuccessedPlay);

        RoundModel.ComputerHandle -= Roundmodel_ComputerHandle;
        RoundModel.PlayerHandle -= RoundModel_PlayHandle;

    }
    /// <summary>
    /// 处理发牌
    /// </summary>
    /// <param name="evt"></param>
    private void OnDealCard(IEvent evt)
    {
        DealCardArgs e = evt.data as DealCardArgs;
        CharacterView.AddCard(e.cType, e.card, e.isSelected, ShowPoint.Desk);
    }
    /// <summary>
    /// 发牌结束后
    /// </summary>
    private void OnCompleteDeal()
    {
        CharacterView.ComputerLeftController.Sort(true);
        CharacterView.ComputerRightController.Sort(true);
        CharacterView.DeskController.Sort(true);
    }
    /// <summary>
    /// 发三张牌
    /// </summary>
    /// <param name="evt"></param>
    private void OnDealThreeCards(IEvent evt)
    {
        GrabAndDisGrabArgs e = evt.data as GrabAndDisGrabArgs;
        CharacterView.DealThreeCard(e.cType);
    }

    /// <summary>
    /// 玩家出牌调用
    /// </summary>
    private void OnPlayerPlayCard()
    {
        //不能直接出牌，需要判断
        List<Card> cardList = CharacterView.PlayerController.FindSelected();
        CardType cardType;
        if (Ruler.CanPop(cardList, out cardType))
        {
            PlayCardArgs e=new PlayCardArgs()
            {
                CardType = cardType,
                CharacterType=CharacterType.Player,
                Length=cardList.Count,
                Weight=Tools.GetWeight(cardList,cardType)
            };
            UnityEngine.Debug.Log(cardType.ToString());
            dispatcher.Dispatch(CommandEvent.PlayCard,e);
        }
        else
        {Debug.Log("牌不对！");}
    }
    /// <summary>
    /// 成功出牌
    /// </summary>
    private void OnSuccessedPlay()
    {
        List<Card> cardList = CharacterView.PlayerController.FindSelected();
        //清空桌面
        CharacterView.DeskController.Clear(ShowPoint.Player);
         //添加到桌面
        foreach (var card in cardList)
            CharacterView.DeskController.AddCard(card,false,ShowPoint.Player);
        CharacterView.PlayerController.DestroySelectCard();

        if (!CharacterView.PlayerController.HasCard)
        {
            Identity r = CharacterView.ComputerRightController.Identity;
            Identity l = CharacterView.ComputerLeftController.Identity;
            Identity p = CharacterView.PlayerController.Identity;
            GameOverArgs eee=new GameOverArgs()
            {
                ComputerLeftWin = l==p? true:false,
                ComputerRightWin =r==p?true:false,
                PlayWin = true,
                 isLandlord = p==Identity.Landlord
            };
            dispatcher.Dispatch(CommandEvent.GameOver,eee);
        }
    }
    /// <summary>
    /// 玩家出牌监听
    /// </summary>
    /// <param name="obj"></param>
    private void RoundModel_PlayHandle(bool obj)
    {
        CharacterView.DeskController.Clear(ShowPoint.Player);
        
    }



    void Roundmodel_ComputerHandle(ComputerSmartArgs e)
    {
        StartCoroutine(Delay(e));        
    }
    /// <summary>
    /// 电脑出牌
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    IEnumerator Delay(ComputerSmartArgs e)
    {
        bool can = false;
        yield return new WaitForSeconds(1f);
        switch (e.CharacterType)
        {
            case CharacterType.ComputerRight:
                //清空桌面
                CharacterView.DeskController.Clear(ShowPoint.ComputerRight);

                can = CharacterView.ComputerRightController.SmartSelectCards(e.CardType, e.Weight, e.Length,
                    e.IsBiggest == CharacterType.ComputerRight);
                //UnityEngine.Debug.Log(e.IsBiggest.ToString());
                //UnityEngine.Debug.Log("CRight"+can);
                if (can)
                {
                    List<Card> cardList = CharacterView.ComputerRightController.SelectCards;
                    CardType CurrType = CharacterView.ComputerRightController.CurrentType;
                    //添加牌到桌面
                    foreach (var card in cardList)
                        CharacterView.DeskController.AddCard(card,false,ShowPoint.ComputerRight);
                    PlayCardArgs ee=new PlayCardArgs()
                    {
                        CardType=CurrType,
                        Length=cardList.Count,
                        CharacterType=CharacterType.ComputerRight,
                        Weight = Tools.GetWeight(cardList,CurrType)
                    };
                 //判断胜负
                    if (!CharacterView.ComputerRightController.HasCard)
                    {
                        Identity r = CharacterView.ComputerRightController.Identity;
                        Identity l = CharacterView.ComputerLeftController.Identity;
                        Identity p = CharacterView.PlayerController.Identity;
                        GameOverArgs eee=new GameOverArgs()
                        {
                            ComputerRightWin=true,
                            ComputerLeftWin = l==r?true:false,
                            PlayWin = p==r?true:false,
                            isLandlord = p==Identity.Landlord
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver);
                    }
                    else
                    {
                        dispatcher.Dispatch(CommandEvent.PlayCard,ee);
                    }
                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }

                break;
            case CharacterType.ComputerLeft:
                //清空桌面
                CharacterView.DeskController.Clear(ShowPoint.ComputerLeft);

                can = CharacterView.ComputerLeftController.SmartSelectCards(e.CardType, e.Weight, e.Length,
                    e.IsBiggest == CharacterType.ComputerLeft);
                //UnityEngine.Debug.Log("cOMPUTER LEFT    "+can); 
                //UnityEngine.Debug.Log(e.IsBiggest.ToString());
                if (can)
                {
                    List<Card> cardList = CharacterView.ComputerLeftController.SelectCards;
                    CardType CurrType = CharacterView.ComputerLeftController.CurrentType;
                    //添加牌到桌面
                    foreach (var card in cardList)
                        CharacterView.DeskController.AddCard(card, false, ShowPoint.ComputerLeft);
                    PlayCardArgs ee = new PlayCardArgs()
                    {
                        CardType = CurrType,
                        Length = cardList.Count,
                        CharacterType = CharacterType.ComputerLeft,
                        Weight = Tools.GetWeight(cardList, CurrType)
                    };
                    //判断胜负
                    if (!CharacterView.ComputerLeftController.HasCard)
                    {
                        Identity r = CharacterView.ComputerRightController.Identity;
                        Identity l = CharacterView.ComputerLeftController.Identity;
                        Identity p = CharacterView.PlayerController.Identity;
                        GameOverArgs eee=new GameOverArgs()
                        {
                            ComputerLeftWin = true,
                            ComputerRightWin = r==l?true:false,
                            PlayWin = p==l?true:false,
                            isLandlord = p==Identity.Landlord
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver);
                    }
                    else
                    {
                        dispatcher.Dispatch(CommandEvent.PlayCard,ee);
                    }
                }
                else { dispatcher.Dispatch(CommandEvent.PassCard); }

                break;
        }
    }
}
