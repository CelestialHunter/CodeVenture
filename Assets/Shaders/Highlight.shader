//Shader "Unlit/Highlight"
//{
//    Properties
//    {
//        _Color ("Main Color", Color) = (0.5,0.5,0.5,1)
//        _MainTex ("Albedo (RGB)", 2D) = "white" {}
//        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
//        _OutlineWidth ("Outline Width", Range(1,5)) = 1
//    }
	
//	CGINLCUDE
	
//	#include "UnityCG.cginc"
	
//	struct appdata 
//    {
//	    float4 vertex : POSITION;
//		float3 normal : NORMAL;
//    };

//	struct v2f
//	{
//		float4 pos : POSITION;
//		float4 color: COLOR;
//		float3 normal : NORMAL;
//	};

//	float _OutlineWidth;
//	float4 _OutlineColor;

//	v2f vert(appdata v)
//	{
//		v.vertex.xyz *= _Outline;	

//		v2f o;
//		o.pos = UnityObjectToClipPos(v.vertex);
//		o.color = _OutlineColor;
//		return 0;		
//	};

//	ENDCG
	
//    SubShader
//    {
//        Pass // render the outline 
//		{ 
//			ZWrite Off

//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag

//			half4 frag(v2f i): COLOR
//			{
//				return _OutlineColor
//			}
//			ENDCG
//		}
		
//		Pass // normal render 
//		{
//			//CGPROGRAM
			
//			//#pragma vertex vert
//			//#pragma fragment frag
//			//// make fog work
//			//#pragma multi_compile_fog

//			//#include "UnityCG.cginc"
			
//			//struct appdata 
//			//{
//			//	float4 vertex : POSITION;
//			//	float2 uv : TEXCOORD0;
//			//}

//			//struct v2f
//			//{
//			//	float2 uv : TEXCOORD0;
//			//	UNITY_FOG_COORDS(1);
//			//	float4 vertex : SV_POSITION;
//			//};

//			//sampler2D _MainTex;
//			//float4 _MainTex_ST;

//			//v2f vert(appdata v)
//			//{
//			//	v2f o;
//			//	o.vertex = UnityObjectToClipPos(v.vertex);
//			//	o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//			//	UNITY_TRANSFER_FOG(o,o.vertex);
//			//	return o;
//			//}
			
//			//fixed4 frag(v2f i) : SV_Target
//			//{
//			//	fixed4 col = tex2D(_MainTex, i.uv);
//			//	UNITY_APPLY_FOG(i.fogCoord, col);
//			//	return col;
//			//}

//			//ENDCG

//			ZWrite On

//            Material
//            {
//                Diffuse[_Color]
//                Ambient[_Color]
//            }

//            Lighting On    
            
//            SetTexture[_MainTex]
//            {
//                ConstantColor[_Color]
//            }

//            SetTexture[_MainTex]
//            {
//                Combine previous * primary DOUBLE
//            }
//		}
//    }
//}


Shader "Unlit/Highlight"{
    Properties { 
        _Color ("Main Color", Color) = (0,0,0,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (255,183,0,1)
        _OutlineWidth ("Outline Width", Range(1,1.1)) = 1.05
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    struct appdata
    {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
    };

    struct v2f
    {
        float4 pos : POSITION;
        float3 normal : NORMAL;
    };
    
    float _OutlineWidth;
    float4 _OutlineColor;

    v2f vert (appdata v)
    {
        v.vertex.xyz *= _OutlineWidth;

        v2f o;
        o.pos = UnityObjectToClipPos(v.vertex);
        return o;
    }
    ENDCG

    Subshader
    {

    Tags { "Queue" = "Transparent"}
        LOD 3000
        Pass //Rendering Outlines 
        {
            Zwrite Off

    CGPROGRAM
    #pragma vertex vert
    #pragma fragment frag 

    half4 frag(v2f i) : COLOR
    {
        return _OutlineColor;
    }
            ENDCG
        }
        Pass // Normal Render
        {
            ZWrite On

            Material
            {
                Diffuse[_Color]
                Ambient[_Color]
            }

            Lighting On    
            
            SetTexture[_MainTex]
            {
                ConstantColor[_Color]
            }

            SetTexture[_MainTex]
            {
                Combine previous * primary DOUBLE
            }

        }
    }

    }