Shader "Custom/GoalEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScrollSpeed("ScrollSpeed",Float)=1.0
        [HDR] _EmissionColor ("Emission Color", Color) = (0,0,0)
    }
    SubShader
    {
        Tags 
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "RenderPipeline"="UniversalPipeline"
        }
        Blend SrcAlpha OneMinusSrcAlpha 
        LOD 100

        Pass
        {
            Cull Front
            Name "ForwardLit"
            Tags 
            {
                "LightMode"="UniversalForward" 
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float fogFactor: TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float _ScrollSpeed;
            float4 _EmissionColor;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                float2 offset = float2(0, -_Time.y* _ScrollSpeed);
                o.uv = TRANSFORM_TEX(v.uv + offset, _MainTex);
                o.fogFactor = ComputeFogFactor(o.vertex.z);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                col *= _EmissionColor;
                // apply fog
                col.rgb = MixFog(col.rgb, i.fogFactor);
                return col;
            }
            ENDHLSL
        }
    }
}