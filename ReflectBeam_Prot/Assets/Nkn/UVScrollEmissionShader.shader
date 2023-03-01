Shader "Unlit/UVScrollEmissionShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor("MainColor",Color)=(1,1,1)
        _ScrollSpeed("ScrollSpeed",Float)=1.0
        _EmissionMap("EmissionMap",2D)="white"{}
        [HDR] _EmissionColor ("Emission Color", Color) = (0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;
            float _ScrollSpeed;
            sampler2D _EmissionMap;
            float4 _EmissionColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                v2f uv2 = i;    // 片方のテクスチャだけUVスクロールさせるために複製
                uv2.uv.y += _ScrollSpeed * -_Time;

                fixed4 col = tex2D(_MainTex, uv2.uv) * _MainColor + tex2D(_EmissionMap,i.uv) * _EmissionColor;
                
                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG
        }
    }
}