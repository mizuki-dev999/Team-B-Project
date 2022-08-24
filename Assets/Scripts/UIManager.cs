using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public BattleManager battleManager;
    //キャラクターの編成画像
    public Image[] myPartyImages;
    public Image[] enemyPartyImages;

    [Header("キャラクター選択時用キャラクター詳細UI")]
    public GameObject skillInformationPanel;
    public TextMeshProUGUI skillInformationPanelNameText;
    public TextMeshProUGUI[] skillInformationPanelHandTexts;
    public TextMeshProUGUI[] skillInformationPanelDamageTexts;
    [Header("トップUI")]
    public TextMeshProUGUI turnNumberText;
    public TextMeshProUGUI myHpValueText;
    public TextMeshProUGUI myEnemyValueText;
    public Image myHpBar;
    public Image enemyHpBar;

    private void Start()
    {
        turnNumberText.text = $"{battleManager.Turn}/{battleManager.maxTurn}";
        myHpBar.fillAmount = 1;
        myHpValueText.text = $"<color=white>{battleManager.MyPartyCurrentHp}</color>/{battleManager.myPartyMaxHp}";
        enemyHpBar.fillAmount = 1;
        myEnemyValueText.text = $"<color=white>{battleManager.EnemyPartyCurrentHp}</color>/{battleManager.enemyPartyMaxHp}";
    }

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

    public void PointerDownAnimation(RectTransform target)
    {
        target.DOScale(new Vector3(0.94f, 0.94f, 1), 0.08f);
    }

    public void PointerUpAnimation(RectTransform target)
    {
        target.DOScale(new Vector3(1, 1, 1), 0.08f);
    }

    public void ChangeMyHpUI()
    {
        myHpBar.fillAmount = (float)battleManager.MyPartyCurrentHp/battleManager.myPartyMaxHp;
        myHpValueText.text = $"<color=white>{battleManager.MyPartyCurrentHp}</color>/{battleManager.myPartyMaxHp}";
    }
    public void ChangeEnemyHpUI()
    {
        enemyHpBar.fillAmount = (float)battleManager.EnemyPartyCurrentHp / battleManager.enemyPartyMaxHp;
        myEnemyValueText.text = $"<color=white>{battleManager.EnemyPartyCurrentHp}</color>/{battleManager.enemyPartyMaxHp}";
    }

}
