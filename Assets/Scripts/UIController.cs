using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
	public TextMeshProUGUI speedText;
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI flaresText;
	public PlayerController plCntrl;
	public RectTransform crosshair;
	public RectTransform minimap;
	public RectTransform minimapMask;

	private Image crossAlpha;
	private RawImage minimapAlpha;
	private Image minimapMaskAlpha;
	private float alpha = 0.0f;

	void Start()
	{
		speedText.alpha = 0f;
		healthText.alpha = 0f;
		flaresText.alpha = 0f;

		crossAlpha = crosshair.gameObject.GetComponent<Image>();
		var tempAlpha = crossAlpha.color;
		tempAlpha.a = alpha;
		crossAlpha.color = tempAlpha;

		minimapAlpha = minimap.gameObject.GetComponent<RawImage>();

		minimapMaskAlpha = minimapMask.gameObject.GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
    {
		//Speed Alpha
		speedText.alpha = Mathf.Lerp(speedText.alpha, 1f, 0.0025f);
		healthText.alpha = Mathf.Lerp(healthText.alpha, 1f, 0.0025f);
		flaresText.alpha = Mathf.Lerp(flaresText.alpha, 1f, 0.0025f);
		//Crosshair Alpha
		alpha = Mathf.Lerp(alpha, 1f, 0.0025f);
		var tempAlpha = crossAlpha.color;
		tempAlpha.a = alpha;
		crossAlpha.color = tempAlpha;
		//Minimap Alpha
		minimapAlpha.color = tempAlpha;
		//Minimap Mask Alpha
		minimapMaskAlpha.color = tempAlpha;
		//Interface update
		speedText.text = $"Speed: {plCntrl.speed:n0}";
		crosshair.position = plCntrl.crosshairPos;
		healthText.text = $"Health: {plCntrl.health}";
		flaresText.text = $"Flares: {plCntrl.haveFlares}";
	}
}
