using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public BattleManager battleManager;
    //キャラクターの編成画像
    public Image[] myPartyImages;
    public Image[] enemyPartyImages;

    /// <summary>
    /// キャラクター画像を編成画像にセットする
    /// </summary>
    public void SetCharacterImageToParty()
    {
        for (int i = 0; i < myPartyImages.Length; i++)
        {
            if (battleManager.myParty[i] != null) myPartyImages[i].sprite = battleManager.myParty[i].characterImage;
        }
        for (int i = 0; i < enemyPartyImages.Length; i++)
        {
            if (battleManager.enemyParty[i] != null) enemyPartyImages[i].sprite = battleManager.enemyParty[i].characterImage;
        }
    }

}
