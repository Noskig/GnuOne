using System.Security.Cryptography;

RSA rsa = RSA.Create();

RSAParameters rsaKeyInfo = rsa.ExportParameters(false);

var a = rsa.ToString();

//public key
var priv = rsa.ExportRSAPrivateKey();

var pub = rsa.ExportRSAPublicKey();






Console.WriteLine(a);