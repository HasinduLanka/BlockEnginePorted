#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define SV_POSITION POSITION0
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

//DX

// Camera settings.
//matrix World;
matrix View;
matrix Projection;
float DiffuseLight = 1.0;
float3 CamPos = float3(0, 0, 1);
float3 LightDir = float3(0, -1, 0);



float ShadeX = 0.8;
float ShadeY = 1.0;
float ShadeZ = 0.6;

float4 IMC1 = float4(1, 0, 0, 0);
float4 IMC2 = float4(0, 1, 0, 0);
float4 IMC3 = float4(0, 0, 1, 0);




//const float2 FUp = float2(0,1);
//const float2 FDown = float2(0,-1);
//const float2 FLeft = float2(-1,0);
//const float2 FRight = float2(1,0);


//const float4 IMC4 = float4(1000, 0, 1000, 1);

//texture TTexture;


sampler Sampler = sampler_state
{
	Texture = (Texture);
};


struct VertexShaderInput
{
	float4 Position : SV_POSITION;
	float3 Normal : NORMAL0;
	float2 TextureCoordinate : TEXCOORD0;
};


struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float3 Color : COLOR0;
	float2 TextureCoordinate : TEXCOORD0;
	float3 Normal : NORMAL0;
	float3 CamDir : NORMAL1;

	//float3 lightingResult : NORMAL0;
};


// Vertex shader helper function shared between the two techniques.
VertexShaderOutput MainVS(in VertexShaderInput input, float4 instancePos:BLENDWEIGHT0)
{
	VertexShaderOutput output = ( VertexShaderOutput) 0;

	// Apply the world and camera matrices to compute the output position.

	matrix instanceTransform = matrix(IMC1, IMC2, IMC3, instancePos);

	float4 worldPosition = mul(input.Position, instanceTransform);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);

	//output.Position = float4(1.0f, 1.0f, 1.0f, 1.0f);

	//float3 worldNormal = mul(input.Normal, instanceTransform);

   //float diffuseAmount = max(-dot(worldNormal, LightDirection1), -dot(worldNormal, LightDirection2));

	float diffuseAmount = (abs(input.Normal.x) * ShadeX) + (abs(input.Normal.y) * ShadeY) + (abs(input.Normal.z) * ShadeZ);



	//float3 lightingResult = saturate((diffuseAmount * DiffuseLight) + AmbientLight);

	output.Color = saturate(diffuseAmount) * DiffuseLight;

	output.Normal = input.Normal;
	output.CamDir = normalize(input.Position.xyz - CamPos);


	output.TextureCoordinate = input.TextureCoordinate;

	return output;
}




// Both techniques share this same pixel shader.
float4 MainPS(VertexShaderOutput input) : COLOR
{

	//float4 texel = tex2D(Sampler, input.TextureCoordinate);

	//return float4(saturate(AmbientLightColor + (texel.xyz * DiffuseColor * input.lightingResult )), texel.w);

//* distance(Origin, input.TextureCoordinate

/*
	float4 C = tex2D(Sampler, input.TextureCoordinate);

	float4 U1 = tex2D(Sampler, input.TextureCoordinate + FUp);
	float4 D1 = tex2D(Sampler, input.TextureCoordinate + FDown);

	float4 L1 = tex2D(Sampler, input.TextureCoordinate + FLeft);
	float4 R1 = tex2D(Sampler, input.TextureCoordinate + FRight);

	float4 texel = (((((U1 + D1) * 0.5) + C ) * 0.5) + ((((L1 + R1) * 0.5) + C ) * 0.5)) * 0.5;*/

	//float4 texel = tex2D(Sampler, input.TextureCoordinate);
	//float Rad = max(min(((input.TextureCoordinate.x + input.TextureCoordinate.y)*0.4), 1.2),0.8);

	//float4 TxRad = float4(texel.x * Rad, texel.y * Rad, texel.z * Rad, texel.w);


//float IOAngle=dot(input.Normal,LightDir)

//* dot(LightDir, input.Normal) 
	//float3 r = normalize( input.Normal - LightDir);
	//float dotProduct = -dot(input.CamDir,r);
	//float spec = max(dotProduct*0.5f, 0) * length(input.Color);


	float4 pix = tex2D(Sampler, input.TextureCoordinate);
	//float4 pix = float4(1, 1, 1,1);

	float i = (dot(LightDir, input.Normal));
	float r = (dot(input.CamDir, input.Normal));
	float theta = clamp(1 - abs(i - r), 0, 0.8) * 0.4;

	float spec = pow(theta, 2);

	//float spec = (1 - abs(i - r)) * max(0, i) / 4;




	//return  float4(pix.x + spec, pix.y + spec, pix.z + spec, pix.w);

	return  float4(pix.x * input.Color.x + spec, pix.y * input.Color.y + spec, pix.z * input.Color.z + spec, pix.w);


}


// Hardware instancing technique.
technique HardwareInstancing
{
	pass Pass1
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
}


