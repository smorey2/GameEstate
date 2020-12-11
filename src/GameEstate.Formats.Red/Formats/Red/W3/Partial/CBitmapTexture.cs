using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;
using WolvenKit.Common.Model;

namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public partial class CBitmapTexture : ITexture
	{
		[Ordinal(1)] [RED("width")] 		public CUInt32 Width { get; set;}

		[Ordinal(2)] [RED("height")] 		public CUInt32 Height { get; set;}

		[Ordinal(3)] [RED("format")] 		public CEnum<ETextureRawFormat> Format { get; set;}

		[Ordinal(4)] [RED("compression")] 		public CEnum<ETextureCompression> Compression { get; set;}

		[Ordinal(5)] [RED("sourceData")] 		public CHandle<CSourceTexture> SourceData { get; set;}

		[Ordinal(6)] [RED("textureGroup")] 		public CName TextureGroup { get; set;}

		[Ordinal(7)] [RED("pcDownscaleBias")] 		public CInt32 PcDownscaleBias { get; set;}

		[Ordinal(8)] [RED("xboneDownscaleBias")] 		public CInt32 XboneDownscaleBias { get; set;}

		[Ordinal(9)] [RED("ps4DownscaleBias")] 		public CInt32 Ps4DownscaleBias { get; set;}

		[Ordinal(10)] [RED("residentMipIndex")] 		public CUInt8 ResidentMipIndex { get; set;}

		[Ordinal(11)] [RED("textureCacheKey")] 		public CUInt32 TextureCacheKey { get; set;}

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBitmapTexture(cr2w, parent, name);

	}
}