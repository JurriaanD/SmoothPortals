using UnityEngine.Events;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public UnityEvent collectionEvent;
    public Material material;

    public GameObject gemModel;
    public GameObject beam;

    private AudioSource audioSource;
    private bool isCollected = false;


    void Start()
    {
        gemModel.GetComponent<Renderer>().material = material;
        beam.GetComponent<Renderer>().material = material;

        if (collectionEvent == null)
            collectionEvent = new UnityEvent();
        collectionEvent.AddListener(Collect);

        audioSource = GetComponent<AudioSource>();
    }

    void Collect()
    {
        if (!isCollected)
        {
            isCollected = true;
            audioSource.Play();
            gemModel.SetActive(false);
            beam.SetActive(false);
        }
    }
}
