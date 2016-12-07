using UnityEngine;
using System.Collections;

public class voidKillZoneCode : GameFunction {

    short damage = 999;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "enemy") {
            other.gameObject.GetComponent<npcScript>().npcHPCheck(damage,"void");
        }
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<npcScript>().npcHPCheck(damage, "void");
            //Gfunction.OnPlayerGameOver();
        }

    }
}
