using UnityEngine;
using System.Collections;

public class winCheck : GameFunction
{
    gameStateDataClass gameStateDataClass;
    [SerializeField]
    private GameObject GoalPoint ;

	// Use this for initialization
	void Start () {
        gameStateDataClass = GetComponent<gameStateDataClass>();
        if (gameStateDataClass.gameWin == gameStateDataClass.gameWinCondition.reciprocal) {
            StartCoroutine(reciprocal());
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator reciprocal(  ) {

        yield return new WaitForSeconds(20);

        if (gameStateDataClass.gamestate != gameStateDataClass.gameState.gameover) {
            OnPlayerWin();
            gameStateDataClass.gamestate = gameStateDataClass.gameState.gameover;
        }

    }

}
