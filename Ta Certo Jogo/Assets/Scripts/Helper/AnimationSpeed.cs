using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeed : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        //animator.SetFloat("Multiplier", Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
