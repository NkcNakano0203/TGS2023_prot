Shader "Unlit/LaserShader"
{
    

    Properties
    {        
        _MainTex ("Main map", 2D) = "white" {}        

        [HDR]_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)   
        inside_Color("inside_Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Float("ColorWidth", Float) = 1
    }
    SubShader
    {
        Tags 
        {
            "RenderType"="Opaque" 
            "RenderPipeline"="UniversalPipeline"   
        }
        
        LOD 100

        Pass
        {
            // 両面を描画
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                half3 normal : NORMAL;
                half4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                half3 normal : TEXCOORD1; //法線
                half3 tangent : TEXCOORD2; //接線
                half3 binormal : TEXCOORD3; //従法線
                float4 worldPos : TEXCOORD4;
            };
            

            fixed4 _Color;
            fixed4 inside_Color;
            fixed4 Emission_Color;
            float _Float;
             
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _NormalMap_ST;   

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex); //頂点をMVP行列変換
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); //テクスチャスケールとタイリングを加味
                o.normal = UnityObjectToWorldNormal(v.normal); //法線をワールド座標系に変換
                o.tangent = normalize(mul(unity_ObjectToWorld, v.tangent)).xyz; //接線をワールド座標系に変換
                o.binormal = cross(v.normal, v.tangent) * v.tangent.w; //変換前の法線と接線から従法線を計算
                o.binormal = normalize(mul(unity_ObjectToWorld, o.binormal)); //従法線をワールド座標系に変換
                o.worldPos = v.vertex; //変換前の頂点を渡す

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                
                float3 normal = i.normal;
                normal = normalize(normal); //法線ベクトルを正規化

                
                float3 toEye = _WorldSpaceCameraPos - i.worldPos; //カメラからの視線ベクトルを計算
                toEye = normalize(toEye); //視線ベクトルを正規化
                
                
                
                
                //レーザーの白い部分と色のついた部分の設定
                float angle=saturate(pow(dot(toEye, normal),_Float));                
                
                float4 finalColor =(_Color*(1-angle))+(inside_Color *(angle));
                
                return finalColor;
            }
            ENDCG
        }
    }        
}