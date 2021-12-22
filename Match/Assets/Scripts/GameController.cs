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
		Player.instance.StartPunching(numOfMatches);
		
	}

	public void FinishedMoving(){
		Board.instance.setDisable(false);
	}

	public void PlayerFinishedPunching(){
		CurrentEnemy.Punch();
	}

	public void EnemyFinishedPunching(){
		Board.instance.setDisable(false);
	}

	public void EnemyIsDying(){
		Player.instance.StopPunching();
	}

	public void EnemyDied(){
		CurrentEnemy = Environment.instance.CurrentEnemy();
		if(CurrentEnemy == null){
			InGameUI.instance.Vanish();
			Player.instance.Victory();
		}
		else{
			Player.instance.StartMoving();
			InGameUI.instance.SetHealth();
		}
	}
    
}
