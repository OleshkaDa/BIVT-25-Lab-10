namespace Lab9.Purple;

public abstract class Purple
{
    protected string _input;
    public string Input => _input;

    protected Purple(string input = "")
    {
        if (input == null)
        {
            _input = "";
            return;
        }

        _input = input;
    }

    public abstract void Review();

    public virtual void ChangeText(string text)
    {
        _input = text;
        Review();
    }
}
