using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterView:View
{
    public PlayerController PlayerController;
    public DeskController DeskController;
    public ComputerController ComputerLeftController;
    public ComputerController ComputerRightController;
    /// <summary>
    /// 初始化UI
    /// </summary>
    public void Init()
    {
        PlayerController.Identity = Identity.Former;
        ComputerLeftController.Identity = Identity.Former;
        ComputerRightController.Identity = Identity.Former;
    }
    /// <summary>
    /// 添加牌
    /// </summary>
    /// <param name="cType">给谁</param>
    /// <param name="card">发什么牌</param>
    /// <param name="isSelected">是否选中</param>
    /// <param name="pos">桌子位置</param>
    public void AddCard(CharacterType cType,Card card,bool isSelected,ShowPoint pos)
    {
        switch (cType)
        {           
            case CharacterType.Player:
                PlayerController.AddCard(card, isSelected);
                PlayerController.Sort(false);
                break;
            case CharacterType.ComputerRight:
                ComputerRightController.AddCard(card, isSelected);
                break;
            case CharacterType.ComputerLeft:
                ComputerLeftController.AddCard(card, isSelected);
                break;
            case CharacterType.Desk:
                DeskController.AddCard(card, isSelected);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 添加三张牌(地主牌
    /// </summary>
    /// <param name="cType">给谁添加</param>
    internal void DealThreeCard(CharacterType cType)
    {
        Card card = null;
        switch (cType)
        {         
            case CharacterType.Player:
                for (int i = 0; i < 3; i++)
                {
                    card = DeskController.DealCard();
                    //牌高出
                    PlayerController.AddCard(card, true);
                    //更新到桌面
                    DeskController.SetShowCard(card, i);
                }
                PlayerController.Identity = Identity.Landlord;
                PlayerController.Sort(false);
                break;
            case CharacterType.ComputerRight:
                for (int i = 0; i < 3; i++)
                {
                    card = DeskController.DealCard();
                    ComputerRightController.AddCard(card, false);
                    DeskController.SetShowCard(card, i);
                }
                ComputerRightController.Identity = Identity.Landlord;
                ComputerRightController.Sort(true);
                break;
            case CharacterType.ComputerLeft://
                for (int i = 0; i < 3; i++)
                {
                    card = DeskController.DealCard();
                    ComputerLeftController.AddCard(card, false);
                    DeskController.SetShowCard(card, i);
                }
                ComputerLeftController.Identity = Identity.Landlord;
                ComputerRightController.Sort(true);
                break;
            default:
                break;
        }
        DeskController.Clear(ShowPoint.Desk);
    }
}