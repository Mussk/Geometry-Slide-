using UnityEngine;

[ExecuteAlways]
public class ShaderAnimationController : MonoBehaviour
{
    private static readonly int AnimationEnabled = Shader.PropertyToID("_IsAnimating");
    
    
    public Material material;
    
    void Update()
    {
        material.SetFloat(AnimationEnabled, Application.isPlaying ? 1f : 0f);
    }
}
