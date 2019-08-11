Shader "Unlit/Multitap"
{
	Properties { _MainTex ("", any) = "" {} }

    SubShader
    {
        Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" }

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            
            sampler2D _MainTex;
            half4 _MainTex_TexelSize;
            half4 _BlurOffsets;
            
            struct v2f {
                float4  pos : SV_POSITION;
                float2  uv : TEXCOORD0;
                half2 taps[4] : TEXCOORD1; 
            };	
            
            	
            v2f vert( appdata_img v ) {
                v2f o; 
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord - _BlurOffsets.xy * _MainTex_TexelSize.xy;
                o.taps[0] = o.uv + _MainTex_TexelSize * _BlurOffsets.xy;
                o.taps[1] = o.uv - _MainTex_TexelSize * _BlurOffsets.xy;
                o.taps[2] = o.uv + _MainTex_TexelSize * _BlurOffsets.xy * half2(1,-1);
                o.taps[3] = o.uv - _MainTex_TexelSize * _BlurOffsets.xy * half2(1,-1);
                return o;
            }
            half4 frag(v2f i) : SV_Target {
                half4 color = tex2D(_MainTex, i.taps[0]);
                color += tex2D(_MainTex, i.taps[1]);
                color += tex2D(_MainTex, i.taps[2]);
                color += tex2D(_MainTex, i.taps[3]);
                
                return color * 0.25;
            }
            
            ENDCG
        }
    }
}
