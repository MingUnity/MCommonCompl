using MingUnity.MVVM;

public class TestViewModel : ViewModelBase
{
    private string _text;

    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            string old = _text;

            _text = value;

            RaiseCPropertyChanged("Text", old, value);
        }
    }

    public override void Setup()
    {
        Text = _text;
    }
}
