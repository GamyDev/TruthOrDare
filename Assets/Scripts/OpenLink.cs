using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{

    public void PrivacyPolicy()
    {
       Application.OpenURL("https://docs.google.com/document/d/1ivZVZ_1iXpQZr1l4qNH9LtP0Y0rV0pEnNMstShVluR0/edit#heading=h.75yj1osgcmas");
    }

    public void Terms()
    {
        Application.OpenURL("https://docs.google.com/document/d/1dCRKwmEAgJxJZsB8s4kBAZt1zq3YM-gLp7LH3WIZ25w/edit#");
    }

}
