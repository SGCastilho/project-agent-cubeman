namespace Cubeman.Interfaces
{
    public interface IDamageble 
    {
        public void RecoveryHealth(int recoveryAmount);
        public void ApplyDamage(int damageAmount);
    }
}
