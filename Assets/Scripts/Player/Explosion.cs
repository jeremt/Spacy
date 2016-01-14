using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public int NumberOfParticles = 5;
    public Rigidbody2D Particle;
    public Color Color;
    float Duration = 0.4f;

    private Rigidbody2D[] _particles;
    private float _currentTime;

    public void Start() {
        _particles = new Rigidbody2D[NumberOfParticles];
        _currentTime = 0f;
        for (int i = 0; i < NumberOfParticles; ++i) {
            var particleInstance = Instantiate(Particle, transform.position, transform.rotation) as Rigidbody2D;
            particleInstance.velocity = new Vector2(
                Random.Range(1f, 2f) * (Random.Range(0f, 1f) > 0.5 ? -1f : 1f),
                Random.Range(1f, 2f) * (Random.Range(0f, 1f) > 0.5 ? -1f : 1f)
            );
            particleInstance.GetComponent<SpriteRenderer>().color = Color;
            _particles[i] = particleInstance;
        }
    }

    public void Update() {
        _currentTime += Time.deltaTime;
        for (int i = 0; i < NumberOfParticles; ++i) {
            _particles[i].GetComponent<SpriteRenderer>().color = new Color(Color.r, Color.g, Color.b, Mathf.Lerp(1, 0, _currentTime / Duration));
        }
        if (_currentTime > Duration) {
            for (int i = 0; i < NumberOfParticles; ++i) {
                Destroy(_particles[i].gameObject);
            }
            Destroy(gameObject);
        }
    }

}
