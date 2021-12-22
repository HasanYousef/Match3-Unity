using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
	public static InGameUI instance;
    private Text PlayerHealth;
    private Image PlayerHealthBar;
    private Text EnemyHealth;
    private Image EnemyHealthBar;
    private float OriginalBarWidth;
    private bool IsVanishing = false;

	void Start () {
		instance = GetComponent<InGameUI>();
        this.name = "InGameUI";
        PlayerHealth = GameObject.Find("PlayerHealth").GetComponent<Text>();
        EnemyHealth = GameObject.Find("EnemyHealth").GetComponent<Text>();
        PlayerHealthBar = GameObject.Find("PlayerHealthBar").GetComponent<Image>();
        EnemyHealthBar = GameObject.Find("EnemyHealthBar").GetComponent<Image>();
        OriginalBarWidth = PlayerHealthBar.rectTransform.rect.width;
        SetHealth();
    }

    public void Vanish(){
        Destroy(gameObject);
    }

    public void SetHealth(){
        float EHP = GameController.instance.CurrentEnemy.GetHealth();
        UpdatePlayerHealth(Player.instance.GetHealth(), Player.instance.GetMaxHealth());
        UpdateEnemyHealth(EHP, EHP);
    }

    public void UpdatePlayerHealth(float newHealth, float maxHealth){
        PlayerHealth.text = newHealth + "";
        float currWidth = PlayerHealthBar.rectTransform.rect.width;
        PlayerHealthBar.rectTransform.sizeDelta = new Vector2(newHealth / maxHealth * OriginalBarWidth, PlayerHealthBar.rectTransform.rect.height);
        PlayerHealthBar.rectTransform.position += new Vector3((PlayerHealthBar.rectTransform.rect.width - currWidth) / 2, 0, 0);
    }

    public void UpdateEnemyHealth(float newHealth, float maxHealth){
        EnemyHealth.text = newHealth + "";
        float currWidth = EnemyHealthBar.rectTransform.rect.width;
        EnemyHealthBar.rectTransform.sizeDelta = new Vector2(newHealth / maxHealth * OriginalBarWidth, EnemyHealthBar.rectTransform.rect.height);
        EnemyHealthBar.rectTransform.position -= new Vector3((EnemyHealthBar.rectTransform.rect.width - currWidth) / 2, 0, 0);
    }

}
