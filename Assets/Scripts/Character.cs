using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public int id;
    public new string name;
    public Sprite characterImage;
    public enum HandElement
    {
        Goo, //�O�[
        Choki, //�`���L
        Paa, //�p�[
    }
    public HandElement handElement;
    public List<Skill> skills = new();

    [System.Serializable]
    public class Skill
    {
        public enum Hand
        {
            Goo, //�O�[
            Choki, //�`���L
            Paa //�p�[
        }
        public Hand hand;
        public int damage;
    }
}
