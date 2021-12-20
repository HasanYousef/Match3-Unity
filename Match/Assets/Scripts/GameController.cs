using UnityEngine;

public class GameController : MonoBehaviour {
	public static GameController instance;

	[SerializeField] private StartMenuUI startMenuUI;
	[SerializeField] private InGameUI inGameUI;

	public Enemy CurrentEnemy;

	void Start () {
		instance = GetComponent<GameController>();
		Instantiate(startMenuUI, new Vector3(0, 0, 0), startMenuUI.transform.rotation);
    }

	public void StartGame()
	{
		StartMenuUI.instance.Leave();
		Player.instance.StartMoving();
		Instantiate(inGameUI, new Vector3(0, 0, 0), inGameUI.transform.rotation);
		CurrentEnemy = Environment.instance.CurrentEnemy();
	}

	public void Match(int numOfMatches){
		Board.instance.setDisable(true);
		Player.instance.Punch(numOfMatches);
	}

	public void FinishedMoving(){
		Board.instance.setDisable(false);
	}

	public void FinishedPunching(){
		Board.instance.setDisable(false);
	}
    
}
