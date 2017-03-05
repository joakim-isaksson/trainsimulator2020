using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ketsu
{
    public class Train : MonoBehaviour
    {
        public AudioClip GoalSfx;

        public float MinSpeed;
        public float MaxSpeed;

        public GameObject ExplosionPrefab;

        public Sprite[] Sprites;

        [HideInInspector]
        public int Id;

        [HideInInspector]
        public TrainSpawner HomeSpawner;

        SpriteRenderer spriteRenderer;

        bool rotating;
        bool rotated;
        bool rotateLeft;
        Vector3 rotationPoint;

        float speed;

        AudioSource asrc;

        void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            asrc = GetComponent<AudioSource>();

            Id = Random.Range(0, Sprites.Length);
            spriteRenderer.sprite = Sprites[Id];
            speed = Random.Range(MinSpeed, MaxSpeed);
        }

        void Start()
        {
            
        }

        void Update()
        {
            if (GameManager.GameOver) return;

            if (rotating && !rotated)
            {
                transform.position = Vector3.MoveTowards(transform.position, rotationPoint, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, rotationPoint) < 0.001f)
                {
                    transform.position = rotationPoint;

                    if (rotateLeft) transform.Rotate(Vector3.forward, 90.0f);
                    else transform.Rotate(Vector3.forward, -90.0f);

                    rotated = true;
                }
            } else
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Train")
            {
                collision.GetComponent<Train>().Explode();
                Explode();
            }
            else if (collision.tag == "Goal")
            {
                if (collision.GetComponent<TrainGoal>().Id == Id)
                {
                    ReachGoal();
                }
                else Explode();
            }
            else if(collision.tag == "LeftRotator")
            {
                if (!rotating)
                {
                    rotating = true;
                    rotated = false;
                    rotateLeft = true;
                    rotationPoint = collision.gameObject.transform.position;
                }
            }
            else if (collision.tag == "RightRotator")
            {
                if (!rotating)
                {
                    rotating = true;
                    rotated = false;
                    rotateLeft = false;
                    rotationPoint = collision.gameObject.transform.position;
                }
            }
            else if (collision.tag == "Wall")
            {
                if (!rotating)
                {
                    Explode();
                }
            }
            else if (collision.tag == "Spawner")
            {
                if (HomeSpawner == null || !collision.GetComponent<TrainSpawner>().Equals(HomeSpawner)) Explode();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Spawner")
            {
                collision.GetComponent<TrainSpawner>().Equals(HomeSpawner);
                HomeSpawner = null;
            }
            else if (collision.tag == "Rotator")
            {
                rotating = false;
            }
        }

        void ReachGoal()
        {
            GameManager.TrainsSaved++;
            asrc.PlayOneShot(GoalSfx);
            spriteRenderer.enabled = false;
            DestroyAfterDelay(3.0f);
        }

        void Explode()
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            GameManager.Accidents++;
            Destroy(gameObject);
        }

        IEnumerator DestroyAfterDelay(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}