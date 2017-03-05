using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class TrainGoal : MonoBehaviour
    {
        public Sprite[] Sprites;

        [HideInInspector]
        public int Id;

        SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            Id = Random.Range(0, Sprites.Length);
            spriteRenderer.sprite = Sprites[Id];
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}