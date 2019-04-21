using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCell : MonoBehaviour {
    public static GameObject left;
    public static GameObject right;

    private void OnMouseDown()
    {
        StartCoroutine(MainFlow());
    }

    IEnumerator MainFlow()
    {
        Debug.Log("Left operand:" + left +
            "Right operand:" + right);

        var audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlaySound(audioSource));

        if (left != null && right == null)
        {
            right = this.gameObject;
            this.gameObject.transform.position = new Vector3(1f, 0);
            yield return new WaitForSeconds(audioSource.clip.length);
            audioSource.clip = null;
            if (checkSequence(left.GetComponent<SoundCellProperties>().RightPosition, right.GetComponent<SoundCellProperties>().RightPosition))
            {
                var newObject = Instantiate(this.gameObject, new Vector2(left.GetComponent<SoundCellProperties>().xPos, left.GetComponent<SoundCellProperties>().yPos), Quaternion.identity);
                newObject.AddComponent<SoundCellProperties>();
                newObject.GetComponent<SoundCellProperties>().RightPosition = new ArrayList(concatinateArrays(left.GetComponent<SoundCellProperties>().RightPosition, right.GetComponent<SoundCellProperties>().RightPosition));
                newObject.GetComponent<SoundCellProperties>().Sounds = new ArrayList(concatinateArraysString(left.GetComponent<SoundCellProperties>().Sounds, right.GetComponent<SoundCellProperties>().Sounds));
                newObject.GetComponent<SoundCellProperties>().xPos = left.GetComponent<SoundCellProperties>().xPos;
                newObject.GetComponent<SoundCellProperties>().yPos = left.GetComponent<SoundCellProperties>().yPos;
                Destroy(left);
                Destroy(right);
                left = null;
                right = null;
                yield break;
            }
            else
            {
                left.transform.position = new Vector3(left.GetComponent<SoundCellProperties>().xPos, left.GetComponent<SoundCellProperties>().yPos);
                right.transform.position = new Vector3(right.GetComponent<SoundCellProperties>().xPos, right.GetComponent<SoundCellProperties>().yPos);
                left = null;
                right = null;
                yield break;
            }
        }

        if (left == null && right == null)
        {
            left = this.gameObject;
            this.gameObject.transform.position = new Vector3(-1f, 0);
        }

        Debug.Log(this.gameObject.GetComponent<SoundCellProperties>().RightPosition + " x:" +
            left.GetComponent<SoundCellProperties>().xPos +
            " y:" + left.GetComponent<SoundCellProperties>().yPos);
    }

    IEnumerator PlaySound(AudioSource audioSource)
    {
        for (int i = 0; i < this.gameObject.GetComponent<SoundCellProperties>().Sounds.Count; i++)
        {
            audioSource.clip = (AudioClip)Resources.Load((string)this.gameObject.GetComponent<SoundCellProperties>().Sounds[i], typeof(AudioClip));
            audioSource.Play();
            while (audioSource.isPlaying){
                yield return null;
            }
        }
    }

    private ArrayList concatinateArraysString(ArrayList left, ArrayList right)
    {
        for (int i = 0; i < right.Count; i++)
        {
            left.Add((string)right[i]);
        }

        return left;
    }

    private ArrayList concatinateArrays(ArrayList left, ArrayList right)
    {
        for (int i = 0; i < right.Count; i++)
        {
            left.Add((int)right[i]);
        }

        return left;
    }

    private bool checkSequence(ArrayList first, ArrayList second)
    {
        try
        {
            int maxFirst = 0;
            for (int i = 0; i < first.Count; i++)
            {
                if ((int)first[i] > maxFirst)
                {
                    maxFirst = (int)first[i];
                }
            }

            maxFirst = maxFirst + 1;

            for (int i = 0; i < second.Count; i++)
            {
                if (maxFirst == (int)second[i])
                {
                    return true;
                }
            }
            return false;
        } catch (Exception ex)
        {
            Debug.Log(ex);
            return false;
        }
    }
}
