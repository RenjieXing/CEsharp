using CEsharp.Model.DeepRock.View;
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
        Border b = sender as Border;
        if (b != null)
        {
            deepRockCheat.TestEable();// 动画效果
            var buttonColor = deepRockCheat.IsEnable ? Colors.Green : Colors.Red;
            await b.ColorTo(buttonColor, 500); // 假设你有一个扩展方法来处理颜色动画 // 也可以添加其他的动画效果
            await b.ScaleTo(1.2, 250);
            await b.ScaleTo(1, 250);
        }
    }
    private async void OnButtonTappedFunc(object sender, EventArgs e)
    {
        Border b = sender as Border;
        if (b != null)
        {
            NodeInfo nodeInfo = b.BindingContext as NodeInfo;
            if (nodeInfo != null)
            {
                var buttonColor = deepRockCheat.Excute(nodeInfo) ? Colors.Green : Colors.Red;
                await b.ColorTo(buttonColor, 500); // 假设你有一个扩展方法来处理颜色动画 // 也可以添加其他的动画效果
                await b.ScaleTo(1.2, 250);
                await b.ScaleTo(1, 250);
            }

        }

    }


}
public static class AnimationExtensions
{
    public static Task<bool> ColorTo(this VisualElement self, Color toColor, uint length = 250, Easing easing = null)
    {
        var tcs = new TaskCompletionSource<bool>(); var fromColor = self.BackgroundColor;
        new Animation(v =>
        {
            self.BackgroundColor = Color.FromRgba(fromColor.Red + (toColor.Red - fromColor.Red) * v,
                fromColor.Green + (toColor.Green - fromColor.Green) * v, fromColor.Blue + (toColor.Blue - fromColor.Blue) * v,
                fromColor.Alpha + (toColor.Alpha - fromColor.Alpha) * v);
        })
            .Commit(self, "ColorTo", length: length, easing: easing, finished: (v, c) => tcs.SetResult(c));
        return tcs.Task;
    }
}

