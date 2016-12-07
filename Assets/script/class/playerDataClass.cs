using UnityEngine;
using System.Collections;

public class playerDataClass : MonoBehaviour {
    public int playerSouls { get; set; }
    [SerializeField]
    public float HP = 0.0f;
    [SerializeField]
    public int MAXHP = 0;

    public playerDataClass() {
        playerSouls = 0;
    }



}
