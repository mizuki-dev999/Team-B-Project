using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public BattleManager battleManager;
    public float duration = 0.3f;
    [Header("キャラクター選択時用キャラクター詳細UI")]
    public GameObject selectCharacterCanvas;
    public RectTransform myPartyCardPanelRectTransform;
    public CanvasGroup myPartyCardPanelCanvasGroup;
    public RectTransform enemyPartyCardPanelRectTransform;
    public CanvasGroup enemyPartyCardPanelCanvasGroup;
    //キャラクターの編成画像
    public Image[] myPartyImages;
    public Image[] enemyPartyImages;
    public GameObject skillInformationPanel;
    public TextMeshProUGUI skillInformationPanelNameText;
    public TextMeshProUGUI[] skillInformationPanelHandTexts;
    public TextMeshProUGUI[] skillInformationPanelDamageTexts;
    public GameObject stopSelectCharacterPanel;
    [Header("トップUI")]
    public TextMeshProUGUI turnNumberText;
    public TextMeshProUGUI myHpValueText;
    public TextMeshProUGUI myEnemyValueText;
    public Image myHpBar;
    public Image enemyHpBar;
    [Header("手選択UI")]
    public GameObject selectHandCanvas;
    public GameObject stopSelectHandPanel;
    //自分
    public RectTransform myBattleCharacterCardRectTransform;
    public Image myBattleCharacterCardImage;
    public GameObject myHandSkillPanel;
    public GameObject[] myHandSkillGameObjects;
    public TextMeshProUGUI[] myHandSkillTexts;
    //相手
    public RectTransform enemyBattleCharacterCardRectTransform;
    public Image enemyBattleCharacterCardImage;
    public GameObject enemyHandSkillPanel;
    public GameObject[] enemyHandSkillGameObjects;
    public TextMeshProUGUI[] enemyHandSkillTexts;

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

    //キャラクターを決めるステート--------------------------------------------------------------
    public void PointerDownAnimation(RectTransform target)
    {
        target.DOScale(new Vector3(0.96f, 0.96f, 1), 0.08f);
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

    public void SlideInPartyCharacterCard()
    {
        stopSelectCharacterPanel.SetActive(true);
        myPartyCardPanelRectTransform.DOAnchorPosX(-30, duration);
        myPartyCardPanelCanvasGroup.DOFade(1, duration);
        enemyPartyCardPanelRectTransform.DOAnchorPosX(30, duration);
        enemyPartyCardPanelCanvasGroup.DOFade(1, duration).OnComplete(() =>
        {
            stopSelectCharacterPanel.SetActive(false);
        });
    }

    public void SlideOutPartyCharacterCard()
    {
        stopSelectCharacterPanel.SetActive(true);
        myPartyCardPanelRectTransform.DOAnchorPosX(100, duration);
        myPartyCardPanelCanvasGroup.DOFade(0, duration);
        enemyPartyCardPanelRectTransform.DOAnchorPosX(-100, duration);
        enemyPartyCardPanelCanvasGroup.DOFade(0, duration).OnComplete(() =>
        {
            stopSelectCharacterPanel.SetActive(false);
            selectCharacterCanvas.SetActive(false);
            battleManager._state = BattleManager.State.SelectHandState;
        });
    }
    public void InitializeSelectCharacterUI()
    {
        
    }

    //手を決めるステート--------------------------------------------------------------
    public void SlideInBattleCharacterCard()
    {
        myBattleCharacterCardRectTransform.DOAnchorPosX(150, duration);
        myBattleCharacterCardImage.DOFade(1, duration);

        myHandSkillPanel.GetComponent<RectTransform>().DOAnchorPosX(240, duration);
        myHandSkillPanel.GetComponent<CanvasGroup>().DOFade(1, duration);

        enemyBattleCharacterCardRectTransform.DOAnchorPosX(-150, duration);
        enemyBattleCharacterCardImage.DOFade(1, duration);

        enemyHandSkillPanel.GetComponent<RectTransform>().DOAnchorPosX(-240, duration);
        enemyHandSkillPanel.GetComponent<CanvasGroup>().DOFade(1, duration);
    }

    public void SlideOutBattleCharacterCard()
    {
        myBattleCharacterCardRectTransform.DOAnchorPosX(300, duration);
        myHandSkillPanel.GetComponent<RectTransform>().DOAnchorPosX(390, duration);
        myHandSkillPanel.GetComponent<CanvasGroup>().DOFade(0, duration);
        enemyBattleCharacterCardRectTransform.DOAnchorPosX(-300, duration);
        enemyHandSkillPanel.GetComponent<RectTransform>().DOAnchorPosX(-390, duration);
        enemyHandSkillPanel.GetComponent<CanvasGroup>().DOFade(0, duration);
    }

    public void OnHandButton(int myHandNuber)
    {
        int enemyHandNumber = Random.Range(0, 3);
        battleManager.Judge(myHandNuber, enemyHandNumber);
        SelectedHandAnimation(myHandNuber, enemyHandNumber);
    }

    public void ChangeBattleCardImage()
    {
        myBattleCharacterCardImage.sprite = battleManager.MyBattleCharacter.characterImage;
        enemyBattleCharacterCardImage.sprite = battleManager.EnemyBattleCharacter.characterImage;
    }

    public void ChangeHandUI()
    {
        for(int i = 0; i<myHandSkillGameObjects.Length; i++)
        {
            myHandSkillTexts[i].text = $"{battleManager.MyBattleCharacter.skills[i].hand}\n{battleManager.MyBattleCharacter.skills[i].damage}";
        }
        for (int i = 0; i < enemyHandSkillGameObjects.Length; i++)
        {
            enemyHandSkillTexts[i].text = $"{battleManager.EnemyBattleCharacter.skills[i].hand}\n{battleManager.EnemyBattleCharacter.skills[i].damage}";
        }
    }

    public void SelectedHandAnimation(int myHandNummber, int enemyHandNumber)
    {
        for(int i=0; i< myHandSkillGameObjects.Length; i++)
        {
            if (i == myHandNummber) myHandSkillGameObjects[i].GetComponent<RectTransform>().DOAnchorPosX(0, duration);
            else myHandSkillGameObjects[i].SetActive(false);
        }
        for (int i = 0; i < enemyHandSkillGameObjects.Length; i++)
        {
            if (i == enemyHandNumber) enemyHandSkillGameObjects[i].GetComponent<RectTransform>().DOAnchorPosX(0, duration);
            else enemyHandSkillGameObjects[i].SetActive(false);
        }
        DOVirtual.DelayedCall(duration+1, () => SlideOutBattleCharacterCard()).OnComplete(() =>
        {
            stopSelectHandPanel.SetActive(false);
            selectHandCanvas.SetActive(false);
            battleManager.Turn++;
            turnNumberText.text = $"{battleManager.Turn}/{battleManager.maxTurn}";
            battleManager._state = BattleManager.State.SelectBattleCharacterState;
        });
    }

    public void InitializeSelectHandUI()
    {
        myHandSkillGameObjects[0].SetActive(true);
        myHandSkillGameObjects[0].transform.localPosition = new Vector2(-130, 0);
        myHandSkillGameObjects[1].SetActive(true);
        myHandSkillGameObjects[1].transform.localPosition = new Vector2(0, 0);
        myHandSkillGameObjects[2].SetActive(true);
        myHandSkillGameObjects[2].transform.localPosition = new Vector2(130, 0);

        enemyHandSkillGameObjects[0].SetActive(true);
        enemyHandSkillGameObjects[0].transform.localPosition = new Vector2(-130, 0);
        enemyHandSkillGameObjects[1].SetActive(true);
        enemyHandSkillGameObjects[1].transform.localPosition = new Vector2(0, 0);
        enemyHandSkillGameObjects[2].SetActive(true);
        enemyHandSkillGameObjects[2].transform.localPosition = new Vector2(130, 0);
    }
}
