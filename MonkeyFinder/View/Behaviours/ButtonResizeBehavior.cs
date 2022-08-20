namespace MonkeyFinder.View.Behaviours;

public class ButtonResizeBehavior : Behavior<Button>
{
    private const int initWidth = 20;
    private int width = initWidth;

    protected override void OnAttachedTo(Button bindable)
    {
        bindable.Pressed += OnPressed;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(Button bindable)
    {
        bindable.Pressed -= OnPressed;
        base.OnDetachingFrom(bindable);
    }

    private void OnPressed(object sender, EventArgs e)
    {
        width += 2;
        ((Button)sender).WidthRequest = width;
    }
}
