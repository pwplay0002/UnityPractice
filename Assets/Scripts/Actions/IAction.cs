//interface c++이 아니면 interface 말고는 다중상속이 안된다.
public interface IAction
{
    void Begin(object obj);//object : 특정 객체들의 최상위 타입
    void End();
}
