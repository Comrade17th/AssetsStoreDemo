using UnityEngine;

namespace QuestExplosiveCubeV2
{
    [RequireComponent(typeof(MeshRenderer),
        typeof(Spawner),
        typeof(Explosion))
    ]
    public class ExplosiveCube : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private Spawner _spawner;
        private Explosion _explosion;
        
        private float _scaleModifier = 0.5f;
        private float _splitChanceModifier = 0.5f;
        private float _hundreadPercent = 1f;
        private float _baseSplitChance = 1f;
        private float _currentSplitChance;

        private void Awake()
        {
            _currentSplitChance = _baseSplitChance;
            _meshRenderer = GetComponent<MeshRenderer>();
            _spawner = GetComponent<Spawner>();
            _explosion = GetComponent<Explosion>();
            ChangeColor();
        }

        public void OnClick()
        {
            if (IsSplitChance())
                _explosion.ExplodeGroup(_spawner.SpawnGroup(_currentSplitChance));
            else
                _explosion.ExplodeAround();
            
            Destroy(gameObject);
        }

        public void Initialize(Vector3 parentScale, float parentSplitChance)
        {
            transform.localScale = parentScale * _scaleModifier;
            _currentSplitChance = parentSplitChance * _splitChanceModifier;
        }

        private bool IsSplitChance()
        {
            return _currentSplitChance >= Random.Range(0, _hundreadPercent);
        }

        private void ChangeColor()
        {
            _meshRenderer.material.color = Random.ColorHSV();
        }
    }
}