#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler2D TextureSampler : register(s0); // Main texture

// Maximum number of rectangles (you can adjust this based on your needs)
#define MAX_RECTANGLES 10

// Rectangle data: [x, y, width, height]
float4 Rectangles[MAX_RECTANGLES];
int RectangleCount; // Number of active rectangles

float4 MainPS(float2 texCoord : TEXCOORD0) : COLOR
{
    float4 color = tex2D(TextureSampler, texCoord); // Sample the main texture

    // Convert texCoord (0-1) to pixel coordinates
    float2 pixelCoord = texCoord;

    // Check if the pixel lies inside any rectangle
    bool isMasked = false;
    for (int i = 0; i < RectangleCount; i++)
    {
        float4 rect = Rectangles[i];
        if (pixelCoord.x >= rect.x && pixelCoord.x <= rect.x + rect.z &&
            pixelCoord.y >= rect.y && pixelCoord.y <= rect.y + rect.w)
        {
            isMasked = true;
            break;
        }
    }

    // If masked, make the pixel transparent
    if (isMasked)
    {
        return float4(0, 0, 0, 0); // Fully transparent pixel
    }

    return color; // Render the original color
}

technique Technique1
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
}