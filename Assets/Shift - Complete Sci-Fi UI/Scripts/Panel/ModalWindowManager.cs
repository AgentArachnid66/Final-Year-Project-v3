﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Michsky.UI.Shift
{
    public class ModalWindowManager : MonoBehaviour
    {
        [Header("Resources")]
        public TextMeshProUGUI windowTitle;
        public TextMeshProUGUI windowDescription;

        [Header("Settings")]
        public bool sharpAnimations = false;
        public bool useCustomTexts = false;
        public string titleText = "Title";
        [TextArea] public string descriptionText = "Description here";

        public Animator mWindowAnimator;
        bool isOn = false;

        void Start()
        {
          //  mWindowAnimator = gameObject.GetComponent<Animator>();

            if (useCustomTexts == false)
            {
                windowTitle.text = titleText;
                windowDescription.text = descriptionText;
            }

            gameObject.SetActive(false);
        }

        [ContextMenu("Fade In")]
        public void ModalWindowIn()
        {
            Debug.Log($"Fading In Window {gameObject.name}");
            StopCoroutine("DisableWindow");

            gameObject.SetActive(true);

            if (isOn == false)
            {
                if (sharpAnimations == false)
                    mWindowAnimator.CrossFade("Window In", 0.1f);
                else
                    mWindowAnimator.Play("Window In");

                isOn = true;
            }
        }

        public void ModalWindowOut()
        {
            Debug.Log($"Fading Out Window {gameObject.name}");

            if (isOn == true)
            {
                if (sharpAnimations == false)
                    mWindowAnimator.CrossFade("Window Out", 0.1f);
                else
                    mWindowAnimator.Play("Window Out");

                isOn = false;
            }

            StartCoroutine("DisableWindow");
        }

        IEnumerator DisableWindow()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }
}