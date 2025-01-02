using CEsharp.ViewModels;

namespace CEsharp.View;

public partial class DeepRock : ContentPage
{
    private DeepRockCheat deepRockCheat = new DeepRockCheat();
    public DeepRock()
    {
        InitializeComponent();
        BindingContext = deepRockCheat;
    }
    private async void OnButtonTapped(object sender, EventArgs e)
    {
        deepRockCheat.IsEnable = !deepRockCheat.IsEnable;
        var buttonColor = deepRockCheat.IsEnable ?Colors.Red: Colors.Green;
        // ����Ч��
        await AnimatedButton.ColorTo(buttonColor, 500); // ��������һ����չ������������ɫ���� // Ҳ������������Ķ���Ч��
        await AnimatedButton.ScaleTo(1.2, 250);
        await AnimatedButton.ScaleTo(1, 250);
        deepRockCheat.TestEable();
        if (!deepRockCheat.IsEnable)
        {
            await AnimatedButton.ColorTo(buttonColor, 500); // ��������һ����չ������������ɫ���� // Ҳ������������Ķ���Ч��
            await AnimatedButton.ScaleTo(1.2, 250);
            await AnimatedButton.ScaleTo(1, 250);
        }
    }
}
public static class AnimationExtensions
{
    public static Task<bool> ColorTo(this VisualElement self, Color toColor, uint length = 250, Easing easing = null)
    {
        var tcs = new TaskCompletionSource<bool>(); var fromColor = self.BackgroundColor;
        new Animation(v => { 
            self.BackgroundColor = Color.FromRgba(fromColor.Red + (toColor.Red - fromColor.Red) * v, 
                fromColor.Green + (toColor.Green - fromColor.Green) * v, fromColor.Blue + (toColor.Blue - fromColor.Blue) * v, 
                fromColor.Alpha + (toColor.Alpha - fromColor.Alpha) * v); })
            .Commit(self, "ColorTo", length: length, easing: easing, finished: (v, c) => tcs.SetResult(c)); 
        return tcs.Task;
    }
}
    
