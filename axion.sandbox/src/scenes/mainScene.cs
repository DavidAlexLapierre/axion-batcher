using Axion;
using Axion.Cameras;
using Axion.Components;

class MainScene : Scene {
    public override void Init() {
        LoadContent();
        Axn.SetCamera(new Camera2D(640, 360));
        var rand = new Random();
        for (int i = 0; i < 32000; i++) {
            Axn.CreateEntity(rand.Next(640 - 16), rand.Next(360 - 16), new TestEntity());
        }
    }

    void LoadContent() {
        Axn.Load<Sprite>("content/sprites/s_test.json");
    }
}