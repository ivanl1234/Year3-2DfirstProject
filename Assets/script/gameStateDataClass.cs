using UnityEngine;
using System.Collections;

public class gameStateDataClass : MonoBehaviour {

    public enum gameState { menu,game,pause,gameover };
    public gameState gamestate;

    public enum gameWinCondition { reciprocal , goal };
    public gameWinCondition gameWin = gameWinCondition.goal;

    public float reciprocalTime;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
