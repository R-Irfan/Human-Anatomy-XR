using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
public class SanpToOriginalPosition : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    Vector3 initialPosition, initialScale;
    Quaternion initialRotation;


    [SerializeField]
    TextMeshProUGUI leftHandPartLableText, rightHandPartLableText;

    AudioSource sound;
    [SerializeField]
    AudioClip grabSoundClip, releaseSoundClip;
    
    GameObject leftPartLableGO, rightPartLableGO;

    private Color originalColor;
    private Vector3 originalScale;

    private Renderer objectRenderer;

    [Header("Affordance Settings")]
    [Tooltip("Color to highlight the object when pointed at.")]
    public Color highlightColor = Color.yellow;

    [Tooltip("Scale multiplier to enlarge the object when pointed at.")]
    [Range(1.0f, 2.0f)]
    public float scaleMultiplier = 1.2f;
    [Tooltip("Speed of the scale transition.")]
    public float scaleTransitionSpeed = 9.0f;

    private Coroutine scaleCoroutine;

    void Awake()
    {
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        objectRenderer = GetComponent<Renderer>();

       /* if (objectRenderer != null && objectRenderer.materials.Length > 0)
        {
            originalColor = objectRenderer.materials[0].color;
        }*/



        originalScale = transform.localScale;



        initialScale = transform.localScale;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        

        leftPartLableGO = leftHandPartLableText.transform.parent.gameObject;
        leftHandPartLableText.text = "";
        leftPartLableGO.SetActive(false);

        rightPartLableGO = rightHandPartLableText.transform.parent.gameObject;
        rightHandPartLableText.text = "";
        rightPartLableGO.SetActive(false);

        sound = GetComponent<AudioSource>();


        //hintText.text = "Started ..";

        // Subscribe to the selectExited event
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);

            grabInteractable.hoverEntered.AddListener(OnHoverEntered);
            grabInteractable.hoverExited.AddListener(OnHoverExited);


            //hintText.text = "Add Listner Added ..";
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args) 
    {

        Debug.Log("Hover entered!");

        /*if (objectRenderer != null && objectRenderer.materials.Length > 0)
        {
            Material[] materials = objectRenderer.materials;
            materials[0].color = highlightColor;
            objectRenderer.materials = materials;
        }*/

        // Change scale
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        scaleCoroutine = StartCoroutine(SmoothScale(originalScale * scaleMultiplier));
    }

    IEnumerator SmoothScale(Vector3 targetScale)
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleTransitionSpeed);
            yield return null;
        }
        transform.localScale = targetScale;
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {

        Debug.Log("Hover exited!");
       /* if (objectRenderer != null && objectRenderer.materials.Length > 0)
        {
            Material[] materials = objectRenderer.materials;
            materials[0].color = originalColor;
            objectRenderer.materials = materials;
        }*/


        // Reset scale
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        scaleCoroutine = StartCoroutine(SmoothScale(originalScale));
    }


    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"Object Grabbed: {gameObject.name}");


        sound.PlayOneShot(grabSoundClip);

        /*if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }*/

        // Reset scale
        transform.localScale = originalScale;

        leftPartLableGO.SetActive(true);
        leftHandPartLableText.text = gameObject.name;

        rightPartLableGO.SetActive(true);
        rightHandPartLableText.text = gameObject.name;
        

    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);

            grabInteractable.hoverEntered.RemoveListener(OnHoverEntered);
            grabInteractable.hoverExited.RemoveListener(OnHoverExited);
        }
    }





    // Function called when the object is released
    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log($"Object Released: {gameObject.name}");
        // Add your custom logic here
        //hintText.text = "Object Relesed : " + gameObject.name;

        sound.PlayOneShot(releaseSoundClip);

        rightPartLableGO.SetActive(false);
        leftPartLableGO.SetActive(false);

        if (Vector3.Distance(initialPosition, transform.position) <= 0.75f)
        {
            ResetObjectPoseSmooth();
        }
    }

    public void ResetObjectPoseSmooth()
    {
        Debug.Log("ResetObjectPsoeSmooth");
        StartCoroutine(SmoothReset());
        //hintText.text = "Object Relesed : " + gameObject.name;
        //transform.position = initialPosition;
        //transform.rotation = initialRotation;
    }

    private IEnumerator SmoothReset()
    {
        Debug.Log("Smooth Reset");
        float duration = 0.5f; // Time in seconds for the transition
        float elapsed = 0;

        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;
        Quaternion startRotation = transform.rotation;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, initialPosition, elapsed / duration);
            transform.localScale = Vector3.Lerp(startScale, initialScale, elapsed / duration);
            transform.rotation = Quaternion.Slerp(startRotation, initialRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and rotation are set
        transform.localScale = initialScale;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }


}