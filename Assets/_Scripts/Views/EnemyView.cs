using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text intentionText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    

    public int attackPower { get; set; }



    public void Setup(EnemyData enemyData){
        base.SetupBase(enemyData.health);
        spriteRenderer.sprite = enemyData.image;
        attackPower = enemyData.attackPower;
        UpdateIntentionText();
    }
    
    private void UpdateIntentionText(){
        intentionText.text = "Atk: " + attackPower;
    }


}
