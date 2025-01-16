Shader "Custom/BloodWritingShader" 
{
    // Define shader properties that can be modified in the Unity Editor or via scripts.
    Properties
    {
        _MainTex ("Blood Writing Texture", 2D) = "white" {} // The main texture representing the blood writing.
        _RevealTex ("Reveal Mask", 2D) = "white" {}         // The mask texture that controls which parts of the main texture are revealed.
        _RevealProgress ("Reveal Progress", Range(0, 1)) = 0 // A float value (0 to 1) that indicates the progress of the reveal effect.
    }
    
    SubShader
    {
        // Define rendering options for the shader.
        Tags { "Queue"="Transparent" "RenderType"="Transparent" } // Set rendering to the Transparent queue for proper blending.
        Lighting Off // Disable lighting for this shader, as the effect doesn't rely on lighting.
        ZWrite Off // Disable depth writing to prevent this object from affecting the depth buffer.
        Blend SrcAlpha OneMinusSrcAlpha // Use alpha blending for smooth transparency effects.

        Pass
        {
            // Begin the programmable shader section.
            CGPROGRAM
            #pragma vertex vert // Define the vertex shader function.
            #pragma fragment frag // Define the fragment shader function.
            #include "UnityCG.cginc" // Include Unity's built-in shader library for common functions.

            // Define the structure for vertex input (appdata_t).
            struct appdata_t
            {
                float4 vertex : POSITION; // Vertex position in 3D space.
                float2 uv : TEXCOORD0; // UV coordinates for texture mapping.
            };

            // Define the structure for vertex output (v2f).
            struct v2f
            {
                float2 uv : TEXCOORD0; // Pass UV coordinates to the fragment shader.
                float4 vertex : SV_POSITION; // Transformed vertex position in clip space.
            };

            // Declare textures and uniform variables.
            sampler2D _MainTex; // The main texture (blood writing).
            sampler2D _RevealTex; // The reveal mask texture.
            float _RevealProgress; // The reveal progress value (controlled externally).

            // Vertex shader: processes vertices and prepares data for the fragment shader.
            v2f vert (appdata_t v)
            {
                v2f o; 
                o.vertex = UnityObjectToClipPos(v.vertex); // Transform vertex position to clip space.
                o.uv = v.uv; // Pass the UV coordinates to the output structure.
                return o; // Return the processed data to the fragment shader.
            }

            // Fragment shader: calculates the color for each pixel.
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.uv); // Sample the main texture at the UV coordinates.
                fixed4 revealMask = tex2D(_RevealTex, i.uv); // Sample the reveal mask texture at the same UV coordinates.

                // Determine if this pixel should be revealed based on the mask and progress value.
                if (revealMask.r < _RevealProgress)
                    mainColor.a = 0; // Set the alpha to 0, making the pixel fully transparent.

                return mainColor; // Return the final color, with transparency applied.
            }
            ENDCG // End the programmable shader section.
        }
    }
}
