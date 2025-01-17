﻿using Assets.Scripts.Auxiliar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// a class to manage compass in the game GUI
public class CompassScript : MonoBehaviour
{

    public Text compassText;            // reference to the Text component (to show cardinal directions: N, W, E, s)
                                        // all cardinal directions: north, west, south, east
    private static char north = 'N';
    private static char west = 'W';
    private static char south = 'S';
    private static char east = 'E';

    [SerializeField]
    private Transform playerTransform;      // reference to player's transform component

    [FMODUnity.EventRef]
    public string northSound;
    FMOD.Studio.EventInstance northSounde;



    // Use this for initialization (before Start)
    void Awake()
    {
        // init references
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform = Constantes.MAIN_PLAYER.transform;
        northSounde = FMODUnity.RuntimeManager.CreateInstance(northSound);

    }

    // Use this for initialization
    void Start()
    {
        SetCompassText();       // call function SetCompassText in this script
    }

    public void initiate()
    {
        Awake();
    }

    // function to start updating the compass (called from PlayerMove script)
    public void UpdateCompass(float waitTime)
    {
        // set up "-" marker to the compassText and call function SetCompassText with delay "waitTime"
        compassText.text = "-";
        Invoke("SetCompassText", waitTime);
    }

    // function to set the heading to the compass
    private void SetCompassText()
    {
        // define the current rotation of the player and define variable  compassChar
        int playerRotation = Mathf.RoundToInt(playerTransform.eulerAngles.y);
        //Debug.Log("Rotacion del jugador: "+playerRotation);
        char compassChar = '-';

        // switch case to define the rotation in Y-axis (0, 90, 180 or 270) and appropriate cardinal heading (north, west, south or east)
        switch (playerRotation)
        {
            case 0:
                compassChar = north;
                break;
            case 90:
                compassChar = east;

                break;
            case 180:
                compassChar = south;

                break;
            case 270:
                compassChar = west;

                break;
            default:
                compassChar = '.';

                break;
        }

        compassText.text = compassChar.ToString();		// set up the text to compassText component

        Constantes.GAME_ORIENTATION = compassText.text;

        //Sound play on north
        if (Constantes.GAME_ORIENTATION.Equals("N") && !Constantes.GAME_ORIENTATION.Equals("W") && !Constantes.GAME_ORIENTATION.Equals("E"))
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(northSounde, Constantes.MAIN_PLAYER.transform, Constantes.MAIN_PLAYER.GetComponent<Rigidbody>());
            northSounde.start();
        }
        else
        {
            northSounde.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

}
