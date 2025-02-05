using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseEffectUIDetail : MonoBehaviour
{
    [SerializeField]
    TextMeshPro effectOwner;
    [SerializeField]
    TextMeshPro effectDescription;
    [SerializeField]
    ParticleSystem useEffectUIParticle;

    private void Awake()
    {
        effectOwner.text = "";
        effectDescription.text = "";
        useEffectUIParticle.Stop();
    }

    public void StartUseEffectUI(string effectOwner, string effectDescription)
    {
        this.effectOwner.text = effectOwner;
        this.effectDescription.text = effectDescription;
        useEffectUIParticle.Play();
    }
}
