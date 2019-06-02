using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
public class InteractionView:View
{
    /// <summary>
    /// 发牌
    /// </summary>
    public Button Deal;

    public Button Grab;
    public Button DisGrab;

    public Button Play;
    public Button Pass;

    /// <summary>
    /// 全部隐藏
    /// </summary>
    public void DeactiveAll()
    {
        Play.gameObject.SetActive(false);
        Pass.gameObject.SetActive(false);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Deal.gameObject.SetActive(false);
    }
    /// <summary>
    /// 显示发牌按钮
    /// </summary>

    public void ActivePlay()
    {
        Play.gameObject.SetActive(false);
        Pass.gameObject.SetActive(false);
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Deal.gameObject.SetActive(true);//开始发牌
    }
    /// <summary>
    /// 显示抢地主
    /// </summary>
    public void ActiveGrabOrDisGrab()
    {
        Play.gameObject.SetActive(false);
        Pass.gameObject.SetActive(false);
        Grab.gameObject.SetActive(true);
        DisGrab.gameObject.SetActive(true);
        Deal.gameObject.SetActive(false);
    }
    /// <summary>
    /// 显示出牌
    /// </summary>
    public void ActiveDealOrPass(bool isActive=true)
    {
        Play.gameObject.SetActive(true);
        Pass.gameObject.SetActive(true);
        Pass.interactable = isActive;
        Grab.gameObject.SetActive(false);
        DisGrab.gameObject.SetActive(false);
        Deal.gameObject.SetActive(false);
    }
}
