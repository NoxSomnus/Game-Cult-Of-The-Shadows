Shader "Custom/ParticleLightShader" // New shader name for clarity
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)  // Color of emitted light
        _Intensity ("Intensity", Float) = 1.0 // Intensity of emitted light
        _MainTex ("Albedo (RGB)", 2D) = "white" {}  // Optional texture for particle
        _Glossiness ("Smoothness", Range(0,1)) = 0.5  // Optional smoothness for particle
        _Metallic ("Metallic", Range(0,1)) = 0.0  // Optional metallic for particle
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" } // Change to Transparent for light to show through
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows // Use standard lighting model

        #pragma target 3.0

        sampler2D _MainTex;  // Optional texture

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Intensity;  // New property for light intensity

        // You might need to add instancing support if using particle systems with instancing

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Sample texture or use a constant color
            fixed4 particleColor = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            
            // Combine particle color with global illumination (consider using Shader.GetGlobalColor)
            fixed4 globalLightColor = /* Get global illumination color */;
            fixed4 finalColor = mix(particleColor, globalLightColor, pow(_Intensity, 2.0)); // Adjust intensity curve

            o.Albedo = finalColor.rgb;
            o.Emission = finalColor.rgb * _Intensity;  // Set emission based on color and intensity
            // Optional: set metallic and smoothness if using texture
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = finalColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
}
