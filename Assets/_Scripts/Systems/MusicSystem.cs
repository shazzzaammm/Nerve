using System;
using UnityEngine;

public class MusicSystem : Singleton<MusicSystem>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip menuMusic, enemyMusic, dungeonMusic, preBossMusic, bossMusic;

    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
        ActionSystem.SubscribeReaction<PlayerKilledGA>(PlayerKilledReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<StartMatchGA>(StartMatchReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EndMatchGA>(EndMatchReaction, ReactionTiming.PRE);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<PlayerKilledGA>(PlayerKilledReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<StartMatchGA>(StartMatchReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EndMatchGA>(EndMatchReaction, ReactionTiming.PRE);
        audioSource.Stop();
    }

    public void EnteredMainMenuReaction()
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
    }
    private void PlayerKilledReaction(PlayerKilledGA gA)
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
    }
    
    public void StartDungeonMusic(){
        audioSource.clip = dungeonMusic;
        audioSource.Play();
    }

    private void StartMatchReaction(StartMatchGA startMatchGA)
    {
        Debug.Log("now: \ncontains: " + startMatchGA.enemies.Contains(DataSystem.instance.boss) + "\nbossTime: " + GridSystem.instance.bossTime);
        if (startMatchGA.enemies.Contains(DataSystem.instance.boss))
        {
            audioSource.clip = bossMusic;
            audioSource.Play();
        }
        else if (!GridSystem.instance.bossTime)
        {
            audioSource.clip = enemyMusic;
            audioSource.Play();
        }
        else{
            // enjkoy preboss music
        }
    }

    private void EndMatchReaction(EndMatchGA endMatchGA)
    {
        if (GridSystem.instance.bossTime)
        {
            audioSource.clip = preBossMusic;
        }
        else
        {
            audioSource.clip = dungeonMusic;
        }
        audioSource.Play();
    }
}
