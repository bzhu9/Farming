using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Plant", menuName ="Plant")]
public class PlantObject : ScriptableObject
{   
    //change
    public string plantName;
    public Sprite[] plantStages;
    public float timeBetweenStages;
    public int price;
    public Sprite icon;
}

