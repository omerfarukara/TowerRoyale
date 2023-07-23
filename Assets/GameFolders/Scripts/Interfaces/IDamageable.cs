using GameFolders.Scripts.General.Enum;

namespace GameFolders.Scripts.Interfaces
{
    public interface IDamageable
    {
        public OwnerType OwnerType { get;}
        public float Health { get; set; }
        public void TakeDamage(float damage);
    }
}
