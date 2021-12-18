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
		Object.Destroy(GameObject.Find("InGameUI"));
		Instantiate(inGameUI, new Vector3(0, 0, 0), inGameUI.transform.rotation);
	}
    
}
