using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    Transform SpawnPosTransform;

    [SerializeField]
    GameObject SkeletonGO, MusculaGO, JointsGO, CardioGO, NervousGO, MuscleGO, VisceralGO;

    [SerializeField]
    AudioClip buttonClickSound;

    AudioSource sound;

    bool skeleton = true, muscular, joints, cardio, nervous, muscle, visceral = false;

    GameObject skeletonClone, muscularClone, jointsClone, cardioClone, nervousClone, muscleClone, visceralClone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (skeleton)
        {
            //skeletonClone = Instantiate(SkeletonPrefab, SpawnPosTransform.position, SpawnPosTransform.rotation);
        }

        sound = GetComponent<AudioSource>();
    }



    public void SkeletonSystem() 
    {
        Debug.Log("Skeleton System ");
        skeleton = !skeleton;

        if (skeleton)
        {
            //skeletonClone = Instantiate(SkeletonPrefab, SpawnPosTransform.position, Quaternion.identity);

            SkeletonGO.SetActive(true);

            CardioGO.SetActive(false);
            NervousGO.SetActive(false);

            sound.PlayOneShot(buttonClickSound);
        }
        else 
        {
            //GameObject temp = skeletonClone;
            //Destroy(temp);
            SkeletonGO.SetActive(false);
            sound.PlayOneShot(buttonClickSound);
        }

    }

    public void MuscularSystem()
    {
        Debug.Log("MuscularSystem ");
        muscular = !muscular;

        if (muscular)
        {
            sound.PlayOneShot(buttonClickSound);
            //muscularClone = Instantiate(MusculaPrefab, SpawnPosTransform.position, Quaternion.identity);
            MusculaGO.SetActive(true);
            CardioGO.SetActive(false);
            NervousGO.SetActive(false);
        }
        else
        {
            sound.PlayOneShot(buttonClickSound);
            //GameObject temp = muscularClone;
            //Destroy(temp);
            MusculaGO.SetActive(false);
        }

    }

    public void JointsSystem()
    {
        Debug.Log("Joints System ");
        joints = !joints;

        if (joints)
        {
            sound.PlayOneShot(buttonClickSound);
            JointsGO.SetActive(true);

            CardioGO.SetActive(false);
            NervousGO.SetActive(false);
            //jointsClone = Instantiate(JointsPrefab, SpawnPosTransform.position, Quaternion.identity);
        }
        else
        {
            sound.PlayOneShot(buttonClickSound);
            JointsGO.SetActive(false);
           // GameObject temp = jointsClone;
           // Destroy(temp);
        }

    }

    public void CardioSystem()
    {
        Debug.Log("cardio System ");
        cardio = !cardio;

        if (cardio)
        {
            
            sound.PlayOneShot(buttonClickSound);
            CardioGO.SetActive(true);

            
            NervousGO.SetActive(false);
            // cardioClone = Instantiate(CardioPrefab, SpawnPosTransform.position, Quaternion.identity);
        }
        else
        {
            sound.PlayOneShot(buttonClickSound);
            CardioGO.SetActive(false);
            //GameObject temp = cardioClone;
            //Destroy(temp);
        }

    }

    public void NervousSystem()
    {
        Debug.Log("nervous System ");
        nervous = !nervous;

        if (nervous)
        {
            sound.PlayOneShot(buttonClickSound);
            NervousGO.SetActive(true);
            CardioGO.SetActive(false);
            
            //nervousClone = Instantiate(NervousPrefab, SpawnPosTransform.position, Quaternion.identity);
        }
        else
        {
            sound.PlayOneShot(buttonClickSound);
            NervousGO.SetActive(false);
           // GameObject temp = nervousClone;
            //Destroy(temp);
        }

    }

    public void MuscleSystem()
    {
        Debug.Log("muscle System ");
        muscle = !muscle;

        if (muscle)
        {
            sound.PlayOneShot(buttonClickSound);
            MuscleGO.SetActive(true);
            CardioGO.SetActive(false);
            NervousGO.SetActive(false);
            // muscleClone = Instantiate(MusclePrefab, SpawnPosTransform.position, Quaternion.identity);
        }
        else
        {
            sound.PlayOneShot(buttonClickSound);
            MuscleGO.SetActive(false);
            //GameObject temp = muscleClone;
            //Destroy(temp);
        }

    }

    public void VisceralSystem()
    {
        Debug.Log("visceral System ");
        visceral = !visceral;

        if (visceral)
        {
            sound.PlayOneShot(buttonClickSound);
            VisceralGO.SetActive(true);
            CardioGO.SetActive(false);
            NervousGO.SetActive(false);
            //visceralClone = Instantiate(VisceralPrefab, SpawnPosTransform.position, Quaternion.identity);


        }
        else
        {
            sound.PlayOneShot(buttonClickSound);
            VisceralGO.SetActive(false);
            // GameObject temp = visceralClone;
            // Destroy(temp);
        }

    }


    public void CloseApp() 
    {
        Debug.Log("Closing the App");
        Application.Quit();
    }


}
