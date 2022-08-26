using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] protected Sprite[] sprites; //�A�j���[�V�����p�X�v���C�g
    [SerializeField] private protected float interval = 0.035f; //�X�v���C�g�̐ؑ֎���(�b)
    [SerializeField] protected Image image;
    private protected float deltaTime = 0;
    private protected bool active = false;
    private protected int count = 0; // �X�v���C�g

    private void Update()
    {
        if (active)
        {
            if (count == sprites.Length - 1)
            {
                StopAnimation();
                return;
            }//�Ō�܂ōĐ�������X�g�b�v
            deltaTime += Time.deltaTime;
            if (deltaTime >= interval) NextAnimation();
        }
    }
    public void BeginAnimation(Sprite[] sprites) //�X�L���A�C�R�����ŌĂяo��
    {
        this.gameObject.SetActive(true);
        count = 0;
        this.sprites = sprites;
        image.sprite = this.sprites[count];
        active = true;
    }

    public void BeginAnimation() //�X�L���A�C�R�����ŌĂяo��
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
