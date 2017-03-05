using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class Rotator : MonoBehaviour
    {
        public AudioClip TurnSfx;

        int trains;

        AudioSource asrc;

        void Awake()
        {
            asrc = GetComponent<AudioSource>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        void OnMouseDown()
        {
            if (trains > 0) return;

            asrc.PlayOneShot(TurnSfx);
            transform.Rotate(Vector3.forward, -90.0f);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Train")
            {
                trains++;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Train")
            {
                trains--;
            }
        }
    }
}