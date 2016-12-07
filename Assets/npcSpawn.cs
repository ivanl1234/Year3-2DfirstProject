using UnityEngine;
using System.Collections;

public class npcSpawn : MonoBehaviour {
    bool spawnLock = false;
    [SerializeField]
    private GameObject npcPrefabs;
    [SerializeField]
    private float perSecond;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!spawnLock) {
            StartCoroutine(spawnTimer());
        }
        
    }

    IEnumerator spawnTimer() {

        spawnLock = true;
        StartCoroutine(spawnBullet(npcPrefabs ));
        yield return new WaitForSeconds(perSecond);
        spawnLock = false;
    }

    IEnumerator spawnBullet(GameObject prefabs) {
        GameObject PF = Instantiate(prefabs, transform.position, transform.rotation) as GameObject;
        PF.GetComponent<npcClass>().attackStateP = npcClass.attackState.guard;
        yield break;
        //yield return new WaitForSeconds(5);
        //Destroy(PF);
        //StopCoroutine("spawnBullet");
    }


}
