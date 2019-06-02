using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 设置电脑，玩家的头像，积分显示
/// </summary>
public class CharacterUI : MonoBehaviour {

    public Image head;
    public Text score;
    public Text remain;


    public void SetIdentity(Identity identity)
    {
        switch (identity)
        {
            case Identity.Former:
                head.sprite = Resources.Load<Sprite>("Pokers/Role_Farmer");
                break;
            case Identity.Landlord:
                head.sprite = Resources.Load<Sprite>("Pokers/Role_Landlord");
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 设置积分
    /// </summary>
    /// <param name="score"></param>
    public void SetIntegration(int score)
    {
        this.score.text = score.ToString();
    }

    public void SetRemain(int number)
    {
        remain.text = "剩余手牌：" + number.ToString();
    }
}
