using System.Collections.Generic;

public interface IAction
{
    void InitAction(FinishEvent onFinish);
    void Do();

    void ContinueAction();
}

public enum Sequence
{
    Animation,
    Sound,
    Line
}
