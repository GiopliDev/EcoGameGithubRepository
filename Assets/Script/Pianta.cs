using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Pianta")]
public class Pianta : ScriptableObject

{
    private float whenLastGet = 0f;
    public string nome;
    public Sprite[] fasi = new Sprite[4]; //icona,fase1,fase2,fase3
    public int IDCrescita=0;
    public float progressNeeded;
    public int progress;
    
}
