using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PrizeData", menuName = "CustomObjects/PrizeData", order = 2)]
public class PrizeData : ScriptableObject
{
    public string Name = null;
    public int Amount = 0;
    public Sprite Image = null;
}
