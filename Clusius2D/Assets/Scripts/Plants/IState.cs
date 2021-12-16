public interface IState 
{  
    void Tick();
    void OnEnter();
    void OnExit();
    void OnPauze();
    void OnContinue();
}
