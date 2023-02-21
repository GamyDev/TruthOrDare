using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
   [SerializeField] private GameObject[] _vfx;
   [SerializeField] private GameObject _backCard;



    public void PlayVFX()
    {
          int rnd = Random.Range(0, 23);
         _vfx[rnd].gameObject.SetActive(true);
      //  _vfx.SetActive(true);
        //_backCard.SetActive(false);

        Invoke("DeletVFX", 2f);
    }


    void DeletVFX()
    {
       // _vfx.SetActive(false);
        //_backCard.SetActive(false);
         for (int i = 0; i < 23; i++)
         {
             _vfx[i].gameObject.SetActive(false);
         }


    }
}
 

