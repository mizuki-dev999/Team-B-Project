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
    public GameObject stopSelectCharacterPanel;
    public SelectCharacter[] mySelectCharacters;
    public SelectCharacter[] enemySelectCharacters;

    public Image[] handIconImages;
    public Sprite gooSprite;
    public Sprite chokiSprite;
    public Sprite paaSprite;
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
    public CanvasGroup myBattleCharacterCardCanvasGroup;
    public GameObject myHandSkillPanel;
    public GameObject[] myHandSkillGameObjects;
    public TextMeshProUGUI[] myHandSkillTexts;
    public TextMeshProUGUI myElementDamageText;
    //相手
    public RectTransform enemyBattleCharacterCardRectTransform;
    public Image enemyBattleCharacterCardImage;
    public CanvasGroup enemyBattleCharacterCanvasGroup;
    public GameObject enemyHandSkillPanel;
    public GameObject[] enemyHandSkillGameObjects;
    public TextMeshProUGUI[] enemyHandSkillTexts;
    public TextMeshProUGUI enemyElementDamageText;

    public GameObject winLogoGameObject;
    public GameObject loseLogoGameObject;
    public GameObject drawLogoGameObject;


    

    private void Start()
    {
        turnNumberText.text = $"{battleManager.Turn}";
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
            battleManager.STATE = BattleManager.State.SelectHandState;
        });
    }
    public void InitializeSelectCharacterUI()
    {
        for(int i=0; i<battleManager.usedMyPatry.Count; i++)
        {
            mySelectCharacters[battleManager.usedMyPatry[i]].coverImageGameObject.SetActive(true);
            enemySelectCharacters[battleManager.usedEnemyParty[i]].coverImageGameObject.SetActive(true);
            switch (i)
            {
                case 0:
                    mySelectCharacters[battleManager.usedMyPatry[i]].orderNumberText.text = "1st";
                    enemySelectCharacters[battleManager.usedEnemyParty[i]].orderNumberText.text = "1st";
                    break;
                case 1:
                    mySelectCharacters[battleManager.usedMyPatry[i]].orderNumberText.text = "2nd";
                    enemySelectCharacters[battleManager.usedEnemyParty[i]].orderNumberText.text = "2nd";
                    break;
                case 2:
                    mySelectCharacters[battleManager.usedMyPatry[i]].orderNumberText.text = "3rd";
                    enemySelectCharacters[battleManager.usedEnemyParty[i]].orderNumberText.text = "3rd";
                    break;
                case 3:
                    mySelectCharacters[battleManager.usedMyPatry[i]].orderNumberText.text = "4th";
                    enemySelectCharacters[battleManager.usedEnemyParty[i]].orderNumberText.text = "4th";
                    break;
                default:
                    break;
            }
        }
    }

    //手を決めるステート--------------------------------------------------------------
    /// <summary>
    /// キャラクターカード等UIのスライドインアニメーションメソッド
    /// </summary>
    public void SlideInBattleCharacterCard()
    {
        stopSelectHandPanel.SetActive(true);

        myBattleCharacterCardRectTransform.DOAnchorPosX(150, duration);
        myBattleCharacterCardCanvasGroup.DOFade(1, duration);

        myHandSkillPanel.GetComponent<RectTransform>().DOAnchorPosX(240, duration);
        myHandSkillPanel.GetComponent<CanvasGroup>().DOFade(1, duration);

        enemyBattleCharacterCardRectTransform.DOAnchorPosX(-150, duration);
        enemyBattleCharacterCanvasGroup.DOFade(1, duration);

        enemyHandSkillPanel.GetComponent<RectTransform>().DOAnchorPosX(-240, duration);
        enemyHandSkillPanel.GetComponent<CanvasGroup>().DOFade(1, duration);

        DOVirtual.DelayedCall(duration, () => stopSelectHandPanel.SetActive(false));
    }
    /// <summary>
    /// キャラクターカード等UIのスライドアウトアニメーションメソッド＋じゃんけんステート終了処理
    /// </summary>
    public void SlideOutBattleCharacterCard()
    {
        myBattleCharacterCardRectTransform.DOAnchorPosX(300, duration);
        myBattleCharacterCardCanvasGroup.DOFade(0, duration);
        myHandSkillPanel.GetComponent<RectTransform>().DOAnchorPosX(390, duration);
        myHandSkillPanel.GetComponent<CanvasGroup>().DOFade(0, duration);

        enemyBattleCharacterCardRectTransform.DOAnchorPosX(-300, duration);
        enemyBattleCharacterCanvasGroup.DOFade(0, duration);
        enemyHandSkillPanel.GetComponent<RectTransform>().DOAnchorPosX(-390, duration);
        enemyHandSkillPanel.GetComponent<CanvasGroup>().DOFade(0, duration).OnComplete(() =>
        {
            stopSelectHandPanel.SetActive(false);
            selectHandCanvas.SetActive(false);
            if (battleManager.Turn == battleManager.GetMaxTurn()) ResetUsedCharacter();
            battleManager.Turn++;
            turnNumberText.text = $"{battleManager.Turn}";
            battleManager.STATE = BattleManager.State.SelectBattleCharacterState;
        });
    }

    public void ResetUsedCharacter()
    {
        battleManager.usedMyPatry.Clear();
        battleManager.usedEnemyParty.Clear();
        for(int i = 0; i<mySelectCharacters.Length; i++)
        {
            mySelectCharacters[i].Used = false;
            mySelectCharacters[i].coverImageGameObject.SetActive(false);
            enemySelectCharacters[i].Used = false;
            enemySelectCharacters[i].coverImageGameObject.SetActive(false);
        }
    }

    public void OnHandButton(int myHandNuber)
    {
        stopSelectHandPanel.SetActive(true);
        int enemyHandNumber = Random.Range(0, 3);
        battleManager.Judge(myHandNuber, enemyHandNumber);
        SelectedHandAnimation(myHandNuber, enemyHandNumber);
    }

    public void ChangeBattleCardImage()
    {
        myBattleCharacterCardImage.sprite = battleManager.MyBattleCharacter.characterImage;
        enemyBattleCharacterCardImage.sprite = battleManager.EnemyBattleCharacter.characterImage;
    }

    //じゃんけんの手を決めるステート------------------------------------------------------------------
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
        DOVirtual.DelayedCall(duration + 1.5f, () => SlideOutBattleCharacterCard());
    }

    public void ShowJudgeImage(GameObject logo)
    {
        logo.transform.localScale = new Vector3(0.86f, 0.86f, 1);
        logo.SetActive(true);
        logo.GetComponent<Image>().DOFade(1, 0);
        logo.transform.DOScale(1, duration).SetEase(Ease.OutBack);
        DOVirtual.DelayedCall(duration + 1.5f, () => FadeOutImage(logo));
    }

    public void FadeOutImage(GameObject logo)
    {
        logo.GetComponent<Image>().DOFade(0, duration).OnComplete(() =>
        {
            logo.SetActive(false);
        });
    }

    public void ChangeElementDamage()
    {
        myElementDamageText.text = $"{battleManager.MyBattleCharacter.handElementDamage}";
        enemyElementDamageText.text = $"{battleManager.EnemyBattleCharacter.handElementDamage}";
    }

    public void InitializeSelectHandUI()
    {
        ChangeElementDamage();

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
