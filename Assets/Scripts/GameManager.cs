using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ketsu
{
    public class GameManager : MonoBehaviour
    {
        public int TrainsRequired;
        public int MaxAxccidents;

        public Text TrainsText;
        public Text AccidentText;

        [HideInInspector]
        public static int TrainsSaved;
        [HideInInspector]
        public static int Accidents;

        public static bool GameOver;

        MenuManager menuManager;

        void Awake()
        {
            menuManager = FindObjectOfType<MenuManager>();
        }

        void Start()
        {
            TrainsSaved = 0;
            Accidents = 0;
            GameOver = false;
            TrainsText.text = "Trains: " + TrainsSaved + " / " + TrainsRequired;
            AccidentText.text = "Accidents: " + Accidents + " / " + MaxAxccidents;
        }

        void Update()
        {
            if (GameOver) return;

            if (Accidents >= MaxAxccidents)
            {
                Accidents = MaxAxccidents;
                GameOver = true;
                menuManager.LoadScene("Lose");
            }
            else if (TrainsSaved >= TrainsRequired)
            {
                TrainsSaved = TrainsRequired;
                GameOver = true;
                menuManager.LoadScene("Win");
            }

            TrainsText.text = "Trains: " + TrainsSaved + " / " + TrainsRequired;
            AccidentText.text = "Accidents: " + Accidents + " / " + MaxAxccidents;
        }
    }
}