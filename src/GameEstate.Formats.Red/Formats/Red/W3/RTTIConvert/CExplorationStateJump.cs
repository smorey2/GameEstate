using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExplorationStateJump : CExplorationStateAbstract
	{
		[Ordinal(1)] [RED("jumpEnabled")] 		public CBool JumpEnabled { get; set;}

		[Ordinal(2)] [RED("m_SubstateE")] 		public CEnum<EJumpSubState> M_SubstateE { get; set;}

		[Ordinal(3)] [RED("m_OrientationInitialF")] 		public CFloat M_OrientationInitialF { get; set;}

		[Ordinal(4)] [RED("m_MaxHeightReachedF")] 		public CFloat M_MaxHeightReachedF { get; set;}

		[Ordinal(5)] [RED("m_SlopeAngleMaxToJump")] 		public CFloat M_SlopeAngleMaxToJump { get; set;}

		[Ordinal(6)] [RED("m_UseGenericJumpB")] 		public CBool M_UseGenericJumpB { get; set;}

		[Ordinal(7)] [RED("m_AllowSprintJumpB")] 		public CBool M_AllowSprintJumpB { get; set;}

		[Ordinal(8)] [RED("m_JumpParmsS")] 		public SJumpParams M_JumpParmsS { get; set;}

		[Ordinal(9)] [RED("m_JumpParmsGenericS")] 		public SJumpParams M_JumpParmsGenericS { get; set;}

		[Ordinal(10)] [RED("m_JumpParmsIdleS")] 		public SJumpParams M_JumpParmsIdleS { get; set;}

		[Ordinal(11)] [RED("m_JumpParmsIdleToWalkS")] 		public SJumpParams M_JumpParmsIdleToWalkS { get; set;}

		[Ordinal(12)] [RED("m_JumpParmsWalkS")] 		public SJumpParams M_JumpParmsWalkS { get; set;}

		[Ordinal(13)] [RED("m_JumpParmsWalkHighS")] 		public SJumpParams M_JumpParmsWalkHighS { get; set;}

		[Ordinal(14)] [RED("m_JumpParmsRunS")] 		public SJumpParams M_JumpParmsRunS { get; set;}

		[Ordinal(15)] [RED("m_JumpParmsSprintS")] 		public SJumpParams M_JumpParmsSprintS { get; set;}

		[Ordinal(16)] [RED("m_JumpParmsFallS")] 		public SJumpParams M_JumpParmsFallS { get; set;}

		[Ordinal(17)] [RED("m_JumpParmsHitS")] 		public SJumpParams M_JumpParmsHitS { get; set;}

		[Ordinal(18)] [RED("m_JumpParmsSlideS")] 		public SJumpParams M_JumpParmsSlideS { get; set;}

		[Ordinal(19)] [RED("m_JumpParmsVaultS")] 		public SJumpParams M_JumpParmsVaultS { get; set;}

		[Ordinal(20)] [RED("m_JumpParmsToWaterS")] 		public SJumpParams M_JumpParmsToWaterS { get; set;}

		[Ordinal(21)] [RED("m_JumpParmsKnockBackS")] 		public SJumpParams M_JumpParmsKnockBackS { get; set;}

		[Ordinal(22)] [RED("m_JumpParmsKnockBackFallS")] 		public SJumpParams M_JumpParmsKnockBackFallS { get; set;}

		[Ordinal(23)] [RED("m_JumpParmsSkateIdleS")] 		public SJumpParams M_JumpParmsSkateIdleS { get; set;}

		[Ordinal(24)] [RED("m_SprintJumpNeedsStaminaB")] 		public CBool M_SprintJumpNeedsStaminaB { get; set;}

		[Ordinal(25)] [RED("m_SprintJumpTimeExtraF")] 		public CFloat M_SprintJumpTimeExtraF { get; set;}

		[Ordinal(26)] [RED("m_SprintJumpTimeGapF")] 		public CFloat M_SprintJumpTimeGapF { get; set;}

		[Ordinal(27)] [RED("m_ConserveHorizontalCoefF")] 		public CFloat M_ConserveHorizontalCoefF { get; set;}

		[Ordinal(28)] [RED("m_ConserveVertUpCoefF")] 		public CFloat M_ConserveVertUpCoefF { get; set;}

		[Ordinal(29)] [RED("m_ConserveVertDownCoefF")] 		public CFloat M_ConserveVertDownCoefF { get; set;}

		[Ordinal(30)] [RED("m_ConserveHorizontalMaxF")] 		public CFloat M_ConserveHorizontalMaxF { get; set;}

		[Ordinal(31)] [RED("m_ConserveVertUpMaxF")] 		public CFloat M_ConserveVertUpMaxF { get; set;}

		[Ordinal(32)] [RED("m_ConserveVertDownMaxF")] 		public CFloat M_ConserveVertDownMaxF { get; set;}

		[Ordinal(33)] [RED("m_SpeedSqrMinToConserveF")] 		public CFloat M_SpeedSqrMinToConserveF { get; set;}

		[Ordinal(34)] [RED("m_ReactToHitCeilingB")] 		public CBool M_ReactToHitCeilingB { get; set;}

		[Ordinal(35)] [RED("m_HitCeilingB")] 		public CBool M_HitCeilingB { get; set;}

		[Ordinal(36)] [RED("m_BehEventPredictLandN")] 		public CName M_BehEventPredictLandN { get; set;}

		[Ordinal(37)] [RED("m_BehListenInertialJumpN")] 		public CName M_BehListenInertialJumpN { get; set;}

		[Ordinal(38)] [RED("m_BehListenFinishTakeOffN")] 		public CName M_BehListenFinishTakeOffN { get; set;}

		[Ordinal(39)] [RED("m_BehParamJumpTypeN")] 		public CName M_BehParamJumpTypeN { get; set;}

		[Ordinal(40)] [RED("m_BehEventPredictingS")] 		public CName M_BehEventPredictingS { get; set;}

		[Ordinal(41)] [RED("m_BehEventPredictTypeS")] 		public CName M_BehEventPredictTypeS { get; set;}

		[Ordinal(42)] [RED("m_BehParamIsHandledByAnimS")] 		public CName M_BehParamIsHandledByAnimS { get; set;}

		[Ordinal(43)] [RED("m_BehParamWalkOrSprintS")] 		public CName M_BehParamWalkOrSprintS { get; set;}

		[Ordinal(44)] [RED("m_BehParamNormalLandS")] 		public CName M_BehParamNormalLandS { get; set;}

		[Ordinal(45)] [RED("m_BehEventCeilingHit")] 		public CName M_BehEventCeilingHit { get; set;}

		[Ordinal(46)] [RED("m_InteractAlwaysB")] 		public CBool M_InteractAlwaysB { get; set;}

		[Ordinal(47)] [RED("m_InteractTimeMinFallF")] 		public CFloat M_InteractTimeMinFallF { get; set;}

		[Ordinal(48)] [RED("m_InteractTimeMinF")] 		public CFloat M_InteractTimeMinF { get; set;}

		[Ordinal(49)] [RED("m_InteractTimeMinVaultF")] 		public CFloat M_InteractTimeMinVaultF { get; set;}

		[Ordinal(50)] [RED("m_InteractHeightFallMaxF")] 		public CFloat M_InteractHeightFallMaxF { get; set;}

		[Ordinal(51)] [RED("m_InteractTimeAdjustingF")] 		public CFloat M_InteractTimeAdjustingF { get; set;}

		[Ordinal(52)] [RED("m_InteractDistanceExtraF")] 		public CFloat M_InteractDistanceExtraF { get; set;}

		[Ordinal(53)] [RED("m_InteractSpeedDiffAllowedF")] 		public CFloat M_InteractSpeedDiffAllowedF { get; set;}

		[Ordinal(54)] [RED("m_InteractOwnerOffsetV")] 		public Vector M_InteractOwnerOffsetV { get; set;}

		[Ordinal(55)] [RED("m_LockingJumpOnInteractionAreaB")] 		public CBool M_LockingJumpOnInteractionAreaB { get; set;}

		[Ordinal(56)] [RED("m_LockingJumpOnHorseAreaB")] 		public CBool M_LockingJumpOnHorseAreaB { get; set;}

		[Ordinal(57)] [RED("m_AllowJumpInSlopesB")] 		public CBool M_AllowJumpInSlopesB { get; set;}

		[Ordinal(58)] [RED("m_FallDistToUseHelpF")] 		public CFloat M_FallDistToUseHelpF { get; set;}

		[Ordinal(59)] [RED("m_FallRecoverMaxHeightUpF")] 		public CFloat M_FallRecoverMaxHeightUpF { get; set;}

		[Ordinal(60)] [RED("m_FallRecoverMaxHeightDownF")] 		public CFloat M_FallRecoverMaxHeightDownF { get; set;}

		[Ordinal(61)] [RED("m_FallRecoverMaxDistF")] 		public CFloat M_FallRecoverMaxDistF { get; set;}

		[Ordinal(62)] [RED("m_CanSetVelocityB")] 		public CBool M_CanSetVelocityB { get; set;}

		[Ordinal(63)] [RED("m_ForceIdleJumpOnColliisonB")] 		public CBool M_ForceIdleJumpOnColliisonB { get; set;}

		[Ordinal(64)] [RED("m_ForceIdleJumpHeightFreeF")] 		public CFloat M_ForceIdleJumpHeightFreeF { get; set;}

		[Ordinal(65)] [RED("m_ForceIdleJumpDistFreeF")] 		public CFloat M_ForceIdleJumpDistFreeF { get; set;}

		[Ordinal(66)] [RED("m_InteractionLastLockingF")] 		public CFloat M_InteractionLastLockingF { get; set;}

		[Ordinal(67)] [RED("m_LandPredictedB")] 		public CBool M_LandPredictedB { get; set;}

		[Ordinal(68)] [RED("m_LandGroundPredictB")] 		public CBool M_LandGroundPredictB { get; set;}

		[Ordinal(69)] [RED("m_LandWaterPredictB")] 		public CBool M_LandWaterPredictB { get; set;}

		[Ordinal(70)] [RED("m_LandPredictTimeMin")] 		public CFloat M_LandPredictTimeMin { get; set;}

		[Ordinal(71)] [RED("m_LandPredictionTimeF")] 		public CFloat M_LandPredictionTimeF { get; set;}

		[Ordinal(72)] [RED("m_CollisionGroupsNamesNArr", 2,0)] 		public CArray<CName> M_CollisionGroupsNamesNArr { get; set;}

		[Ordinal(73)] [RED("m_LandPredicedTypeE")] 		public CEnum<ELandPredictionType> M_LandPredicedTypeE { get; set;}

		[Ordinal(74)] [RED("m_LandPredicedCoefF")] 		public CFloat M_LandPredicedCoefF { get; set;}

		[Ordinal(75)] [RED("m_LandPredicedBlendF")] 		public CFloat M_LandPredicedBlendF { get; set;}

		[Ordinal(76)] [RED("m_SlopedLandZF")] 		public CFloat M_SlopedLandZF { get; set;}

		[Ordinal(77)] [RED("m_JumpOriginalPositionV")] 		public Vector M_JumpOriginalPositionV { get; set;}

		[Ordinal(78)] [RED("m_CameraDebugB")] 		public CBool M_CameraDebugB { get; set;}

		[Ordinal(79)] [RED("m_CameraStartB")] 		public CBool M_CameraStartB { get; set;}

		[Ordinal(80)] [RED("m_CameraPositionV")] 		public Vector M_CameraPositionV { get; set;}

		[Ordinal(81)] [RED("m_CameraRotationEA")] 		public EulerAngles M_CameraRotationEA { get; set;}

		[Ordinal(82)] [RED("m_CameraTimeToEndF")] 		public CFloat M_CameraTimeToEndF { get; set;}

		[Ordinal(83)] [RED("cameraRoationName")] 		public CName CameraRoationName { get; set;}

		[Ordinal(84)] [RED("cameraToFallHeightNeed")] 		public CFloat CameraToFallHeightNeed { get; set;}

		[Ordinal(85)] [RED("cameraFallIsSet")] 		public CBool CameraFallIsSet { get; set;}

		[Ordinal(86)] [RED("m_CollideBehGraphSideNameS")] 		public CName M_CollideBehGraphSideNameS { get; set;}

		[Ordinal(87)] [RED("m_CollidingSideE")] 		public CEnum<ESideSelected> M_CollidingSideE { get; set;}

		[Ordinal(88)] [RED("m_CooldownTotalF")] 		public CFloat M_CooldownTotalF { get; set;}

		[Ordinal(89)] [RED("m_CooldownCurF")] 		public CFloat M_CooldownCurF { get; set;}

		[Ordinal(90)] [RED("useWalkJump")] 		public CBool UseWalkJump { get; set;}

		[Ordinal(91)] [RED("useIdleWalkJump")] 		public CBool UseIdleWalkJump { get; set;}

		[Ordinal(92)] [RED("useHighJump")] 		public CBool UseHighJump { get; set;}

		[Ordinal(93)] [RED("jumpingOnIdleIsForward")] 		public CBool JumpingOnIdleIsForward { get; set;}

		[Ordinal(94)] [RED("jumpIdleWhenObstructed")] 		public CBool JumpIdleWhenObstructed { get; set;}

		public CExplorationStateJump(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExplorationStateJump(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}