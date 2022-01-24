using GnuOne.Data;
using Library;
using Library.HelpClasses;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System.Security.Cryptography;
using System.Text;

//RSA rsa = RSA.Create();

//RSAParameters rsaKeyInfo = rsa.ExportParameters(false);

//var a = rsa.ToString();

////public key
//var priv = rsa.ExportRSAPrivateKey();

//var pub =§rsa.ExportRSAPublicKey();

RsaKeyPairGenerator keyGeneretor = new RsaKeyPairGenerator();
keyGeneretor.Init(new Org.BouncyCastle.Crypto.KeyGenerationParameters(new Org.BouncyCastle.Security.SecureRandom(), 2048));
AsymmetricCipherKeyPair keypair = keyGeneretor.GenerateKeyPair(); 

RsaKeyParameters PrivateKey = (RsaKeyParameters)keypair.Private;
RsaKeyParameters PublicKey = (RsaKeyParameters)keypair.Public;

TextWriter writer = new StringWriter();
PemWriter pemWriter = new PemWriter(writer);
pemWriter.WriteObject(PublicKey);
pemWriter.Writer.Flush();

string print_pub = writer.ToString(); // skriver pubnyckel

TextWriter writ = new StringWriter();
PemWriter pemWrit = new PemWriter(writ);
pemWrit.WriteObject(PrivateKey);
pemWrit.Writer.Flush();

string priv = writ.ToString();

//
RsaKeyPairGenerator peyGeneretor = new RsaKeyPairGenerator();
peyGeneretor.Init(new Org.BouncyCastle.Crypto.KeyGenerationParameters(new Org.BouncyCastle.Security.SecureRandom(), 2048));
AsymmetricCipherKeyPair peypair = peyGeneretor.GenerateKeyPair(); 

RsaKeyParameters Privatepey = (RsaKeyParameters)peypair.Private;
RsaKeyParameters Publicpey = (RsaKeyParameters)peypair.Public;

TextWriter priter = new StringWriter();
PemWriter pempriter = new PemWriter(priter);
pempriter.WriteObject(Publicpey);
pempriter.Writer.Flush();

string pint_pub = priter.ToString(); // skriver pubnyckel

TextWriter prit = new StringWriter();
PemWriter pemprit = new PemWriter(prit);
pemprit.WriteObject(Privatepey);
pemprit.Writer.Flush();

string prip = writ.ToString();



var friend = new MyFriend { Email = "mailconsolejonatan@gmail.com", userName = "yos", hideFriend = true,pictureID = 5 };

var jsonfriend = JsonConvert.SerializeObject(friend);
var jsonextraCrypt = AesCryption.Encrypt(jsonfriend, "SecretKey");
var JsonExtraE= AesCryption.Encrypt(jsonextraCrypt, "SecretKey");
var JsonExtraEE = AesCryption.Encrypt(JsonExtraE, "SecretKey");

byte[] tmpSource;
byte[] tmpHash;

//encrypt
tmpSource = ASCIIEncoding.ASCII.GetBytes(jsonfriend);

IAsymmetricBlockCipher cipher = new OaepEncoding(new RsaEngine());
cipher.Init(true, PublicKey);
byte[] ciphertext = cipher.ProcessBlock(tmpSource, 0, tmpSource.Length);
string result = Encoding.UTF8.GetString(ciphertext);
Console.WriteLine("Encrypted text: "+ result);
Console.WriteLine();
Console.WriteLine();

//decrypt
var avkrypterat = Decryption(ciphertext, PrivateKey);


Console.WriteLine(avkrypterat);
Console.ReadKey();

static string Decryption(byte[] ct, RsaKeyParameters pvtkey)
{

    IAsymmetricBlockCipher cipherOne = new OaepEncoding(new RsaEngine());
    cipherOne.Init(false, pvtkey);
    byte[] decipher = cipherOne.ProcessBlock(ct, 0, ct.Length);
    string deciphherText = Encoding.UTF8.GetString(decipher);

   //var text = AesCryption.Decrypt(deciphherText, "SecretKey");

    return deciphherText;
}