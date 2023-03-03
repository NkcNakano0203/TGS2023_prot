Shader "Custom/GoalEffectShader"
{
    Properties
    {
        [HDR] _MainColor ("MainColor", Color) = (0,0,0)
        _SubColor ("SubColor", Color) = (0,0,0)
        _SliceSpan ("SliceSpan", Range(0,1)) = 0.5
        _ScrollSpeed ("ScrollSpeed",Float) = 0
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

            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
            float4 _MainColor;
            float4 _SubColor;
            float _SliceSpan;
            float _ScrollSpeed;
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f o;
                // UVスクロール
                o.uv = v.uv+ -_Time.x* _ScrollSpeed;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.fogFactor = ComputeFogFactor(o.vertex.z);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = _MainColor;
                // 0か1を返す
                float ip = step(frac(i.uv.y* 15), _SliceSpan);
                col = lerp(_MainColor, _SubColor, ip);

                // apply fog
                col.rgb = MixFog(col.rgb, i.fogFactor);
                return col;
            }
            ENDHLSL
        }
    }
}