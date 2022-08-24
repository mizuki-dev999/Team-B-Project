using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public BattleManager battleManager;
    //�L�����N�^�[�̕Ґ��摜
    public Image[] myPartyImages;
    public Image[] enemyPartyImages;

    /// <summary>
    /// �L�����N�^�[�摜��Ґ��摜�ɃZ�b�g����
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
