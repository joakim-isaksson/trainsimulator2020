using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ketsu
{
    public class MenuManager : MonoBehaviour
    {
        public float FadeSpeed;
        public string NextScene;
        public string ExitScene;
        public bool QuitOnExit;
        public bool AutoLoad;
        public float AutoLoadTime;
        public bool AllowReload;
        public bool LoadNextSceneOnKeyPress;

        ScreenFaider Faider;

        bool loading = true;

        void Start()
        {
            Faider = ScreenFaider.instance;

            Faider.SetTo(Color.black);
            Faider.FadeOut(Color.black, FadeSpeed, delegate
            {
                loading = false;
                if (AutoLoad)
                {
                    DelayedAction(AutoLoadTime, delegate
                    {
                        LoadScene(ExitScene);
                    });
                }
            });
        }

        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (QuitOnExit) Application.Quit();
                else LoadScene(ExitScene);
            }
            else if (AllowReload && Input.GetKeyDown(KeyCode.R)) LoadScene(SceneManager.GetActiveScene().name);
            else if (LoadNextSceneOnKeyPress && Input.anyKeyDown) LoadScene(NextScene);
        }

        public void LoadNextScene()
        {
            LoadScene(NextScene);
        }

        public void LoadScene(string name)
        {
            if (loading) return;
            loading = true;

            Faider.FadeIn(Color.black, FadeSpeed, delegate
            {
                SceneManager.LoadScene(name, LoadSceneMode.Single);
            });
        }

        public void DelayedAction(float time, Action action)
        {
            StartCoroutine(RunDelayedAction(time, action));
        }

        IEnumerator RunDelayedAction(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            if (action != null) action();
        }
    }
}