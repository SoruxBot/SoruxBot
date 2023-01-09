using System.Security.Cryptography;
using System.Text;

namespace Sorux.Framework.Bot.Core.Kernel.Utils;

/// <summary>
/// 哈希操作 扩展
/// </summary>
public static class HashExtensions
{
    /// <summary>
    /// 获取文件MD5
    /// </summary>
    /// <param name="path">文件地址</param>
    /// <returns>MD5</returns>
    public static string GetFileMd5(this string path)
    {
        return GetFileHash(path, MD5.Create());
    }

    /// <summary>
    /// 获取文本MD5
    /// </summary>
    /// <param name="str">字符串内容</param>
    /// <returns>MD5</returns>
    public static string GetMd5(this string str)
    {
        return GetHash(str, MD5.Create());
    }

    /// <summary>
    /// 获取文件Sha1
    /// </summary>
    /// <param name="path">文件地址</param>
    /// <returns>Sha1</returns>
    public static string GetFileSha1(this string path)
    {
        return GetFileHash(path, SHA1.Create());
    }

    /// <summary>
    /// 获取文本Sha1
    /// </summary>
    /// <param name="str">字符串内容</param>
    /// <returns>Sha1</returns>
    public static string GetSha1(this string str)
    {
        return GetHash(str, SHA1.Create());
    }


    /// <summary>
    /// 获取文件Sha256
    /// </summary>
    /// <param name="path">文件地址</param>
    /// <returns>Sha256</returns>
    public static string GetFileSha256(this string path)
    {
        return GetFileHash(path, SHA256.Create());
    }

    /// <summary>
    /// 获取文本Sha256
    /// </summary>
    /// <param name="str">字符串内容</param>
    /// <returns>Sha256</returns>
    public static string GetSha256(this string str)
    {
        return GetHash(str, SHA256.Create());
    }

    /// <summary>
    /// 获取文件Sha384
    /// </summary>
    /// <param name="path">文件地址</param>
    /// <returns>Sha384</returns>
    public static string GetFileSha384(this string path)
    {
        return GetFileHash(path, SHA384.Create());
    }

    /// <summary>
    /// 获取文本Sha384
    /// </summary>
    /// <param name="str">字符串内容</param>
    /// <returns>Sha384</returns>
    public static string GetSha384(this string str)
    {
        return GetHash(str, SHA384.Create());
    }

    /// <summary>
    /// 获取文件Sha512
    /// </summary>
    /// <param name="path">文件地址</param>
    /// <returns>Sha512</returns>
    public static string GetFileSha512(this string path)
    {
        return GetFileHash(path, SHA512.Create());
    }

    /// <summary>
    /// 获取文本Sha512
    /// </summary>
    /// <param name="str">字符串内容</param>
    /// <returns>Sha512</returns>
    public static string GetSha512(this string str)
    {
        return GetHash(str, SHA512.Create());
    }

    /// <summary>
    /// 获取文件的Hash
    /// </summary>
    /// <param name="path">文件地址</param>
    /// <param name="hash">hash算法</param>
    /// <returns>hash值</returns>
    public static string GetFileHash(this string path, HashAlgorithm hash)
    {
        return GetFileHash(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), hash);
    }

    /// <summary>
    /// 获取文件的Hash
    /// </summary>
    /// <param name="fs">文件流</param>
    /// <param name="hash">hash算法</param>
    /// <returns>hash值</returns>
    public static string GetFileHash(this FileStream fs, HashAlgorithm hash)
    {
        using (fs)
        using (hash)
        using (var bs = new BufferedStream(fs))
        {
            var sb = new StringBuilder();
            var result = hash.ComputeHash(bs);
            foreach (var t in result)
                sb.Append(t.ToString("x2"));
            return sb.ToString();
        }
    }

    /// <summary>
    /// 获取文本的Hash
    /// </summary>
    /// <param name="str">文本</param>
    /// <param name="hash">hash算法</param>
    /// <param name="encoding">编码</param>
    /// <returns>hash值</returns>
    public static string GetHash(this string str, HashAlgorithm hash, Encoding encoding)
    {
        using (hash)
        {
            var sb = new StringBuilder();
            var result = hash.ComputeHash(encoding.GetBytes(str));
            foreach (var t in result)
                sb.Append(t.ToString("x2"));
            return sb.ToString();
        }
    }

    /// <summary>
    /// 获取文本的Hash
    /// </summary>
    /// <param name="str">文本</param>
    /// <param name="hash">hash算法</param>
    /// <returns>hash值</returns>
    public static string GetHash(this string str, HashAlgorithm hash)
    {
        return GetHash(str, hash, Encoding.UTF8);
    }
}