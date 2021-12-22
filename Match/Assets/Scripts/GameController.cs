using UnityEngine;

public class GameController : MonoBehaviour {
	public static GameController instance;

	[SerializeField] private StartMenuUI startMenuUI;
	[SerializeField] private EndMenuUI endMenuUI;
	[SerializeField] private InGameUI inGameUI;
	[SerializeField] private Environment[] Environments;
	private int Level = 0;
	private Environment CurrentEnvironment;

	public Enemy CurrentEnemy;

	void Start () {
		instance = GetComponent<GameController>();
		Instantiate(startMenuUI, new Vector3(0, 0, 0), startMenuUI.transform.rotation);
		CurrentEnvironment = Instantiate(Environments[Level], new Vector3(0, -0.32f, -3.5f), Quaternion.Euler(-22f, 0, 0));
    }

	public void StartGamePlease()
	{
		GameController.instance.StartGame();
	}

	public void NextLevelPlease()
	{
		GameController.instance.NextLevel();
	}

	private void StartGame()
	{
		StartMenuUI.instance.Leave();
		StartLevel();
	}

	private void NextLevel()
	{
		Level++;
		if(Level >= Environments.Length) Level = 0;
		CurrentEnvironment.GoFuckYourSelf();
		CurrentEnvironment = Instantiate(Environments[Level], new Vector3(0, -0.32f, -3.5f), Quaternion.Euler(-22f, 0, 0));
		EndMenuUI.instance.Leave();
		StartLevel();
	}

	private void StartLevel(){
		CurrentEnemy = CurrentEnvironment.CurrentEnemy();
		Player.instance.StartMoving();
		Instantiate(inGameUI, new Vector3(0, 0, 0), inGameUI.transform.rotation);
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
		CurrentEnemy = CurrentEnvironment.CurrentEnemy();
		if(CurrentEnemy == null){
			InGameUI.instance.Vanish();
			Player.instance.Victory();
			Instantiate(endMenuUI, new Vector3(0, 0, 0), endMenuUI.transform.rotation);
		}
		else{
			Player.instance.StartMoving();
			InGameUI.instance.SetHealth();
		}
	}

	public Environment GetCurrentEnvironment(){
		return CurrentEnvironment;
	}
    
}
