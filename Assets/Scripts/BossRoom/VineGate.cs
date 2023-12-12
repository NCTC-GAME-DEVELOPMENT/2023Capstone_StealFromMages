using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class VineGate : MonoBehaviour
{



    public float showTime = .1f;
    float timer = .2f;
    bool IsDone = false;

    public List<GameObject> VineList;
    int vineIndex = 0;
    public UnityEvent OnStartBossFight;
    public event Action OnStartFight;

    // Start is called before the first frame update
    void Start()
    {
        timer = showTime; 
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDone) { return;  }

        timer -= Time.deltaTime; 
        if (timer < 0 )
        {
            VineList[vineIndex].SetActive(true);
            timer = showTime;
            vineIndex++; 
            if (vineIndex == VineList.Count)
            {
                IsDone = true;
                OnStartBossFight.Invoke();
                OnStartFight?.Invoke();
                PlayerHintManager.instance.ShowMessage("What is that!?!?", 1f, .25f, Color.red);
            }
        }

        
    }
}
