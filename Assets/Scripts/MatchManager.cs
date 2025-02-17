﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class MatchManager : MonoBehaviour, OnBeatElement
{
    public Player[] players;
    public int roundToWin = 2;


    public Sequence currentSequence;
    public Player winner;
    public Player loser;
    public int winnerID;
    public bool isWon = false;
    public bool matchIsEnd = false;

    private bool gameIsPaused = false;
    private bool roundIsEnd = false;
   
    private bool hasIncremented = false;
    public bool isFinalPhase = false;

    public Color victoryJapColor;
    public Color victoryUsColor;
    public Color baseColor;
    public CueManager cueManager;

    public FinalMenu finalMenu;

    //SONDIER
    public MusicManager musicManager;
    public GameObject camera;
    //SONDIER

    public bool isAI = false;
    private PlayerAI playerAI;

    void Start()
    {

        customStart();
        
    }

    public void customStart()
    {
        Camera.main.backgroundColor = baseColor;
        foreach (Player player in players) {
            player.UpdateRoundCounter();
        }

        BeatManager.RegisterOnBeatElement(this);

        if (isAI) {
            playerAI = (PlayerAI)players[1];
            playerAI.SetInputLength(3f);
        }
    }

    public void OnBeat()
    {
     
    }

    void Update()
    {
        if (isWon) {
            if (!hasIncremented) {
                winner.wins += 1;

                winner.UpdateRoundCounter();
                hasIncremented = true;

                Debug.Log("test");
                if (winner.wins == loser.wins && winner.wins == roundToWin - 1)
                {
                    if (isAI) {
                        playerAI.SetInputLength(1f);
                    }
                    isFinalPhase = true;
                    Debug.Log(isFinalPhase);
                }
                    

            }

            onRoundEnd();

            if (winner != null && winner.wins == roundToWin) {
                //SONDIER
                if (winnerID == 0) {
                    onMatchEnd(players[0], players[1]);
                    musicManager.WinUS();
                }
                if (winnerID == 1) {
                    onMatchEnd(players[1], players[0]);
                    musicManager.WinJP();
                }
                //SONDIER;
            }
            else
            {
                //SONDIER
                if (winnerID == 0)
                {
                    musicManager.RoundWinUS();
                }
                if (winnerID == 1)
                {
                    musicManager.RoundWinJP();
                }
                //SONDIER;
            }


        }
        else if (players[0].currentLife <= 0) {
            players[0].health.gameObject.SetActive(false);
            winner = players[1];
            loser = players[0];
            winnerID = 1;
            isWon = true;
        }

        else if (players[1].currentLife <= 0) {
            players[1].health.gameObject.SetActive(false);
            winner = players[0];
            loser = players[1];
            winnerID = 0;
            isWon = true;
        }

        //if (matchIsEnd)

    }

    public void customResetRound()
    {
        camera.GetComponent<Animator>().SetTrigger("Start");
    }

    public void onRoundEnd()
    {
        loser.animator.Play("Death", 0);
        //anim/sons de fin de round
    }

    public void resetRound()
    {
        camera.GetComponent<Animator>().SetTrigger("Start");
        loser.animator.Play("Idle", 0);
        if (isFinalPhase)
        {
            InputTranslator.step = 1;
          
        }

        for (int i = 0; i < players.Length; i++) {
            players[i].currentLife = players[i].maxLife;
            players[i].health.currentAmount = players[i].currentLife;
            players[i].health.refreshHealth();
            players[i].health.gameObject.SetActive(true);
            players[i].BufferReset();
        }
        camera.GetComponent<Animator>().SetTrigger("Start");
        loser.animator.Play("Idle", 0);
        winner = null;
        loser = null;
        isWon = false;
        hasIncremented = false;
    }


    public void onMatchEnd(Player winner, Player loser)
    {
        matchIsEnd = true;

        winner.animator.Play("Victory", 0);
        loser.animator.Play("Death", 0);
    }

    public void resetGame()
    {
        Camera.main.backgroundColor = baseColor;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].wins = 0;
            players[i].UpdateRoundCounter();
        }
        resetRound();
    }
}