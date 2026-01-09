Shader "Unlit/ShaderSquareFill"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _BaseColor ("Base Tint", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (0,1,0,1)

        _Fill ("Fill (0-1)", Range(0,1)) = 0
        _Thickness ("Outline Thickness", Range(0.002, 0.05)) = 0.02
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 col : COLOR;
            };

            sampler2D _MainTex;
            float4 _BaseColor;
            float4 _OutlineColor;
            float _Fill;
            float _Thickness;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.col = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 1️⃣ Base sprite (HER ZAMAN)
                fixed4 baseCol = tex2D(_MainTex, i.uv) * _BaseColor * i.col;

                // Alpha yoksa erken çık
                if (baseCol.a <= 0)
                    return baseCol;

                // 2️⃣ Outline mask
                float left   = i.uv.x;
                float right  = 1.0 - i.uv.x;
                float bottom = i.uv.y;
                float top    = 1.0 - i.uv.y;

                float edgeDist = min(min(left, right), min(bottom, top));
                float isOutline = step(edgeDist, _Thickness);

                if (isOutline == 0)
                    return baseCol; // İç kısım → normal sprite

                // 3️⃣ Perimeter progress (clockwise)
                float progress;

                if (i.uv.y >= 1.0 - _Thickness)                    // Top
                    progress = i.uv.x * 0.25;
                else if (i.uv.x >= 1.0 - _Thickness)               // Right
                    progress = 0.25 + (1.0 - i.uv.y) * 0.25;
                else if (i.uv.y <= _Thickness)                     // Bottom
                    progress = 0.5 + (1.0 - i.uv.x) * 0.25;
                else                                               // Left
                    progress = 0.75 + i.uv.y * 0.25;

                // 4️⃣ Fill kontrolü
                if (progress <= _Fill)
                {
                    return _OutlineColor; // Outline fill
                }

                return baseCol; // Outline ama henüz dolmamış → sprite görünsün
            }
            ENDCG
        }
    }
}
