using System;
using System.Collections.Generic;

public class AotClass
{
	public static List<Type> scanNames = new List<Type>()
	{
        // 时间
        typeof(UnityEngine.Time),
        
        // 物理
        typeof(UnityEngine.Physics),
		typeof(UnityEngine.Collider),
		typeof(UnityEngine.BoxCollider),
		typeof(UnityEngine.CapsuleCollider),
		typeof(UnityEngine.MeshCollider),
		typeof(UnityEngine.SphereCollider),
		typeof(UnityEngine.WheelCollider),
		typeof(UnityEngine.TerrainCollider),
		typeof(UnityEngine.WheelCollider),

		typeof(UnityEngine.Physics2D),
		typeof(UnityEngine.Collider2D),
		typeof(UnityEngine.BoxCollider2D),
		typeof(UnityEngine.CapsuleCollider2D),
		typeof(UnityEngine.CircleCollider2D),
		typeof(UnityEngine.CompositeCollider2D),
		typeof(UnityEngine.EdgeCollider2D),
		typeof(UnityEngine.PolygonCollider2D),

        // 资源
        typeof(UnityEngine.Resources),
		typeof(UnityEngine.AssetBundle),
		typeof(UnityEngine.AssetBundleManifest),
		typeof(UnityEngine.AssetBundleRequest),

        // 输入
        typeof(UnityEngine.Input),
		typeof(UnityEngine.AndroidInput),

        // Application
        typeof(UnityEngine.Application),

        // 基础组件
        typeof(UnityEngine.Animation),
		typeof(UnityEngine.AnimationClip),
		typeof(UnityEngine.AnimationCurve),
		typeof(UnityEngine.AnimationState),
		typeof(UnityEngine.Animator),
		typeof(UnityEngine.AudioClip),
		typeof(UnityEngine.AudioSource),
		typeof(UnityEngine.AudioListener),

		typeof(UnityEngine.Object),
		typeof(UnityEngine.GameObject),
		typeof(UnityEngine.Transform),
		typeof(UnityEngine.Rigidbody),
		typeof(UnityEngine.ParticleSystem),
		typeof(UnityEngine.Camera),
		typeof(UnityEngine.Light),
		typeof(UnityEngine.Mesh),
		typeof(UnityEngine.MeshFilter),
		typeof(UnityEngine.Texture),
		typeof(UnityEngine.Material),
		typeof(UnityEngine.Shader),

		typeof(UnityEngine.Renderer),
		typeof(UnityEngine.ParticleSystemRenderer),

        // UI
        typeof(UnityEngine.Canvas),
		typeof(UnityEngine.CanvasGroup),
		typeof(UnityEngine.CanvasRenderer),
		typeof(UnityEngine.UI.Button),
		typeof(UnityEngine.UI.ContentSizeFitter),
		typeof(UnityEngine.UI.Dropdown),
		typeof(UnityEngine.UI.Graphic),
		typeof(UnityEngine.UI.GraphicRaycaster),
		typeof(UnityEngine.UI.GridLayoutGroup),
		typeof(UnityEngine.UI.HorizontalLayoutGroup),
		typeof(UnityEngine.UI.VerticalLayoutGroup),
		typeof(UnityEngine.UI.LayoutElement),
		typeof(UnityEngine.UI.LayoutGroup),
		typeof(UnityEngine.UI.LayoutRebuilder),
		typeof(UnityEngine.UI.LayoutUtility),
		typeof(UnityEngine.UI.Mask),
		typeof(UnityEngine.UI.MaskableGraphic),
		typeof(UnityEngine.UI.MaskUtilities),
		typeof(UnityEngine.UI.Outline),
		typeof(UnityEngine.UI.RawImage),
		typeof(UnityEngine.UI.RectMask2D),
		typeof(UnityEngine.UI.Scrollbar),
		typeof(UnityEngine.UI.ScrollRect),
		typeof(UnityEngine.UI.Selectable),
		typeof(UnityEngine.UI.Shadow),
		typeof(UnityEngine.UI.Slider),
		typeof(UnityEngine.UI.Text),
		typeof(UnityEngine.UI.Toggle),
		typeof(UnityEngine.UI.ToggleGroup),

		typeof(UnityEngine.AI.NavMesh),
		typeof(UnityEngine.AI.NavMeshAgent),
		typeof(UnityEngine.AI.NavMeshBuilder),
		typeof(UnityEngine.AI.NavMeshObstacle),

		typeof(UnityEngine.RenderTexture),
		typeof(UnityEngine.Screen),
		typeof(UnityEngine.Skybox),
		typeof(UnityEngine.Terrain),
		typeof(UnityEngine.TextAsset),
		typeof(UnityEngine.Texture2D),

		typeof(UnityEngine.SceneManagement.SceneManager),
		typeof(UnityEngine.SceneManagement.SceneUtility),
	};
	#region 自动生成请勿修改
	void Placeholder()
	{
		// PLACE_START
		UnityEngine.Vector3 temp0;
		UnityEngine.PhysicsScene temp1;
		UnityEngine.RaycastHit temp2;
		UnityEngine.Collider temp3;
		UnityEngine.Rigidbody temp4;
		UnityEngine.ArticulationBody temp5;
		UnityEngine.Bounds temp6;
		UnityEngine.PhysicMaterial temp7;
		UnityEngine.Vector2 temp8;
		UnityEngine.Matrix4x4 temp9;
		UnityEngine.Mesh temp10;
		UnityEngine.MeshColliderCookingOptions temp11;
		UnityEngine.JointSpring temp12;
		UnityEngine.WheelFrictionCurve temp13;
		UnityEngine.TerrainData temp14;
		UnityEngine.PhysicsScene2D temp15;
		UnityEngine.SimulationMode2D temp16;
		UnityEngine.PhysicsJobOptions2D temp17;
		UnityEngine.Color temp18;
		UnityEngine.ColliderDistance2D temp19;
		UnityEngine.RaycastHit2D temp20;
		UnityEngine.Collider2D temp21;
		UnityEngine.CompositeCollider2D temp22;
		UnityEngine.Rigidbody2D temp23;
		UnityEngine.PhysicsMaterial2D temp24;
		UnityEngine.CapsuleDirection2D temp25;
		UnityEngine.CompositeCollider2D.GeometryType temp26;
		UnityEngine.CompositeCollider2D.GenerationType temp27;
		UnityEngine.Object temp28;
		UnityEngine.ResourceRequest temp29;
		UnityEngine.AsyncOperation temp30;
		UnityEngine.AssetBundle temp31;
		UnityEngine.AssetBundleCreateRequest temp32;
		UnityEngine.AssetBundleRequest temp33;
		System.String temp34;
		UnityEngine.AssetBundleRecompressOperation temp35;
		UnityEngine.Hash128 temp36;
		UnityEngine.IMECompositionMode temp37;
		UnityEngine.DeviceOrientation temp38;
		UnityEngine.LocationService temp39;
		UnityEngine.Compass temp40;
		UnityEngine.Gyroscope temp41;
		UnityEngine.Touch temp42;
		UnityEngine.AccelerationEvent temp43;
		UnityEngine.ApplicationInstallMode temp44;
		UnityEngine.ApplicationSandboxType temp45;
		UnityEngine.ThreadPriority temp46;
		UnityEngine.RuntimePlatform temp47;
		UnityEngine.SystemLanguage temp48;
		UnityEngine.NetworkReachability temp49;
		UnityEngine.StackTraceLogType temp50;
		UnityEngine.AnimationClip temp51;
		UnityEngine.WrapMode temp52;
		UnityEngine.AnimationState temp53;
		UnityEngine.AnimationCullingType temp54;
		System.Collections.IEnumerator temp55;
		UnityEngine.AnimationEvent temp56;
		System.Array temp57;
		UnityEngine.Keyframe temp58;
		UnityEngine.AnimationCurve temp59;
		UnityEngine.AnimationBlendMode temp60;
		UnityEngine.Quaternion temp61;
		UnityEngine.AnimatorUpdateMode temp62;
		UnityEngine.AnimatorControllerParameter temp63;
		UnityEngine.Transform temp64;
		UnityEngine.AnimatorCullingMode temp65;
		UnityEngine.AnimatorRecorderMode temp66;
		UnityEngine.RuntimeAnimatorController temp67;
		UnityEngine.Avatar temp68;
		UnityEngine.Playables.PlayableGraph temp69;
		UnityEngine.ScriptableObject temp70;
		UnityEngine.StateMachineBehaviour temp71;
		UnityEngine.AnimatorStateInfo temp72;
		UnityEngine.AnimatorTransitionInfo temp73;
		UnityEngine.AnimatorClipInfo temp74;
		UnityEngine.AudioClipLoadType temp75;
		UnityEngine.AudioDataLoadState temp76;
		UnityEngine.AudioClip temp77;
		UnityEngine.Audio.AudioMixerGroup temp78;
		UnityEngine.AudioVelocityUpdateMode temp79;
		UnityEngine.AudioRolloffMode temp80;
		UnityEngine.HideFlags temp81;
		UnityEngine.SceneManagement.Scene temp82;
		UnityEngine.GameObject temp83;
		UnityEngine.Component temp84;
		UnityEngine.RigidbodyConstraints temp85;
		UnityEngine.CollisionDetectionMode temp86;
		UnityEngine.RigidbodyInterpolation temp87;
		UnityEngine.ParticleSystem.MainModule temp88;
		UnityEngine.ParticleSystem.EmissionModule temp89;
		UnityEngine.ParticleSystem.ShapeModule temp90;
		UnityEngine.ParticleSystem.VelocityOverLifetimeModule temp91;
		UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule temp92;
		UnityEngine.ParticleSystem.InheritVelocityModule temp93;
		UnityEngine.ParticleSystem.LifetimeByEmitterSpeedModule temp94;
		UnityEngine.ParticleSystem.ForceOverLifetimeModule temp95;
		UnityEngine.ParticleSystem.ColorOverLifetimeModule temp96;
		UnityEngine.ParticleSystem.ColorBySpeedModule temp97;
		UnityEngine.ParticleSystem.SizeOverLifetimeModule temp98;
		UnityEngine.ParticleSystem.SizeBySpeedModule temp99;
		UnityEngine.ParticleSystem.RotationOverLifetimeModule temp100;
		UnityEngine.ParticleSystem.RotationBySpeedModule temp101;
		UnityEngine.ParticleSystem.ExternalForcesModule temp102;
		UnityEngine.ParticleSystem.NoiseModule temp103;
		UnityEngine.ParticleSystem.CollisionModule temp104;
		UnityEngine.ParticleSystem.TriggerModule temp105;
		UnityEngine.ParticleSystem.SubEmittersModule temp106;
		UnityEngine.ParticleSystem.TextureSheetAnimationModule temp107;
		UnityEngine.ParticleSystem.LightsModule temp108;
		UnityEngine.ParticleSystem.TrailModule temp109;
		UnityEngine.ParticleSystem.CustomDataModule temp110;
		UnityEngine.ParticleSystemSimulationSpace temp111;
		UnityEngine.ParticleSystemScalingMode temp112;
		UnityEngine.Color32 temp113;
		UnityEngine.ParticleSystem.PlaybackState temp114;
		UnityEngine.ParticleSystem.Trails temp115;
		Unity.Jobs.JobHandle temp116;
		UnityEngine.RenderingPath temp117;
		UnityEngine.Rendering.OpaqueSortMode temp118;
		UnityEngine.TransparencySortMode temp119;
		UnityEngine.CameraType temp120;
		System.Single temp121;
		UnityEngine.CameraClearFlags temp122;
		UnityEngine.DepthTextureMode temp123;
		UnityEngine.Camera.GateFitMode temp124;
		UnityEngine.Rect temp125;
		UnityEngine.RenderTexture temp126;
		UnityEngine.Camera temp127;
		UnityEngine.StereoTargetEyeMask temp128;
		UnityEngine.Camera.MonoOrStereoscopicEye temp129;
		UnityEngine.Ray temp130;
		UnityEngine.Rendering.CommandBuffer temp131;
		UnityEngine.LightType temp132;
		UnityEngine.LightShape temp133;
		UnityEngine.Vector4 temp134;
		UnityEngine.Flare temp135;
		UnityEngine.LightBakingOutput temp136;
		UnityEngine.LightShadowCasterMode temp137;
		UnityEngine.LightShadows temp138;
		UnityEngine.Rendering.LightShadowResolution temp139;
		UnityEngine.Texture temp140;
		UnityEngine.LightRenderMode temp141;
		UnityEngine.LightmapBakeType temp142;
		UnityEngine.Light temp143;
		UnityEngine.Rendering.IndexFormat temp144;
		System.Int32 temp145;
		UnityEngine.BoneWeight temp146;
		UnityEngine.Rendering.VertexAttributeDescriptor temp147;
		UnityEngine.Rendering.VertexAttributeFormat temp148;
		UnityEngine.Rendering.SubMeshDescriptor temp149;
		UnityEngine.MeshTopology temp150;
		UnityEngine.Rendering.VertexAttribute temp151;
		UnityEngine.Mesh.MeshDataArray temp152;
		UnityEngine.AnisotropicFiltering temp153;
		UnityEngine.Experimental.Rendering.GraphicsFormat temp154;
		UnityEngine.Rendering.TextureDimension temp155;
		UnityEngine.TextureWrapMode temp156;
		UnityEngine.FilterMode temp157;
		UnityEngine.ColorSpace temp158;
		UnityEngine.UnityException temp159;
		UnityEngine.Shader temp160;
		UnityEngine.MaterialGlobalIlluminationFlags temp161;
		UnityEngine.Material temp162;
		UnityEngine.Rendering.ShaderTagId temp163;
		UnityEngine.Rendering.ShaderPropertyType temp164;
		UnityEngine.Rendering.ShaderPropertyFlags temp165;
		UnityEngine.Rendering.ShadowCastingMode temp166;
		UnityEngine.MotionVectorGenerationMode temp167;
		UnityEngine.Rendering.LightProbeUsage temp168;
		UnityEngine.Rendering.ReflectionProbeUsage temp169;
		UnityEngine.Experimental.Rendering.RayTracingMode temp170;
		UnityEngine.ParticleSystemRenderSpace temp171;
		UnityEngine.ParticleSystemRenderMode temp172;
		UnityEngine.ParticleSystemSortMode temp173;
		UnityEngine.SpriteMaskInteraction temp174;
		UnityEngine.RenderMode temp175;
		UnityEngine.AdditionalCanvasShaderChannels temp176;
		UnityEngine.Canvas temp177;
		UnityEngine.UI.Button.ButtonClickedEvent temp178;
		UnityEngine.UI.ContentSizeFitter.FitMode temp179;
		UnityEngine.RectTransform temp180;
		UnityEngine.UI.Text temp181;
		UnityEngine.UI.Image temp182;
		UnityEngine.UI.Dropdown.DropdownEvent temp183;
		UnityEngine.CanvasRenderer temp184;
		UnityEngine.UI.GraphicRaycaster.BlockingObjects temp185;
		UnityEngine.LayerMask temp186;
		UnityEngine.UI.GridLayoutGroup.Corner temp187;
		UnityEngine.UI.GridLayoutGroup.Axis temp188;
		UnityEngine.UI.GridLayoutGroup.Constraint temp189;
		UnityEngine.RectOffset temp190;
		UnityEngine.TextAnchor temp191;
		UnityEngine.UI.Graphic temp192;
		UnityEngine.UI.MaskableGraphic.CullStateChangedEvent temp193;
		UnityEngine.UI.RectMask2D temp194;
		UnityEngine.Vector2Int temp195;
		UnityEngine.UI.Scrollbar.Direction temp196;
		UnityEngine.UI.Scrollbar.ScrollEvent temp197;
		UnityEngine.UI.Selectable temp198;
		UnityEngine.UI.ScrollRect.MovementType temp199;
		UnityEngine.UI.Scrollbar temp200;
		UnityEngine.UI.ScrollRect.ScrollbarVisibility temp201;
		UnityEngine.UI.ScrollRect.ScrollRectEvent temp202;
		UnityEngine.UI.Navigation temp203;
		UnityEngine.UI.Selectable.Transition temp204;
		UnityEngine.UI.ColorBlock temp205;
		UnityEngine.UI.SpriteState temp206;
		UnityEngine.UI.AnimationTriggers temp207;
		UnityEngine.Animator temp208;
		UnityEngine.UI.Slider.Direction temp209;
		UnityEngine.UI.Slider.SliderEvent temp210;
		UnityEngine.TextGenerator temp211;
		UnityEngine.Font temp212;
		UnityEngine.HorizontalWrapMode temp213;
		UnityEngine.VerticalWrapMode temp214;
		UnityEngine.FontStyle temp215;
		UnityEngine.TextGenerationSettings temp216;
		UnityEngine.UI.ToggleGroup temp217;
		UnityEngine.UI.Toggle temp218;
		UnityEngine.AI.NavMeshTriangulation temp219;
		UnityEngine.AI.NavMeshDataInstance temp220;
		UnityEngine.AI.NavMeshLinkInstance temp221;
		UnityEngine.AI.NavMeshBuildSettings temp222;
		UnityEngine.AI.OffMeshLinkData temp223;
		UnityEngine.AI.NavMeshPathStatus temp224;
		UnityEngine.AI.NavMeshPath temp225;
		UnityEngine.AI.ObstacleAvoidanceType temp226;
		UnityEngine.AI.NavMeshBuildSource temp227;
		UnityEngine.AI.NavMeshData temp228;
		UnityEngine.AI.NavMeshObstacleShape temp229;
		UnityEngine.VRTextureUsage temp230;
		UnityEngine.RenderTextureMemoryless temp231;
		UnityEngine.RenderTextureFormat temp232;
		UnityEngine.RenderBuffer temp233;
		UnityEngine.RenderTextureDescriptor temp234;
		UnityEngine.Resolution temp235;
		UnityEngine.FullScreenMode temp236;
		UnityEngine.ScreenOrientation temp237;
		UnityEngine.TerrainRenderFlags temp238;
		UnityEngine.TextureFormat temp239;
		UnityEngine.Terrain temp240;
		System.Byte temp241;
		UnityEngine.Texture2D temp242;
		// PLACE_END
	}
	#endregion
}
