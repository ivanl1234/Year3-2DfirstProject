using UnityEngine;
using System.Collections;

public class attackSystem : MonoBehaviour {
    selectEnemySystemScript selectEnemySystem;
    npcClass npcclass;

    public float CD = 0.5F;
    bool attackCDLock = false;
    public GameObject attackGO;
    public gunSpawn gunspawn;

    bool attacksensor = false;
    public float attackSensorCD = 0.2F;
	// Use this for initialization
	void Start () {
        npcclass = GetComponentInParent<npcClass>();
        if (GameObject.FindGameObjectsWithTag("megumin_player").Length != 0) {
            selectEnemySystem = GameObject.FindGameObjectsWithTag("megumin_player")[0].GetComponent<selectEnemySystemScript>(); //
        }

        
    }
	
	// Update is called once per frame
	void Update () {
        if (npcclass.TypeP == npcClass.Type.contorl) { //player attack
            if (Input.GetMouseButtonUp(1) && !attackCDLock && !selectEnemySystem.openTargetLockDown) {  //玩家按下攻擊
                StartCoroutine("attackFunction");
                
            }

        }
        else {
            if ( !attackCDLock ) {
                StartCoroutine("attackFunction");
            }
        }

        
	
	}

    IEnumerator attackFunction() {
        if (!attackCDLock) {
            attackCDLock = true;
            if (attackGO != null) {
                StartCoroutine("spawnAttackSensorFunction");

                //velocityPart
                Rigidbody2D rigid2d = GetComponentInParent<Rigidbody2D>();
                rigid2d.velocity -= new Vector2(4.0f * rigid2d.transform.localScale.x, 0);
                //rigid2d.transform.position += transform.right * Time.deltaTime * 100;

            }
            else {
                gunspawn.Shot();
            }
            
        }

        
        yield return new WaitForSeconds(CD);
        attackCDLock = false;
    }

    IEnumerator spawnAttackSensorFunction() {
        if (!attacksensor) {
            attacksensor = true;
            attackGO.SetActive(attacksensor);
        }

        yield return new WaitForSeconds(attackSensorCD);
        attacksensor = false;
        attackGO.SetActive(attacksensor);
    }

    }
