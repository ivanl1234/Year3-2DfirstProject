using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class circularProgress : MonoBehaviour {
    selectEnemySystemScript selectEnemySystemScript;
    [SerializeField] Image imageRadial;
    // Use this for initialization
    void OnEnable() {
        selectEnemySystemScript = GetComponentInParent<Transform>().GetComponentInParent<selectEnemySystemScript>();

        //Use this to Start 
        float timeToComplete = selectEnemySystemScript.timeToComplete * selectEnemySystemScript.slowMotionTimeScale;
        StartCoroutine(RadialProgress(timeToComplete));
        imageRadial.fillAmount = 1;
    }
IEnumerator RadialProgress(float time) {
        float rate = 1 / time;
        float i = 1;
        while (i > 0) {
            i -= Time.deltaTime * rate;

            imageRadial.fillAmount = i;
            //GetComponent<Renderer>().material.SetFloat("_Cutoff", i);
            yield return 0;
        }
    }
}
