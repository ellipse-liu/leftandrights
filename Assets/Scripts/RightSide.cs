using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RightSide : MonoBehaviour, IPointerEnterHandler
{
    public GameScript gamescript;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gamescript.isRight)
        {
            gamescript.addScore();
            StartCoroutine(pausetime(0.5f));
        }

        else
        {
            gamescript.ResetScore();
            StartCoroutine(pausetime(0.5f));
        }
    }

    IEnumerator pausetime(float time)
    {
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(time);
        Cursor.lockState = CursorLockMode.Confined;
    }
}
