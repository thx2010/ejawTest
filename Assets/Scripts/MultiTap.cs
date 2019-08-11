using UnityEngine;

[ExecuteInEditMode]
public class MultiTap : MonoBehaviour
{
    public Material material;


    private RenderTexture _bufferDownSample;
    private RenderTexture _bufferBlur;

    private const int Width = 2048 / 4;
    private const int Height = 2048 / 4;

    private void OnEnable()
    {
        _bufferDownSample = RenderTexture.GetTemporary(Width, Height);
        _bufferBlur = RenderTexture.GetTemporary(Width, Height);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        var offset = 1f;
        
        Graphics.BlitMultiTap(source, _bufferDownSample, material,
            new Vector2(-offset, offset),
            new Vector2(-offset, offset),
            new Vector2(offset, offset)
        );

        for (var i = 0; i < 7; i++)
        {
            offset = i * 0.4f;

            Graphics.BlitMultiTap(_bufferDownSample, _bufferBlur, material,
                new Vector2(-offset, offset),
                new Vector2(-offset, offset),
                new Vector2(offset, offset)
            );
            
            Graphics.Blit(_bufferBlur, _bufferDownSample);
        }

        Graphics.Blit(_bufferDownSample, destination);
    }
}