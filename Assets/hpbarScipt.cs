using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class hpbarScipt : MonoBehaviour {
    [SerializeField] playerDataClass playerDataclass;
    [SerializeField] Image shieldPart;
    [SerializeField] Image HPPart;
    [SerializeField] playerContorl playerContorl;

    npcClass inContorlObjectnpcClass;

    public float HPFillAmountInBar;
    
    
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        HPBARPart();
        ShieldPart();
        /*
        switch (inContorlObjectnpcClass.WeaponP) {
            case npcClass.Weapon.sword:

                break;
            case npcClass.Weapon.axe:

                break;
            case npcClass.Weapon.rifle:

                break;
        }
        */



    }
    void HPBARPart() {
        HPFillAmountInBar = (1.0f / playerDataclass.MAXHP) * playerDataclass.HP;
        HPPart.fillAmount = HPFillAmountInBar;
    }
    void ShieldPart() {
        if (playerContorl != null) {
            inContorlObjectnpcClass = playerContorl.incontorlObj.GetComponent<npcClass>();
            if (inContorlObjectnpcClass.HP > 0.0f) {
                shieldPart.enabled = true;
            }
            else {
                shieldPart.enabled = false;
            }
        }
    }
}
