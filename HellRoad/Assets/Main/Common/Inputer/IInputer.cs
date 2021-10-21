namespace Utility.Inputer
{
    interface IInputer
    {
        float HoriMoveDir();
        float VertMoveDir();
        bool DecideDown();
        bool Decide();
        bool DecideUp();
        bool CancelDown();
        bool Cancel();
        bool CancelUp();
        bool MenuDown();
        bool Menu();
        bool MenuUp();
        bool ShowMapDown();
        bool ShowMap();
        bool ShowMapUp();
    }
}