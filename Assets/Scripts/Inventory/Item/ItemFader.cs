//使用第三方插件DOTween进行颜色透明度过渡
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
//这个脚本实现角色碰撞树时的半透明效果
public class ItemFader : MonoBehaviour
{
    //获取树冠的SpriteRenderer，目的时为了修改Sprite的颜色透明度
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        //函数被唤醒（调用）时初始化获取Sprite组件
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //角色碰到树冠时
    public void FadeIn()
    {
        Color targetcolor = new Color(1, 1, 1, 1);
        spriteRenderer.DOColor(targetcolor, Settings.fadeDuration);
    }
    //角色离开树冠时
    public void FadeOut()
    {
        Color targetcolor = new Color(1, 1, 1, Settings.targetAlpha);
        spriteRenderer.DOColor(targetcolor, Settings.fadeDuration);
    }
}
