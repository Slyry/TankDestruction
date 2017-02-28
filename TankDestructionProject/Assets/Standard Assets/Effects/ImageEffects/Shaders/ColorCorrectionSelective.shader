Shader "Hidden/ColorCorrectionSelective" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
	}
	
	CGINCLUDE
	
	#include "UnityCG.cginc"
	
	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};
	
	sampler2D _MainTex;
<<<<<<< HEAD:TankDestructionProject/Assets/Standard Assets/Effects/ImageEffects/Shaders/ColorCorrectionSelective.shader
	
=======
<<<<<<< HEAD
	
=======
	half4 _MainTex_ST;

>>>>>>> master
>>>>>>> refs/remotes/origin/master:Tank Destruction Project/Assets/Standard Assets/Effects/ImageEffects/Shaders/ColorCorrectionSelective.shader
	float4 selColor;
	float4 targetColor;
	
	v2f vert( appdata_img v ) {
		v2f o;
		o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
<<<<<<< HEAD:TankDestructionProject/Assets/Standard Assets/Effects/ImageEffects/Shaders/ColorCorrectionSelective.shader
		o.uv = v.texcoord.xy;
		return o;
	} 
=======
<<<<<<< HEAD
		o.uv = v.texcoord.xy;
		return o;
	} 
=======
		o.uv = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy, _MainTex_ST);
		return o;
	}
>>>>>>> master
>>>>>>> refs/remotes/origin/master:Tank Destruction Project/Assets/Standard Assets/Effects/ImageEffects/Shaders/ColorCorrectionSelective.shader
	
	fixed4 frag(v2f i) : SV_Target {
		fixed4 color = tex2D (_MainTex, i.uv); 
	
		fixed diff = saturate (2.0 * length (color.rgb - selColor.rgb));
		color = lerp (targetColor, color, diff);
		return color;
	}

	ENDCG  
	
Subshader {
 Pass {
	  ZTest Always Cull Off ZWrite Off

      CGPROGRAM
      
      #pragma vertex vert
      #pragma fragment frag
      
      ENDCG
  }
}

Fallback off
	
} 
