using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField]
	private Transform firePointTransform;

	[HideInInspector]
	public Transform startTransform;

	[HideInInspector]
	public Vector3 startPos;

	[HideInInspector]
	public Vector3 startRotation;

	public Vector3 pivotPos;

	public Vector3 pivotRotation;

	public string camp;

	public float fireRate;

	public float range;

	public int bulletCount;

	private bool isCanNextFire;

	public AudioClip fireSound;

	public AudioClip reloadSound;

	[SerializeField]
	private Bullet bulletPrefab;

	public AudioSource _audioSource;

	private void OnEnable()
	{
		if (startTransform == null)
		{
			startTransform = base.transform.parent;
		}
		startPos = base.transform.localPosition;
		startRotation = base.transform.localEulerAngles;
	}

	protected virtual void Start()
	{
		isCanNextFire = true;
		_audioSource = GetComponent<AudioSource>();
	}

	public virtual void Fire()
	{
		bulletCount--;
		Object.Instantiate(bulletPrefab, firePointTransform.position, firePointTransform.transform.rotation, firePointTransform).camp = camp;
		_audioSource.PlayOneShot(fireSound);
		NextFireTimer();
		Invoke("NextFireTimer", 1f / fireRate);
	}

	public void NextFireTimer()
	{
		isCanNextFire = !isCanNextFire;
	}

	public virtual void Reload()
	{
		Debug.Log("Reloading weapon...");
	}

	public virtual void Use()
	{
		if (bulletCount > 0 && isCanNextFire)
		{
			Fire();
		}
	}

	public virtual void OnUnequipped()
	{
		base.transform.parent = startTransform;
		base.transform.localPosition = startPos;
		base.transform.localRotation = Quaternion.Euler(startRotation);
	}

	public virtual void OnEquipped(Transform holderTransform)
	{
		base.transform.parent = holderTransform;
		base.transform.localPosition = pivotPos;
		base.transform.localRotation = Quaternion.Euler(pivotRotation);
	}
}
