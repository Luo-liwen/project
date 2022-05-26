using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MyMVCDemoo.Models.Tools
{
    public class Util
    {
        public static void CopyObjectData(object source,object target,string excludeProperties = "")
        {
            var excluded = new List<string>();
            if (!string.IsNullOrEmpty(excludeProperties))
                excluded = excludeProperties.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var excludedNew = new List<string>();
            foreach(var item in excluded)
            {
                excludedNew.Add(item.ToUpper());
            }

            var miT = target.GetType().GetMembers();
            foreach (
                var field in
                miT.Where(m =>( m.MemberType == MemberTypes.Property) || (m.MemberType == MemberTypes.Field)))
            {
                var name = field.Name;
                //Skip over any property exceptions
                if (!string.IsNullOrEmpty(excludeProperties) && excludedNew.Contains(name.ToUpper()))
                    continue;
                if (field.MemberType == MemberTypes.Field)
                {
                    var sourceField = source.GetType().GetField(name);
                    if (sourceField == null)
                        continue;
                    var sourceValue = sourceField.GetValue(source);
                    ((FieldInfo)field).SetValue(target, sourceValue);
                }
                else if (field.MemberType == MemberTypes.Property)
                {
                    var piTarget = field as PropertyInfo;
                    var sourceField = source.GetType().GetProperty(name);
                    if (sourceField == null)
                        continue;
                    if (piTarget.CanWrite && sourceField.CanRead)
                    {
                        var sourceValue = sourceField.GetValue(source, null);
                        piTarget.SetValue(target, sourceValue, null);
                    }
                }
            }
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
