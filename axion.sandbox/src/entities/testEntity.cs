using Axion;
using Axion.Components;

class TestEntity : Entity {
    public override void Init() {
        AddComponent(Axn.Get<Sprite>("s_test"));
    }
}