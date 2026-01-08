using Runtime.Signals;

namespace Runtime.Keys
{
    public class ResourcesKeys
    {
        public int money;
        public int gem;


        public ResourcesKeys(int money, int gem)
        {
            this.money = money;
            this.gem = gem;
        }
        public void AddMoney(int amount)
        {
            this.money += amount;
            GameSignals.Instance.onUpdateCoinText(this.money);
        }
        
        public void AddGem(int amount)
        {
            this.gem += amount;
            GameSignals.Instance.onUpdateGemText(this.gem);
        }
    }
}