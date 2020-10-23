Shader "Custom/Terra"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		[Toggle] _IsShading("Is Active", Float) = 0
		_WaterHeight ("WaterHeight", Float) = 0.1
		_CoastHeight ("CoastHeight", Float) = 1
		_GrassHeight ("GrassHeight", Float) = 5
		_RockHeight ("RockHeight", Float) = 7
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            //float2 uv_MainTex;
			float4 color : COLOR0;
			float4 heightColor;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		float _IsShading;
		float _WaterHeight;
		float _CoastHeight;
		float _GrassHeight;
		float _RockHeight;

		float4 HeightColor(float height)
		{
			if (height < _WaterHeight)
				return float4(0.31, 0.52, 0.74, 1);
			if (height < _CoastHeight)
				return float4(0.79, 0.56, 0.26, 1);
			if (height < _GrassHeight)
				return float4(0.08, 0.43, 0.08, 1);
			if (height < _RockHeight)
				return float4(0.31, 0.31, 0.31, 1);

			return float4(0.88, 1, 1, 1);
		}

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.heightColor = (_IsShading > 0.5) ? HeightColor(v.vertex.y) : float4(0,0,0,1);
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 c = (_IsShading > 0.5) ? IN.heightColor : IN.color;		
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
