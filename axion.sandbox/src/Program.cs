using Axion;
using Axion.Desktop;

class Program {
    static void Main() {
        Axn.CreateGame(new DesktopGameProvider());
        Axn.RegisterScene(new MainScene());
        Axn.RunGame();
    }
}