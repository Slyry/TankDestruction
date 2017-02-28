Shader "Hidden/BrightPassFilterForBloom"
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "" {}
	}
	
	CGINCLUDE
	
	#include "UnityCG.cginc"
	
	struct v2f 
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};
	
	sampler2D _MainTex;	
<<<<<<< HEAD:TankDestructionProject/Assets/Standard Assets/Effects/ImageEffects/Shaders/_BloomAndFlares/BrightPassFilter.shader
	
=======
<<<<<<< HEAD
	
=======
	half4     _MainTex_ST;

>>>>>>> master
>>>>>>> refs/remotes/origin/master:Tank Destruction Project/Assets/Standard Assets/Effects/ImageEffects/Shaders/_BloomAndFlares/BrightPassFilter.shader
	half4 threshold;
	half useSrcAlphaAsMask;
		
	v2f vert( appdata_img v ) 
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
<<<<<<< HEAD:TankDestructionProject/Assets/Standard Assets/Effects/ImageEffects/Shaders/_BloomAndFlares/BrightPassFilter.shader
		o.uv =  v.texcoord.xy;
=======
<<<<<<< HEAD
		o.uv =  v.texcoord.xy;
=======
		o.uv = UnityStereoScreenSpaceUVAdjust(v.texcoord.xy, _MainTex_ST);
>>>>>>> master
>>>>>>> refs/remotes/origin/master:Tank Destruction Project/Assets/Standard Assets/Effects/ImageEffects/Shaders/_BloomAndFlares/BrightPassFilter.shader
		return o;
	} 
	
	half4 frag(v2f i) : SV_Target 
	{
		half4 color = tex2D(_MainTex, i.uv);
		//color = color * saturate((color-threshhold.x) * 75.0); // didn't go well with HDR and din't make sense
		color = color * lerp(1.0, color.a, useSrcAlphaAsMask);
		color = max(half4(0,0,0,0), color-threshold.x);
		return color;
	}

	ENDCG 
	
	Subshader 
	{
		Pass 
 		{
			  ZTest Always Cull Off ZWrite Off
		
		      CGPROGRAM
		      
		      #pragma vertex vert
		      #pragma fragment frag
		
		      ENDCG
		}
	}
	Fallback off
}
