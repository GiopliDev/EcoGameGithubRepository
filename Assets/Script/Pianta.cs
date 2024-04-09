using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Pianta")]
public class Pianta : ScriptableObject

{
    private float whenLastGet = 0f;
    public string name;
    public Sprite[] fases = new Sprite[4]; //icona,fase1,fase2,fase3
    public int IDgrowth=0;
    public float progressNeeded;
    public int progress;
    
}
