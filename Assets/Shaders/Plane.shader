Shader "Unlit/Plane"
{
	Properties { 
	    _MainTex ("", any) = "" {}
        _Color ("Main Color", Color) = (1,1,1,1)

    }

    SubShader
    {
    	Tags {"Queue" = "Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha     
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            
            float4 _Color;
            sampler2D _MainTex;
            
            struct v2f {
                float4  pos : SV_POSITION;
                float2  uv : TEXCOORD0;
            };	
            
            float4 _MainTex_ST;	
            	
            v2f vert (appdata_base v){
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
                return o;
            }	
            
            float COLOR_TRESHHOLD=0.2;		
            half4 frag (v2f i) : SV_Target {		
                half4 texcol= tex2D (_MainTex, i.uv);

                clip(texcol.a - 0.2f);
                
                texcol = _Color;
                
                return  texcol;
            }
            
            ENDCG
        }
    }
}
