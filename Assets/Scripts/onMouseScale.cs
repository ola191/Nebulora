using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    public float animationDuration = 0.10f;
    public Vector3 hoverRotationRight = new Vector3(0, 0, 1f);
    public Vector3 hoverRotationLeft = new Vector3(0, 0, -1f);

    public string enterSoundPath = "Audio/short/button.wav";
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private bool isHovering = false;
    private AudioSource audioSource;
    private AudioClip enterSound;

    void Start()
    {
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
        audioSource = GetComponent<AudioSource>();
        enterSound = Resources.Load<AudioClip>(enterSoundPath);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        StopAllCoroutines();
        if (Random.Range(0, 2) == 0)
        {
            StartCoroutine(AnimateScaleAndRotation(hoverScale, hoverRotationLeft * Random.Range(100, 400) / 100));
        }
        else
        {
            StartCoroutine(AnimateScaleAndRotation(hoverScale, hoverRotationRight * Random.Range(100, 400) / 100));
        }
        if (enterSound != null)
        {
            audioSource.PlayOneShot(enterSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        StopAllCoroutines();
        StartCoroutine(AnimateScaleAndRotation(originalScale, originalRotation.eulerAngles));
    }

    private System.Collections.IEnumerator AnimateScaleAndRotation(Vector3 targetScale, Vector3 targetRotation)
    {
        Vector3 startScale = transform.localScale;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float time = 0f;

        while (time < animationDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / animationDuration);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / animationDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        transform.rotation = endRotation;
    }
}
