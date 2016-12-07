using UnityEngine;
using System.Collections.Generic;
using myFunction;

public class selectEnemySystemScript : GameFunction
{
    playerContorl playercontorl;

    [HideInInspector]
    public GameObject[] GB; //�ؼЩҦb��array
    [HideInInspector]
    public GameObject playerSelectPointerSystem; //���oselectPointerMovement�{�� ���e
    [SerializeField]
    public GameObject selectPointerUIpart;

    [SerializeField]
    [HideInInspector]
    gameStateDataClass gameStateDataClass;

    [SerializeField][HideInInspector]
    Transform pointer;  //�H���ӹϧ@?Ew �}�l��̪�?EE
    [SerializeField][HideInInspector]
    pointerTrigger OnTriggerEnter2DCircle;
    [SerializeField]
    public float slowMotionTimeScale;
    [SerializeField]
    public float timeToComplete;
    [SerializeField]
    public int controlHpCost;


    GameObject[] TriggerArray;

    public GameObject targetGameObj;  //���O��ʩ�i�h

    GameObject lockDownTargetGO;

    Function myfunction = new Function();
    int selectTaget = 1;

    float[] eachEnemylerpFloat;

    int target = -1;
    float timerCount = 0.0f;

    public bool openTargetLockDown = false;

    // Use this for initialization
    void Start() {
        playercontorl = GetComponent<playerContorl>();
        TriggerArray = OnTriggerEnter2DCircle.TriggerList.ToArray();

        playerSelectPointerSystem.SetActive(false);
        selectPointerUIpart.SetActive(false);
        GB = getallenemyWithoutContorlOnce();
    }
    
    void startTargetLockDown(Vector3 centerObjectPosition, int targetNum) {
        #region �i: ?�S�y,��?��?�S�y���ڕW �o:targetGameObj
        GB = TriggerArray; //�ϥΨ���enemyArray?

        if (GB.Length > 0) { //TriggerArray�����F?E
            eachEnemylerpFloat = new float[GB.Length];  //�Ʀ��C�ӼĤH?EEw�ۮt�h��?
            short i = 0;
            foreach (GameObject each in GB) {
               // each.GetComponent<SpriteRenderer>().color = Color.white;
                eachEnemylerpFloat[i] = Vector3.Distance(centerObjectPosition, each.GetComponent<Transform>().position);
                i++;
            }

            target = myfunction.findSmallestOfBigestNumberInArray(eachEnemylerpFloat, false, targetNum);  //?�oeachEnemylerpFloat array?���ŏ���?

            targetGameObj = GB[target]; //��ؼЧ�X��
            //targetGameObj.GetComponent<SpriteRenderer>().color = Color.red;

            foreach (GameObject each in getallenemyWithoutContorlOnce()) {  //��?EҦ�enemy���C?Ewithout targetgame  �i��i��
                if (each.GetInstanceID() != targetGameObj.GetInstanceID() ) {
                    //each.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }

        }
        else {
            foreach (GameObject each in getallenemyWithoutContorlOnce()) {  //��?EҦ�enemy���C?E
                //each.GetComponent<SpriteRenderer>().color = Color.white;

            }
        }


        //openTargetLockDown = true;
        #endregion
    }


    public void setupTargetLockDown() {  //
        transform.rotation = Quaternion.Euler(0,0,0);
        Time.timeScale = slowMotionTimeScale;  //���C��ӹC���t��
        //playercontorl.incontorlObj.GetComponent<playerMove>().enabled = false;
        targetGameObj = null;

        playerSelectPointerSystem.SetActive(true);  //�}��?Eܶ�E
        selectPointerUIpart.SetActive(true);
        GB = TriggerArray;
        openTargetLockDown = true;
    }

    public void cancelTargetLockDown() {  //����function
        //GB = TriggerArray;
        Time.timeScale = 1f;
        //playercontorl.incontorlObj.GetComponent<playerMove>().enabled = true;
        playerSelectPointerSystem.SetActive(false);
        selectPointerUIpart.SetActive(false);
        openTargetLockDown = false;

        if (targetGameObj!=null) {
            //targetGameObj.GetComponent<SpriteRenderer>().color = Color.white;
        }

        target = -1;


    }

    // Update is called once per frame
    void Update() {

        if (gameStateDataClass.gamestate != gameStateDataClass.gameState.pause && playercontorl.incontorlObj) {
            if (Input.GetButtonDown("OpenCloseControlPreview")) {
                if (openTargetLockDown) {  //��?EEܱ���E
                    cancelTargetLockDown();
                }
                else {  //�}�l?Eܱ���E
                    playerDataClass playerData = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<playerDataClass>();
                    if (playerData.HP - controlHpCost > 0) { //�v����hp�˔\ᢓ�
                        playerData.HP -= controlHpCost;
                        setupTargetLockDown();
                        timerCount = 0;
                    }

                }
            }

        }


        if (openTargetLockDown) {
            TriggerArray = OnTriggerEnter2DCircle.TriggerList.ToArray();
            Vector3 pointerV3 = pointer.position;
            startTargetLockDown(pointerV3, 0);


            if (timerCount <= timeToComplete * slowMotionTimeScale ) { //?�Ԍv�Z�n? �ߎZ�islowMotion�I?�ԍ����
                timerCount += Time.deltaTime;
            }
            else { //�ߗ�?�ԏA�������
                cancelTargetLockDown();
            }

        }

        #region �E��code
        /*
        if (Input.GetKeyDown(KeyCode.LeftArrow) && openTargetLockDown) {
            if (selectTaget == 0) {
                selectTaget = eachEnemylerpFloat.Length - 1;
            }
            else {
                selectTaget--;
            }
            startTargetLockDown(getLeftestPointer.transform.position,selectTaget);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)&& openTargetLockDown) {



            if (eachEnemylerpFloat.Length-1 == selectTaget) {
                selectTaget = 0;
            }
            else {
                selectTaget++;
            }
            startTargetLockDown(getLeftestPointer.transform.position, selectTaget);
        }
        */

        //Debug.Log(selectTaget);

        /*
        if (Input.GetKeyUp(KeyCode.U)) {
            if (eachEnemylerpFloat.Length == selectTaget) {
                selectTaget = 0;
            }
            else {
                selectTaget++;
            }

        }
        */


        //Debug.Log(eachEnemylerpFloat[0]);




        //Debug.Log(myfunction.findSmallestOfBigestNumberInArray(testfloat, false,3));

        //�}�o�@�ӥ��k�s��?E����{�b�ؼг̪񪺥t�@���Ǫ�

        #endregion
    }
}