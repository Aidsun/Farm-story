//ʹ�õ��������DOTween������ɫ͸���ȹ���
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
//����ű�ʵ�ֽ�ɫ��ײ��ʱ�İ�͸��Ч��
public class ItemFader : MonoBehaviour
{
    //��ȡ���ڵ�SpriteRenderer��Ŀ��ʱΪ���޸�Sprite����ɫ͸����
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        //���������ѣ����ã�ʱ��ʼ����ȡSprite���
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //��ɫ��������ʱ
    public void FadeIn()
    {
        Color targetcolor = new Color(1, 1, 1, 1);
        spriteRenderer.DOColor(targetcolor, Settings.fadeDuration);
    }
    //��ɫ�뿪����ʱ
    public void FadeOut()
    {
        Color targetcolor = new Color(1, 1, 1, Settings.targetAlpha);
        spriteRenderer.DOColor(targetcolor, Settings.fadeDuration);
    }
}
