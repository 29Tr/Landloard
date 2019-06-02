using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;


public class InteractionMediator:EventMediator
{
    [Inject] public InteractionView InteractionView { get; set; }
    [Inject]
    public RoundModel RoundModel { get; set; }
    public override void OnRegister()
    {
        InteractionView.Deal.onClick.AddListener(OnDealClick);
        InteractionView.Grab.onClick.AddListener(OnGrabClick);
        InteractionView.DisGrab.onClick.AddListener(OnDisGrabClick);

        InteractionView.Play.onClick.AddListener(OnPlayClick);
        InteractionView.Pass.onClick.AddListener(OnPassClick);

        dispatcher.AddListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.AddListener(ViewEvent.SuccessedPlay,OnSuccessedPlay);


        RoundModel.PlayerHandle += RoundModel_PlayerHandle;
    }

   
    public override void OnRemove()
    {
        InteractionView.Deal.onClick.RemoveListener(OnDealClick);
        InteractionView.DisGrab.onClick.RemoveListener(OnDisGrabClick);
        InteractionView.Play.onClick.RemoveListener(OnPlayClick);
        InteractionView.Pass.onClick.RemoveListener(OnPassClick);


        dispatcher.RemoveListener(ViewEvent.CompleteDeal, OnCompleteDeal);
        dispatcher.RemoveListener(ViewEvent.SuccessedPlay, OnSuccessedPlay);

        RoundModel.PlayerHandle -= RoundModel_PlayerHandle;

    }
    /// <summary>
    /// 点击发牌事件
    /// </summary>
    private void OnDealClick()
    {
        dispatcher.Dispatch(CommandEvent.RequestDeal);
        InteractionView.DeactiveAll();
    }
    /// <summary>
    /// 不抢地主
    /// </summary>
    private void OnDisGrabClick()
    {
        InteractionView.DeactiveAll();
        CharacterType temp = (CharacterType)Random.Range(2, 4);
        GrabAndDisGrabArgs e = new GrabAndDisGrabArgs()
        {
            cType = temp
        };
        dispatcher.Dispatch(CommandEvent.GrabLanlord,e);
    }
    /// <summary>
    /// 抢地主
    /// </summary>
    private void OnGrabClick()
    {
        InteractionView.DeactiveAll();
        GrabAndDisGrabArgs e = new GrabAndDisGrabArgs()
        {
            cType = CharacterType.Player
        };
        dispatcher.Dispatch(CommandEvent.GrabLanlord, e);
    }
    /// <summary>
    /// 发牌结束
    /// </summary>
    /// <param name="payload"></param>
    private void OnCompleteDeal(IEvent payload)
    {
        InteractionView.ActiveGrabOrDisGrab();
    }
    /// <summary>
    /// 玩家出牌显示UI
    /// </summary>
    /// <param name="canClick"></param>
    void RoundModel_PlayerHandle(bool canClick)
    {
        InteractionView.ActiveDealOrPass(canClick);
    }
    /// <summary>
    /// 玩家出牌
    /// </summary>
    private void OnPlayClick()
    {
        dispatcher.Dispatch(ViewEvent.RequestPlay);
    }
    /// <summary>
    /// 成功出牌
    /// </summary>
    /// <param name="payload"></param>
    private void OnSuccessedPlay()
    {
       InteractionView.DeactiveAll();
    }



    /// <summary>
    /// 不出
    /// </summary>
    private void OnPassClick()
    {
        InteractionView.DeactiveAll();
        dispatcher.Dispatch(CommandEvent.PassCard);
    }

  

}