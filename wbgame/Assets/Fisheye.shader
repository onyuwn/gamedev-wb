Shader "Custom/Fisheye"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        CGINCLUDE
            #include "UnityCG.cginc"

            struct VertexData {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            Texture2D _MainTex;
            SamplerState point_clamp_sampler;
            float4 _MainTex_TexelSize;

            v2f vp(VertexData v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
        ENDCG

        Pass {
            CGPROGRAM
            #pragma vertex vp
            #pragma fragment fp

            fixed4 fp(v2f i) : SV_Target {
                i.uv *= 2;
                i.uv -= float2(1.0,1.0);
                float d = length(i.uv);
                float z = sqrt(1.0 - (d * d));
                float r = atan2(d, z) / 3.14159;
                float phi = atan2(i.uv.y, i.uv.x);
                i.uv = float2(r * cos(phi) + .5, r * sin(phi) + .5);
                float4 col = _MainTex.Sample(point_clamp_sampler, i.uv);
                return col;
                //return float4(phi, phi, phi, 1.0);
            }
            ENDCG
        }

        Pass{
            CGPROGRAM
            #pragma vertex vp
            #pragma fragment fp

            fixed4 fp(v2f i) : SV_Target {
                return _MainTex.Sample(point_clamp_sampler, i.uv);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
