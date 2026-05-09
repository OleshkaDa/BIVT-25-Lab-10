namespace Lab10;

public interface ISerializer<T> where T: Lab9.Purple.Purple
{
    void Serialize(T obj);
    T Deserialize ();
}