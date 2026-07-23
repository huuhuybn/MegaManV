using UnityEngine;

public class ItemPS : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Vector2 lifetime = new Vector2(0.3f, 0.6f);
    [SerializeField] private Vector2 speed = new Vector2(2f, 4f);
    [SerializeField] private Vector2 size = new Vector2(0.08f, 0.18f);

    [Header("Emission")]
    [SerializeField] private int particleCount = 15;

    [Header("Shape")]
    [SerializeField] private float radius = 0.3f;

    [Header("Color")]
    [SerializeField] private Color startColor =
        new Color(0.1f, 0.6f, 1f, 1f);

    [SerializeField] private Color endColor =
        new Color(0.5f, 0.9f, 1f, 0f);

    [Header("Renderer")]
    [SerializeField] private string sortingLayerName = "Default";
    [SerializeField] private int orderInLayer = 10;

    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();

        SetupMain();
        SetupEmission();
        SetupShape();
        SetupColorOverLifetime();
        SetupSizeOverLifetime();
        SetupRenderer();
    }

    private void SetupMain()
    {
        ParticleSystem.MainModule main = particle.main;

        main.duration = duration;
        main.loop = false;
        main.playOnAwake = false;

        main.startLifetime = new ParticleSystem.MinMaxCurve(
            lifetime.x,
            lifetime.y
        );

        main.startSpeed = new ParticleSystem.MinMaxCurve(
            speed.x,
            speed.y
        );

        main.startSize = new ParticleSystem.MinMaxCurve(
            size.x,
            size.y
        );

        main.startColor = startColor;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

       
    }

    private void SetupEmission()
    {
        ParticleSystem.EmissionModule emission = particle.emission;

        emission.enabled = true;
        emission.rateOverTime = 0f;

        // Xóa Burst cũ để tránh bị thêm trùng.
        emission.SetBursts(new ParticleSystem.Burst[0]);

        ParticleSystem.Burst burst = new ParticleSystem.Burst(
            0f,
            (short)particleCount
        );

        emission.SetBurst(0, burst);
    }

    private void SetupShape()
    {
        ParticleSystem.ShapeModule shape = particle.shape;

        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = radius;
        shape.arc = 360f;
        shape.radiusThickness = 0f;
    }

    private void SetupColorOverLifetime()
    {
        ParticleSystem.ColorOverLifetimeModule colorModule =
            particle.colorOverLifetime;

        colorModule.enabled = true;

        Gradient gradient = new Gradient();

        GradientColorKey[] colorKeys =
        {
            new GradientColorKey(startColor, 0f),
            new GradientColorKey(endColor, 1f)
        };

        GradientAlphaKey[] alphaKeys =
        {
            new GradientAlphaKey(1f, 0f),
            new GradientAlphaKey(1f, 0.5f),
            new GradientAlphaKey(0f, 1f)
        };

        gradient.SetKeys(colorKeys, alphaKeys);
        colorModule.color = new ParticleSystem.MinMaxGradient(gradient);
    }

    private void SetupSizeOverLifetime()
    {
        ParticleSystem.SizeOverLifetimeModule sizeModule =
            particle.sizeOverLifetime;

        sizeModule.enabled = true;

        AnimationCurve sizeCurve = new AnimationCurve(
            new Keyframe(0f, 1f),
            new Keyframe(0.7f, 0.8f),
            new Keyframe(1f, 0f)
        );

        sizeModule.size = new ParticleSystem.MinMaxCurve(
            1f,
            sizeCurve
        );
    }

    private void SetupRenderer()
    {
        ParticleSystemRenderer particleRenderer =
            GetComponent<ParticleSystemRenderer>();

        particleRenderer.renderMode =
            ParticleSystemRenderMode.Billboard;
    }

    public void PlayEffect()
    {
        particle.Play();
    }
}
