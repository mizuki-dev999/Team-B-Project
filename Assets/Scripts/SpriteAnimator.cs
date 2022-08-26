using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] protected Sprite[] sprites; //アニメーション用スプライト
    [SerializeField] private protected float interval = 0.035f; //スプライトの切替時間(秒)
    [SerializeField] protected Image image;
    private protected float deltaTime = 0;
    private protected bool active = false;
    private protected int count = 0; // スプライト

    private void Update()
    {
        if (active)
        {
            if (count == sprites.Length - 1)
            {
                StopAnimation();
                return;
            }//最後まで再生したらストップ
            deltaTime += Time.deltaTime;
            if (deltaTime >= interval) NextAnimation();
        }
    }
    public void BeginAnimation(Sprite[] sprites) //スキルアイコン側で呼び出す
    {
        this.gameObject.SetActive(true);
        count = 0;
        this.sprites = sprites;
        image.sprite = this.sprites[count];
        active = true;
    }

    public void BeginAnimation() //スキルアイコン側で呼び出す
    {
        this.gameObject.SetActive(true);
        count = 0;
        image.sprite = this.sprites[count];
        active = true;
    }
    private protected void NextAnimation()
    {
        count++;
        image.sprite = sprites[count];
        deltaTime = 0;
    }
    private protected void StopAnimation()
    {
        active = false;
        this.gameObject.SetActive(false);
    }
}
