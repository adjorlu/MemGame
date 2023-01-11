using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlaySound : MonoBehaviour
{
    public void PlayASound(string aSound)
    {
        FMODUnity.RuntimeManager.PlayOneShot(aSound);

    }
}
