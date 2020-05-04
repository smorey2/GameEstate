using GameEstate.Core;
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary.UltimaUO.Records
{
    public class ClientVersion : IComparable, IComparer
    {
        public enum ClientType
        {
            Regular,
            UOTD,
            God,
            SA
        }

        public ClientVersion(int maj, int min, int rev, int pat, ClientType type = ClientType.Regular)
        {
            Major = maj;
            Minor = min;
            Revision = rev;
            Patch = pat;
            Type = type;
            SourceString = _ToStringImpl();
        }
        public ClientVersion(string fmt)
        {
            SourceString = fmt;
            try
            {
                fmt = fmt.ToLowerInvariant();
                var br1 = fmt.IndexOf('.');
                var br2 = fmt.IndexOf('.', br1 + 1);
                var br3 = br2 + 1;
                while (br3 < fmt.Length && char.IsDigit(fmt, br3))
                    br3++;
                Utility.TryParseInt32(fmt.Substring(0, br1), out Major);
                Utility.TryParseInt32(fmt.Substring(br1 + 1, br2 - br1 - 1), out Minor);
                Utility.TryParseInt32(fmt.Substring(br2 + 1, br3 - br2 - 1), out Revision);
                if (br3 < fmt.Length)
                    if (Major <= 5 && Minor <= 0 && Revision <= 6)
                    { // Anything before 5.0.7
                        if (!char.IsWhiteSpace(fmt, br3))
                            Patch = fmt[br3] - 'a' + 1;
                    }
                    else
                        Utility.TryParseInt32(fmt.Substring(br3 + 1, fmt.Length - br3 - 1), out Patch);
                if (fmt.IndexOf("god") >= 0 || fmt.IndexOf("gq") >= 0) Type = ClientType.God;
                else if (fmt.IndexOf("third dawn") >= 0 || fmt.IndexOf("uo:td") >= 0 || fmt.IndexOf("uotd") >= 0 || fmt.IndexOf("uo3d") >= 0 || fmt.IndexOf("uo:3d") >= 0) Type = ClientType.UOTD;
                else Type = ClientType.Regular;
            }
            catch
            {
                Major = 0;
                Minor = 0;
                Revision = 0;
                Patch = 0;
                Type = ClientType.Regular;
            }
        }

        public readonly int Major;
        public readonly int Minor;
        public readonly int Revision;
        public readonly int Patch;
        public readonly ClientType Type;
        public readonly string SourceString;

        public static Task<ClientVersion> ReadAsync(BinaryDatFile source)
        {
            var path = source.GetFilePath("client.exe");
            if (path == null)
                return Task.FromResult<ClientVersion>(null);
            var info = FileVersionInfo.GetVersionInfo(path);
            if (info.FileMajorPart == 0 && info.FileMinorPart == 0 && info.FileBuildPart == 0 && info.FilePrivatePart == 0)
                return Task.FromResult<ClientVersion>(null);
            return Task.FromResult(new ClientVersion(info.FileMajorPart, info.FileMinorPart, info.FileBuildPart, info.FilePrivatePart));
        }

        public static bool operator ==(ClientVersion l, ClientVersion r) => Compare(l, r) == 0;
        public static bool operator !=(ClientVersion l, ClientVersion r) => Compare(l, r) != 0;
        public static bool operator >=(ClientVersion l, ClientVersion r) => Compare(l, r) >= 0;
        public static bool operator >(ClientVersion l, ClientVersion r) => Compare(l, r) > 0;
        public static bool operator <=(ClientVersion l, ClientVersion r) => Compare(l, r) <= 0;
        public static bool operator <(ClientVersion l, ClientVersion r) => Compare(l, r) < 0;
        public override int GetHashCode() => Major ^ Minor ^ Revision ^ Patch ^ (int)Type;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var v = obj as ClientVersion;
            return v == null
                ? false
                : Major == v.Major
                && Minor == v.Minor
                && Revision == v.Revision
                && Patch == v.Patch
                && Type == v.Type;
        }

        string _ToStringImpl()
        {
            var b = new StringBuilder(16);
            b.Append(Major); b.Append('.'); b.Append(Minor); b.Append('.'); b.Append(Revision);
            if (Major <= 5 && Minor <= 0 && Revision <= 6) // Anything before 5.0.7
            {
                if (Patch > 0)
                    b.Append((char)('a' + (Patch - 1)));
            }
            else { b.Append('.'); b.Append(Patch); }
            if (Type != ClientType.Regular) { b.Append(' '); b.Append(Type.ToString()); }
            return b.ToString();
        }

        public override string ToString() => _ToStringImpl();

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            var o = obj as ClientVersion;
            if (o == null)
                throw new ArgumentException();
            if (Major > o.Major) return 1;
            else if (Major < o.Major) return -1;
            else if (Minor > o.Minor) return 1;
            else if (Minor < o.Minor) return -1;
            else if (Revision > o.Revision) return 1;
            else if (Revision < o.Revision) return -1;
            else if (Patch > o.Patch) return 1;
            else if (Patch < o.Patch) return -1;
            else return 0;
        }

        public static bool IsNull(object x) => ReferenceEquals(x, null);

        public int Compare(object x, object y)
        {
            if (IsNull(x) && IsNull(y)) return 0;
            else if (IsNull(x)) return -1;
            else if (IsNull(y)) return 1;
            var a = x as ClientVersion;
            var b = y as ClientVersion;
            if (IsNull(a) || IsNull(b))
                throw new ArgumentException();
            return a.CompareTo(b);
        }

        public static int Compare(ClientVersion a, ClientVersion b)
        {
            if (IsNull(a) && IsNull(b)) return 0;
            else if (IsNull(a)) return -1;
            else if (IsNull(b)) return 1;
            return a.CompareTo(b);
        }
    }
}