using UnityEngine;

public class GameController : MonoBehaviour {
	public static GameController instance;

	[SerializeField] private StartMenuUI startMenuUI;
	[SerializeField] private InGameUI inGameUI;

	void Start () {
		instance = GetComponent<GameController>();
		Instantiate(startMenuUI, new Vector3(0, 0, 0), startMenuUI.transform.rotation);
    }

	public void StartGame()
	{
		StartMenuUI.instance.Leave();
		Player.instance.StartMoving();
		Instantiate(inGameUI, new Vector3(0, 0, 0), inGameUI.transform.rotation);
	}

	public void Match(int numOfMatches){
		Player.instance.Punch(numOfMatches);
	}

	public void FinishedMoving(){
		Debug.Log("MOVE FINISH");
	}

	public void FinishedPunching(){
		Debug.Log("PUNCH FINISH");
	}
    
}
