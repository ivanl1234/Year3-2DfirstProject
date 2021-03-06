﻿using UnityEngine;
using System.Collections;
using Spine.Unity;
using myFunction;

public class npcScript : GameFunction
{
    npcClass npcclass;
    playerDataClass playerData;

    bool IsAlreadyStartDeadFunction = false;
    bool inDeadHitFly = false;

    [SerializeField]
    private Transform GroundCheck1;
    [SerializeField]
    public  Transform GroundCheckWall1;
    [SerializeField]
    public  Transform GroundCheckWall2;
    [SerializeField]
    public LayerMask groundLayer;

    Rigidbody2D rigidbody2d;

    [SerializeField]
    public SkeletonAnimation thisAnimation;

    [SerializeField]
    public Object hpParticlePrefab;
    // Use this for initialization
    void Start () {
        

        playerData = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<playerDataClass>(); //
        rigidbody2d = GetComponent<Rigidbody2D>();
        npcclass = GetComponent<npcClass>();
        npcclass.npcClassSetUp();

        GameObject[] gameObj = GameObject.FindGameObjectsWithTag("enemy");  //無視其他enemy碰撞
        foreach (GameObject each in gameObj) {
            Physics2D.IgnoreCollision(each.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(each.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
            Physics2D.IgnoreCollision(each.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(each.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        }

        GameObject[] HPpartaclegameObj = GameObject.FindGameObjectsWithTag("HPpartacle");  //無視其他enemy碰撞
        foreach (GameObject each in HPpartaclegameObj)
        {
            Physics2D.IgnoreCollision(each.GetComponent<Collider2D>(), GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(each.GetComponent<Collider2D>(), GetComponent<CircleCollider2D>());
        }

    }
	
	// Update is called once per frame
	void Update () {
        attackStateChangeCodeFunction();
        npcColorSetting();
        movementStateCheck();
        NpcDead();
        if (npcclass.TypeP == npcClass.Type.contorl) {
            Debug.Log(thisAnimation.AnimationState);
        }


    }

    void attackStateChangeCodeFunction() {
        #region  進入不同攻擊狀態時的程式啟用
        if (npcclass.TypeP == npcClass.Type.normal && IsAlreadyStartDeadFunction == false) {  //npc是自已控制,非由玩家控制
            //GetComponent<playerMove>().enabled = false;
            switch (npcclass.attackStateP) {
                case npcClass.attackState.alert:

                    break;
                case npcClass.attackState.attack:


                    if (GetComponentInChildren<attackSystem>() != null && !GetComponentInChildren<attackSystem>().enabled) {
                        GetComponentInChildren<attackSystem>().enabled = true;
                    }

                    break;
                case npcClass.attackState.guard:
                    if (!GetComponent<npcMove>().enabled) {
                        GetComponent<npcMove>().enabled = true;
                    }
                    if (GetComponentInChildren<attackSystem>() != null && GetComponentInChildren<attackSystem>().enabled) {
                        GetComponentInChildren<attackSystem>().enabled = false;
                    }
                    break;
                case npcClass.attackState.stand:
                    if (GetComponentInChildren<attackSystem>() != null && GetComponentInChildren<attackSystem>().enabled) {
                        GetComponentInChildren<attackSystem>().enabled = false;
                    }
                    break;
            }


        }
        else {
            //GetComponent<playerMove>().enabled = true;
            //GetComponent<npcMove>().enabled = false;
            //GetComponent<attackSystem>().enabled = true;
        }
        #endregion
    }
    
    void movementStateCheck() {
        #region npc的move狀態定位
        if ((Physics2D.OverlapCircle(GroundCheck1.position, 0.35f, groundLayer)) && ((rigidbody2d.velocity.x > 0.1 ) || (rigidbody2d.velocity.x < -0.1 ))) {
            npcclass.movementStateP = npcClass.movementState.walking;

            thisAnimation.loop = true;
            thisAnimation.AnimationName = "run_sword";
            thisAnimation.timeScale = 1f;
          
        }
        else if (rigidbody2d.velocity.y > 0.1 && !(Physics2D.OverlapCircle(GroundCheck1.position, 0.35f, groundLayer)) ) {

                npcclass.movementStateP = npcClass.movementState.jumpingBothCanMove;
            GetComponent<Rigidbody2D>().gravityScale = 3.0f;
            thisAnimation.loop = false;
            thisAnimation.AnimationName = "jump_sword";
            thisAnimation.timeScale = 1f;


        }
        else if (rigidbody2d.velocity.y < -0.05 && !(Physics2D.OverlapCircle(GroundCheck1.position, 0.35f, groundLayer)) ) {
            npcclass.movementStateP = npcClass.movementState.falling;
            //thisAnimation.AnimationName = "_sword";
            thisAnimation.timeScale = 1f;
        }
        else if (Physics2D.OverlapCircle(GroundCheck1.position, 0.15f, groundLayer)) {
            npcclass.movementStateP = npcClass.movementState.landed;
            GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            thisAnimation.loop = true;
            thisAnimation.AnimationName = "sword_idel_single_hand";
            thisAnimation.timeScale = 1f;

        }
        #endregion
    }

    void npcColorSetting() {
        #region 顏色設定，但放在這裡不會是一個好主意
        if (npcclass.TypeP == npcClass.Type.contorl) {
            GetComponentInChildren<SkeletonAnimation>().skeleton.SetColor(Color.red);
        }
        else {
            GetComponentInChildren<SkeletonAnimation>().skeleton.SetColor(Color.white);
        }
        #endregion
    }

    public void npcHPCheck(short DamageDeal ,string dealerStringType) {
        #region HP


        if (dealerStringType == "player") { //是誰打出這個傷害
            npcclass.HP = 0;
        }
        else {

        }

        npcclass.HP -= DamageDeal;
        if (npcclass.HP <= 0 ) {
            npcclass.liveStateP = npcClass.liveState.dead;
            if (dealerStringType == "player") { //是誰打出這個傷害
                //playerData.HP = playerData.MAXHP;
            }

            //Destroy(gameObject);
            
            if (npcclass.TypeP == npcClass.Type.contorl) {
               if (playerData.HP - DamageDeal <= 0) {//玩家控制並玩家已經沒有hp
                    Time.timeScale = 1f;
                    playerData.HP = 0.0f;
                    npcclass.TypeP = npcClass.Type.normal;
                    OnPlayerGameOver(); //player gameover  這堆code應該放在player
                    gameStateDataClass gameState = GameObject.FindGameObjectsWithTag("backgroundScipt")[0].GetComponent<gameStateDataClass>();
                    gameState.gamestate = gameStateDataClass.gameState.gameover;
                }
                else {//玩家控制並玩家還有hp
                    playerData.HP -= DamageDeal;
                }
            }

        }
        #endregion
    }

    void NpcDead() { //do once
        #region npc死亡時做的程式
        if (npcclass.liveStateP == npcClass.liveState.dead && npcclass.TypeP == npcClass.Type.normal) { //當死亡時
             if (!IsAlreadyStartDeadFunction) { //當死亡時只做一次
                IsAlreadyStartDeadFunction = true;

                spawnHPParticle(10);
                GetComponent<npcMove>().enabled = false;
                GetComponentInChildren<attackSystem>().enabled = false;

                npcclass.movementStateP = npcClass.movementState.jumpingBothCanMove;//強制鎖定為跳躍  處理死亡時往後擊飛
                inDeadHitFly = true;
                if (transform.localScale.x > 0f) { //大過0面向左邊,反則右邊
                    rigidbody2d.velocity = new Vector2(2.5f, 10);
                }
                else {
                    rigidbody2d.velocity = new Vector2(-2.5f, 10);
                }


            }
            else {  //當死亡時就一直做
                if (npcclass.movementStateP == npcClass.movementState.landed && inDeadHitFly) { //landed時就毀掉自己
                    Destroy(gameObject);
                    playerData.playerSouls += npcclass.souls; //玩家靈魂增加
                }

            }

        }
        #endregion
    }


    void spawnHPParticle(int Number)
    {
        Function myfunction = new Function();
        while (Number >= 0)
        {
            GameObject thisHPparticle = Instantiate(hpParticlePrefab,transform.position,Quaternion.identity) as GameObject;
            Rigidbody2D rigid = thisHPparticle.GetComponent<Rigidbody2D>();
            SpriteRenderer spriteRend = thisHPparticle.GetComponent<SpriteRenderer>();
            hpParticleScript ParticleScript = thisHPparticle.GetComponent<hpParticleScript>();
            rigid.velocity = new Vector2(myfunction.RandomNumber(12)-4, myfunction.RandomNumber(10));
            spriteRend.color = new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, (myfunction.RandomNumber(4)+3)/10f);
            ParticleScript.playerData = playerData;
            Number--;
        }
    }
}
