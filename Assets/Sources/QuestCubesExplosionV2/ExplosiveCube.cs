using UnityEngine;
using Random = UnityEngine.Random;

namespace QuestExplosiveCubeV2
{
    [RequireComponent(typeof(Rigidbody),
        typeof(MeshRenderer))]
    public class ExplosiveCube : MonoBehaviour
    {
        [SerializeField] private ExplosiveCube _cubePrefab;

        [SerializeField] private float _explosionModifier = 100f;
        [SerializeField] private float _explosionPower = 10f;
        [SerializeField] private float _explosionRadius = 5f;
        [SerializeField] private float _explosionUpwardsModifier = 3f;

        private MeshRenderer _meshRenderer;
        
        private int _minSpawnCount = 2;
        private int _maxSpawnCount = 6;
        private float _scaleModifier = 0.5f;
        private float _splitChanceModifier = 0.5f;
        private float _hundreadPercent = 1f;
        private float _baseSplitChance = 1f;
        private float _currentSplitChance;

        private void Awake()
        {
            _currentSplitChance = _baseSplitChance;
            _meshRenderer = GetComponent<MeshRenderer>();
            ChangeColor();
        }

        public void OnClick()
        {
            if (IsSplitChance())
            {
                Split();
            }
            else
            {
                Explode();
            }
        }

        protected void Decrease(Vector3 parentScale, float _parentSplitChance)
        {
            transform.localScale = parentScale * _scaleModifier;
            _currentSplitChance = _parentSplitChance * _splitChanceModifier;
        }

        private ExplosiveCube SpawnCube()
        {
            ExplosiveCube cube = Instantiate(_cubePrefab, transform.position, transform.rotation);
            cube.Decrease(transform.localScale, _currentSplitChance);
            return cube;
        }

        private void Explode()
        {
            float radius = _explosionRadius / transform.localScale.x;
            float power = _explosionPower * _explosionModifier / transform.localScale.x;
            
            Collider[] colliders = Physics.OverlapSphere(
                transform.position,
                radius);
            
            foreach (Collider hit in colliders)
            {
                Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

                if (rigidbody != null)
                    rigidbody.AddExplosionForce(power,
                        transform.position,
                        radius,
                        _explosionUpwardsModifier);
            }
            
            Destroy(gameObject);
        }

        private void Split()
        {
            int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount);

            for (int i = 0; i < spawnCount; i++)
            {
                Rigidbody cubeRigidbody = SpawnCube().GetComponent<Rigidbody>();
                cubeRigidbody.AddExplosionForce(
                    _explosionPower,
                    transform.position, 
                    _explosionRadius,
                    _explosionUpwardsModifier,
                    ForceMode.Impulse);
            }
            
            Destroy(gameObject);
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